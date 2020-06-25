using System;
using System.Collections.Generic;
using System.Drawing;
using MySql.Data.MySqlClient;
using spring_hero_bank.Entity;
using spring_hero_bank.Helper;

namespace spring_hero_bank.Model
{
    public class UserModel
    {
        private GuestModel _guestModel = new GuestModel();
        private PasswordHelper _passwordHelper = new PasswordHelper();
        // 1. Gui tien
        public bool Deposit(string accountNumber, double amount)
        {
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var transaction = cnn.BeginTransaction();
            try
            {
                if (amount <= 0)
                {
                    throw new Exception("Giá trị không hợp lệ");
                }
            
                var stringCmdGetAccount = $"select balance from `accounts` where accountNumber = '{accountNumber}'" +
                                          $"and status = 1";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    throw new Exception("Không tìm thấy tài khoản hoặc tài khoản đã bị khóa!");
                }
            
                var currentBalance = accountReader.GetDouble("balance");
                accountReader.Close();
                currentBalance += amount;
                var stringCmdUpdateAccount = $"update `accounts` set balance = {currentBalance} where " +
                                             $"accountNumber = '{accountNumber}' and status = 1";
                var cmdUpdateAccount = new MySqlCommand(stringCmdUpdateAccount, cnn);
                cmdUpdateAccount.ExecuteNonQuery();
                
                var shbTransaction = new SHBTransaction()
                {
                    TransactionCode = Guid.NewGuid().ToString(),
                    SenderAccountNumber = accountNumber,
                    ReceiverAccountNumber = accountNumber,
                    Type = TransactionType.DEPOSIT,
                    Amount = amount,
                    Fee = 0,
                    Message = "Deposit" + amount,
                    CreateAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = TransactionStatus.DONE
                };
            
                var stringCmdInsertTransaction =
                    $"INSERT INTO `transactions`(`transactionCode`, `senderAccountNumber`, `receiverAccountNumber`, " +
                    $"`type`, `amount`, `fee`, `message`, `createAt`, `updatedAt`, `status`) " +
                    $"VALUES ('{shbTransaction.TransactionCode}', '{shbTransaction.SenderAccountNumber}', " +
                    $"'{shbTransaction.ReceiverAccountNumber}', {(int)shbTransaction.Type}," +
                    $"{shbTransaction.Amount}, {shbTransaction.Fee}, '{shbTransaction.Message}', " +
                    $"'{shbTransaction.CreateAt:yyyy-MM-dd hh:mm:ss}', '{shbTransaction.UpdatedAt: yyyy-MM-dd hh:mm:ss}'," +
                    $"{(int)shbTransaction.Status})";
                var cmdInsertTransaction = new MySqlCommand(stringCmdInsertTransaction, cnn);
                cmdInsertTransaction.ExecuteNonQuery();
                transaction.Commit();
                cnn.Close();
                Console.WriteLine("Gửi tiền vào tài khoản thành công!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
                cnn.Close();
            }
            return false;
        }
        
        // 2. Rút tiền
        public bool Withdraw(string accountNumber, double amount)
        {
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var transaction = cnn.BeginTransaction();
            try
            {
                if (amount <= 0)
                {
                    throw new Exception("Giá trị không hợp !");
                }
                var stringCmdGetAccount =
                    $"SELECT balance FROM `accounts` WHERE accountNumber = '{accountNumber}' AND status = {(int) AccountStatus.ACTIVE}";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    throw new Exception("Không tìm thấy tài khoản hoặc tài khoản đã bị khóa!");
                }
                var currentBalance = accountReader.GetDouble("balance");
                accountReader.Close();
                
                // check balance
                if (currentBalance < amount + 50000)
                {
                    Console.WriteLine("Số dư tài khoản không đủ!");
                    cnn.Close();
                    return false;
                }

                currentBalance -= amount;
                var stringCmdUpdateAccount =
                    $"UPDATE `accounts` SET balance = '{currentBalance}' WHERE accountNumber = '{accountNumber}' AND status = 1";
                var cmdUpdateAccount = new MySqlCommand(stringCmdUpdateAccount, cnn);
                cmdUpdateAccount.ExecuteNonQuery();
                var shbtransaction = new SHBTransaction()
                {
                    TransactionCode = Guid.NewGuid().ToString(),
                    SenderAccountNumber = accountNumber,
                    ReceiverAccountNumber = accountNumber,
                    Type = TransactionType.DEPOSIT,
                    Amount = amount,
                    Fee = 0,
                    Message = "Withdraw: " + amount,
                    CreateAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = TransactionStatus.DONE
                };
                var stringCmdInsertTransaction = $"INSERT INTO `transactions`(`transactionCode`, `senderAccountNumber`, `receiverAccountNumber`, " +
                                                 $"`type`, `amount`, `fee`, `message`, `createAt`, `updatedAt`, `status`) " +
                                                 $"VALUES ('{shbtransaction.TransactionCode}', '{shbtransaction.SenderAccountNumber}', " +
                                                 $"'{shbtransaction.ReceiverAccountNumber}', {(int)shbtransaction.Type}," +
                                                 $"{shbtransaction.Amount}, {shbtransaction.Fee}, '{shbtransaction.Message}', " +
                                                 $"'{shbtransaction.CreateAt:yyyy-MM-dd hh:mm:ss}', '{shbtransaction.UpdatedAt: yyyy-MM-dd hh:mm:ss}'," +
                                                 $"{(int)shbtransaction.Status})";
                var cmdInsertTransaction = new MySqlCommand(stringCmdInsertTransaction, cnn);
                cmdInsertTransaction.ExecuteNonQuery();
                transaction.Commit();
                cnn.Close();
                Console.WriteLine($"Rút {amount} thành công!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
                cnn.Close();
            }
            return false;
        }
        
        // 3. Chuyen khoan
        public bool Tranfer(string senderAccountNumber, string receiverAccountNumber, double amount)
        {
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var transaction = cnn.BeginTransaction();
            try
            {
                if (amount <= 0)
                {
                    throw new Exception("Giá trị không hợp !");
                }
                
                // get receiver account
                var stringCmdGetReceiverAccount =
                    $"SELECT balance FROM `accounts` WHERE accountNumber = '{receiverAccountNumber}' AND status = {(int) AccountStatus.ACTIVE}";
                var cmdGetReceiverAccount = new MySqlCommand(stringCmdGetReceiverAccount, cnn);
                var accountReceiverReader = cmdGetReceiverAccount.ExecuteReader();
                if (!accountReceiverReader.Read())
                {
                    throw new Exception("Không tìm thấy tài khoản hoặc tài khoản đã bị khóa!");
                }
                
                var currentReceiverBalance = accountReceiverReader.GetDouble("balance");
                accountReceiverReader.Close();

                // get sender account
                var stringCmdGetSenderAccount =
                    $"SELECT balance FROM `accounts` WHERE accountNumber = '{senderAccountNumber}' AND status = {(int) AccountStatus.ACTIVE}";
                var cmdGetSenderAccount = new MySqlCommand(stringCmdGetSenderAccount, cnn);
                var accountSenderReader = cmdGetSenderAccount.ExecuteReader();
                if (!accountSenderReader.Read())
                {
                    throw new Exception("Không tìm thấy tài khoản hoặc tài khoản đã bị khóa!");
                }
                
                var currentSenderBalance = accountSenderReader.GetDouble("balance");
                accountSenderReader.Close();
                
                // check balance
                if (currentSenderBalance < amount + 50000)
                {
                    throw new Exception("Số dư tài khoản không đủ!");
                }
                
                currentSenderBalance -= amount;
                var stringCmdUpdateSenderAccount =
                    $"UPDATE `accounts` SET balance = '{currentSenderBalance}' WHERE accountNumber = '{senderAccountNumber}' AND status = 1";
                var cmdUpdateSenderAccount = new MySqlCommand(stringCmdUpdateSenderAccount, cnn);
                cmdUpdateSenderAccount.ExecuteNonQuery();
                
                currentReceiverBalance += amount;
                var stringCmdUpdateReceiverAccount =
                    $"UPDATE `accounts` SET balance = '{currentReceiverBalance}' WHERE accountNumber = '{receiverAccountNumber}' AND status = 1";
                var cmdUpdateReceiverAccount = new MySqlCommand(stringCmdUpdateReceiverAccount, cnn);
                cmdUpdateSenderAccount.ExecuteNonQuery();
                
                var shbtransaction = new SHBTransaction()
                {
                    TransactionCode = Guid.NewGuid().ToString(),
                    SenderAccountNumber = senderAccountNumber,
                    ReceiverAccountNumber = receiverAccountNumber,
                    Type = TransactionType.TRANFER,
                    Amount = amount,
                    Fee = 0,
                    Message = "Tranfer from " + senderAccountNumber + " to " + receiverAccountNumber + " amount: " + amount,
                    CreateAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = TransactionStatus.DONE
                };
                var stringCmdInsertTransaction = $"INSERT INTO `transactions`(`transactionCode`, `senderAccountNumber`, `receiverAccountNumber`, " +
                                                 $"`type`, `amount`, `fee`, `message`, `createAt`, `updatedAt`, `status`) " +
                                                 $"VALUES ('{shbtransaction.TransactionCode}', '{shbtransaction.SenderAccountNumber}', " +
                                                 $"'{shbtransaction.ReceiverAccountNumber}', {(int)shbtransaction.Type}," +
                                                 $"{shbtransaction.Amount}, {shbtransaction.Fee}, '{shbtransaction.Message}', " +
                                                 $"'{shbtransaction.CreateAt:yyyy-MM-dd hh:mm:ss}', '{shbtransaction.UpdatedAt: yyyy-MM-dd hh:mm:ss}'," +
                                                 $"{(int)shbtransaction.Status})";
                var cmdInsertTransaction = new MySqlCommand(stringCmdInsertTransaction, cnn);
                cmdInsertTransaction.ExecuteNonQuery();
                transaction.Commit();
                cnn.Close();
                Console.WriteLine($"Chuyển {amount} đến tài khoản {receiverAccountNumber} thành công!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
                cnn.Close();
            }
            return false;
        }
        
        // 4. Truy van so du
        public void BalanceQuery(string username)
        {
            Account account = _guestModel.GetActiveAccountByUserName(username);
            if (account == null)
            {
                Console.WriteLine("Không tìm thấy tài khoản hoặc tài khoản đã bị khóa!");
                return;
            }
            Console.WriteLine("Số dư tài khoản: " + account.Balance);
        }
        
        // 5. Thay doi thong tin ca nhan
        public bool ChangeUser(string email, string phoneNumber, string fullName)
        {
            var account = _guestModel.GetActiveAccountByUserName(Program.currentLogin.Username);
            if (account == null)
            {
                Console.WriteLine("Không tìm thấy tài khoản hoặc tài khoản đã bị khóa!");
                return false;
            }
            
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            try
            {
                var stringCmdUpdateAccount =
                    $"UPDATE `accounts` SET `phoneNumber` = '{phoneNumber}', `fullName` = '{fullName}', `email` = '{email}' WHERE username = '{Program.currentLogin.Username}' AND status = {(int) AccountStatus.ACTIVE}";
                var cmdUpdateAccount = new MySqlCommand(stringCmdUpdateAccount, cnn);
                cmdUpdateAccount.ExecuteNonQuery();
                cnn.Close();
                Console.WriteLine("Thay đổi thông tin thành công!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                cnn.Close();
            }
            
            return false;
        }
        
        // 6. Thay doi thong tin mat khau
        public bool ChangePassword(string password)
        {
            Account account = _guestModel.GetActiveAccountByUserName(Program.currentLogin.Username);
            if (account == null)
            {
                Console.WriteLine("Không tìm thấy tài khoản hoặc tài khoản đã bị khóa!");
                return false;
            }
            
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            try
            {
                var salt = account.Salt;
                var passwordHash = _passwordHelper.MD5Hash(password + salt);
                var stringCmdUpdateAccount =
                    $"UPDATE `accounts` SET `passwordHash` = '{passwordHash}' WHERE username = '{Program.currentLogin.Username}' AND status = {(int) AccountStatus.ACTIVE}";
                var cmdUpdateAccount = new MySqlCommand(stringCmdUpdateAccount, cnn);
                cmdUpdateAccount.ExecuteNonQuery();
                cnn.Close();
                Console.WriteLine("Đổi mật khẩu thành công!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                cnn.Close();
            }
            
            return false;
        }
        
        // 7. Truy vấn lịch sử giao dịch
        public List<SHBTransaction> TransactionHistory()
        {
            List<SHBTransaction> list = null;
            SHBTransaction shbTransaction;
            Account account = _guestModel.GetActiveAccountByUserName(Program.currentLogin.Username);
            if (account == null)
            {
                Console.WriteLine("Không tìm thấy tài khoản hoặc tài khoản đã bị khóa!");
                return null;
            }
            
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            try
            {
                list = new List<SHBTransaction>();
                var stringCmdGetTransaction =
                    $"SELECT * FROM `transactions` WHERE senderAccountNumber = '{account.AccountNumber}' OR receiverAccountNumber = '{account.AccountNumber}'";
                var cmdGetTransaction = new MySqlCommand(stringCmdGetTransaction, cnn);
                var readerGetTransaction = cmdGetTransaction.ExecuteReader();
                while (readerGetTransaction.Read())
                {
                    shbTransaction = new SHBTransaction()
                    {
                        TransactionCode = readerGetTransaction.GetString("transactionCode"),
                        SenderAccountNumber = readerGetTransaction.GetString("senderAccountNumber"),
                        ReceiverAccountNumber = readerGetTransaction.GetString("receiverAccountNumber"),
                        Type = (TransactionType) readerGetTransaction.GetInt32("type"),
                        Amount = readerGetTransaction.GetDouble("amount"),
                        Fee = readerGetTransaction.GetDouble("fee"),
                        Message = readerGetTransaction.GetString("message"),
                        CreateAt = readerGetTransaction.GetDateTime("createAt"),
                        UpdatedAt = readerGetTransaction.GetDateTime("updatedAt"),
                        Status = (TransactionStatus) readerGetTransaction.GetInt32("status")
                    };
                    list.Add(shbTransaction);
                }
                cnn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                cnn.Close();
            }

            return list;
        }
    }
}