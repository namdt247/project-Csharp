using System;
using MySql.Data.MySqlClient;
using spring_hero_bank.Entity;
using spring_hero_bank.Helper;

namespace spring_hero_bank.Model
{
    public class GuestModel
    {
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
                    throw new Exception("Khong tim thay account hoac account da bi xoa");
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
                Console.WriteLine("Acction success!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
            }
            return false;
        }
    }
}