using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using spring_hero_bank.Entity;
using spring_hero_bank.Helper;

namespace spring_hero_bank.Model
{
    public class AdminModel
    {
        PasswordHelper _passwordHelper = new PasswordHelper();
        // 1. danh sách người dùng
        public List<Account> ListUser()
        {
            List<Account> listAccount = null;
            Account account;
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();

            try
            {
                listAccount = new List<Account>();
                var stringCmdGetAccount = $"select * from `accounts` where role = {(int) AccountRole.USER} and status = {(int) AccountStatus.ACTIVE}";
                var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                var readerGetAccount = cmdGetAccount.ExecuteReader();
                while (readerGetAccount.Read())
                {
                    account = new Account()
                    {
                        AccountNumber = readerGetAccount.GetString("accountNumber"),
                        Username = readerGetAccount.GetString("userName"),
                        Balance = readerGetAccount.GetDouble("balance"),
                        PasswordHash = readerGetAccount.GetString("passwordHash"),
                        Email = readerGetAccount.GetString("email"),
                        PhoneNumber = readerGetAccount.GetString("phoneNumber"),
                        Salt = readerGetAccount.GetString("salt"),
                        FullName = readerGetAccount.GetString("fullName"),
                        Role = (AccountRole) readerGetAccount.GetInt32("role"),
                        Status = (AccountStatus) readerGetAccount.GetInt32("status")
                    };
                    listAccount.Add(account);
                }
                cnn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return listAccount;
        }
        
        // 2. Danh sách lịch sử giao dịch.
        public List<SHBTransaction> ListTransaction()
        {
            List<SHBTransaction> listTran = null;
            SHBTransaction transaction;
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            try
            {
                listTran = new List<SHBTransaction>();
                var stringCmdGetAccount = $"select * from `transactions`";
                var cmd = new MySqlCommand(stringCmdGetAccount, cnn);
                var getListHistory = cmd.ExecuteReader();
                while (getListHistory.Read())
                {
                    transaction = new SHBTransaction()
                    {
                        TransactionCode = getListHistory.GetString("transactionCode"),
                        SenderAccountNumber = getListHistory.GetString("senderAccountNumber"),
                        ReceiverAccountNumber = getListHistory.GetString("receiverAccountNumber"),
                        Type = (TransactionType) getListHistory.GetInt32("type"),
                        Amount = getListHistory.GetDouble("amount"),
                        Fee = getListHistory.GetDouble("fee"),
                        Message = getListHistory.GetString("message"),
                        CreateAt = getListHistory.GetDateTime("createAt"),
                        UpdatedAt = getListHistory.GetDateTime("updatedAt"),
                        Status = (TransactionStatus) getListHistory.GetInt32("status")
                    };
                    listTran.Add(transaction);
                }

                Console.WriteLine("vao");
                cnn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return listTran;
        }
        
        // 3. Tim kiem theo ten
        public Account GetUserByAccountName(string name)
        {
            Account account = null;
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var stringCmdGetAccount = (
                $"SELECT * FROM `accounts` WHERE fullName = '{name}'"
            );
            var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
            var accountReader = cmdGetAccount.ExecuteReader();
            if (accountReader.Read())
            {
                account = new Account()
                {
                    AccountNumber = accountReader.GetString("accountNumber"),
                    Balance = accountReader.GetDouble("balance"),
                    Username = accountReader.GetString("userName"),
                    PhoneNumber = accountReader.GetString("phoneNumber"),
                    Role = (AccountRole) accountReader.GetInt32("role"),
                    FullName = accountReader.GetString("fullName"),
                    Email = accountReader.GetString("email"),
                    Status = (AccountStatus) accountReader.GetInt32("status")
                };
            }
            cnn.Close();
            return account;
        }
        
        // 4. Tim kiem theo stk
        public Account GetUserByAccountAccountNumber(string accountNumber)
        {
            Account account = null;
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var stringCmdGetAccount = (
                $"SELECT * FROM `accounts` WHERE accountNumber = '{accountNumber}'"
            );
            var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
            var accountReader = cmdGetAccount.ExecuteReader();
            if (accountReader.Read())
            {
                account = new Account()
                {
                    AccountNumber = accountReader.GetString("accountNumber"),
                    Balance = accountReader.GetDouble("balance"),
                    Username = accountReader.GetString("userName"),
                    PhoneNumber = accountReader.GetString("phoneNumber"),
                    Role = (AccountRole) accountReader.GetInt32("role"),
                    FullName = accountReader.GetString("fullName"),
                    Email = accountReader.GetString("email"),
                    Status = (AccountStatus) accountReader.GetInt32("status")
                };
            }

            cnn.Close();
            return account;
        }
        
        // 5. Tim kiem theo sdt
        public Account GetUserByPhone(string phoneNumber)
        {
            Account account = null;
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var stringCmdGetAccount = (
                $"SELECT * FROM `accounts` WHERE phoneNumber = '{phoneNumber}'"
            );
            var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
            var accountReader = cmdGetAccount.ExecuteReader();
            if (accountReader.Read())
            {
                account = new Account()
                {
                    AccountNumber = accountReader.GetString("accountNumber"),
                    Balance = accountReader.GetDouble("balance"),
                    Username = accountReader.GetString("userName"),
                    PhoneNumber = accountReader.GetString("phoneNumber"),
                    Role = (AccountRole) accountReader.GetInt32("role"),
                    FullName = accountReader.GetString("fullName"),
                    Email = accountReader.GetString("email"),
                    Status = (AccountStatus) accountReader.GetInt32("status")
                };
            }

            cnn.Close();
            return account;
        }
        
        // 7. lock and unlock user
        public bool ChangStatusUser(string accountNumber, int status)
        {
            if (status == 1)
            {
                Console.WriteLine($"Tài khoản {accountNumber} đang hoạt động, bạn có muốn khóa tài khoản này không?");
                Console.WriteLine("1. Có.");
                Console.WriteLine("2. Không.");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        try
                        {
                            var cnn = ConnectionHelpers.GetConnection();
                            cnn.Open();
                            var stringCmdGetAccount =
                                $"SELECT * FROM `accounts` WHERE accountNumber = '{accountNumber}'";
                            var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                            var accountReader = cmdGetAccount.ExecuteReader();
                            if (!accountReader.Read())
                            {
                                throw new Exception("Không tìm thấy tài khoản hoặc tài khoản đã bị xóa!");
                            }
                            accountReader.Close();

                            var updateStatus = $"UPDATE `accounts` SET `status`= {(int) AccountStatus.LOCK} WHERE accountNumber = '{accountNumber}'";
                            var cmdStatus = new MySqlCommand(updateStatus, cnn);
                            cmdStatus.ExecuteNonQuery();
                            cnn.Close();
                            return true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            return false;
                        }
                    case 2:
                        Console.WriteLine("Hủy thao tác!");
                        break;
                    default:
                        Console.WriteLine("Giá trị không phù hợp");
                        break;
                }
            }
            else if (status == 2)
            {
                Console.WriteLine($"Tài khoản {accountNumber} đang bị khóa, bạn có muốn mở khóa tài khoản này không?");
                Console.WriteLine("1. Có.");
                Console.WriteLine("2. Không.");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        try
                        {
                            var cnn = ConnectionHelpers.GetConnection();
                            cnn.Open();
                            var stringCmdGetAccount =
                                $"SELECT * FROM `accounts` WHERE accountNumber = '{accountNumber}'";
                            var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
                            var accountReader = cmdGetAccount.ExecuteReader();
                            if (!accountReader.Read())
                            {
                                throw new Exception("Không tìm thấy tài khoản hoặc tài khoản đã bị xóa!");
                            }
                            accountReader.Close();

                            var updateStatus = $"UPDATE `accounts` SET `status`= {(int) AccountStatus.ACTIVE} WHERE accountNumber = '{accountNumber}'";
                            var cmdStatus = new MySqlCommand(updateStatus, cnn);
                            cmdStatus.ExecuteNonQuery();
                            cnn.Close();
                            return true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            return false;
                        }
                    case 2:
                        Console.WriteLine("Hủy thao tác!");
                        break;
                    default:
                        Console.WriteLine("Giá trị không phù hợp");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Tài khoản đã bị xóa!");
            }

            return false;
        }

        // 8. Tim  lich su giao dich theo stk
        public List<SHBTransaction> TransactionHistory(string accountNumber)
        {
            List<SHBTransaction> list = new List<SHBTransaction>();
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            try
            {
                var cmdStringTransactionHistory =
                    $"SELECT * FROM transactions WHERE senderAccountNumber = '{accountNumber}' OR receiverAccountNumber = '{accountNumber}'";
                var cmd = new MySqlCommand(cmdStringTransactionHistory, cnn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var transactionHistory = new SHBTransaction()
                    {
                        TransactionCode = reader.GetString("transactionCode"),
                        SenderAccountNumber = reader.GetString("senderAccountNumber"),
                        ReceiverAccountNumber = reader.GetString("receiverAccountNumber"),
                        Type = (TransactionType) reader.GetInt32("type"),
                        Amount = reader.GetDouble("amount"),
                        Fee = reader.GetDouble("fee"),
                        Message = reader.GetString("message"),
                        CreateAt = (DateTime) reader.GetMySqlDateTime("createAt"),
                        UpdatedAt = (DateTime) reader.GetMySqlDateTime("updatedAt"),
                        Status = (TransactionStatus) reader.GetInt32("status"),
                    };
                    list.Add(transactionHistory);
                }

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        // 9. Thay doi thong tin tai khoan
        public bool EditAccount(string phoneNumber, string fullName, string email)
        {
            var account = GetUserByAccountAccountNumber(Program.currentLogin.AccountNumber);
            if (account == null)
            {
                Console.WriteLine("Không tìm thấy tài khoản hoặc tài khoản đã bị khóa!");
                return false;
            }
            
            try {
                var cnn = ConnectionHelpers.GetConnection();
                cnn.Open();
                var stringCmdEditAccount = (
                    $"UPDATE accounts SET phoneNumber = '{phoneNumber}', fullName = '{fullName}', email = '{email}' WHERE accountNumber = '{Program.currentLogin.AccountNumber} AND status = {(int) AccountStatus.ACTIVE}'"
                );
                var cmdEditAccount = new MySqlCommand(stringCmdEditAccount, cnn);
                cmdEditAccount.ExecuteNonQuery();
                cnn.Close();
                Console.WriteLine("Thay đổi thành công.");
                return true;
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
            }

            return false;
        }
        
        // 10. Thay doi password
        public bool ChangePassword(string password)
        {
            var account = GetUserByAccountAccountNumber(Program.currentLogin.Username);
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
    }
}