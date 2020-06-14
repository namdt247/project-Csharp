using System;
using MySql.Data.MySqlClient;
using spring_hero_bank.Entity;
using spring_hero_bank.Helper;

namespace spring_hero_bank.Model
{
    public class AccountModel
    {
        public bool Save(Account account)
        {
            try
            {
                var cnn = ConnectionHelpers.GetConnection();
                cnn.Open();
                var strCmdRegister = $"insert into accounts (accountNumber, balance, userName, " +
                                     $"passwordHash, phoneNumber, salt, role, fullName, email, status) values " +
                                     $"('{account.AccountNumber}', {account.Balance}, '{account.Username}'," +
                                     $"'{account.PasswordHash}', '{{account.PhoneNumber}}', '{{account.Salt}}'," +
                                     $"{{(int)account.Role}}, '{account.FullName}', '{account.Email}'," +
                                     $"{(int)account.Status})";
                var cmdRegister = new MySqlCommand(strCmdRegister, cnn);
                cmdRegister.ExecuteNonQuery();
                cnn.Close();
                Console.WriteLine("Tạo tài khoản thành công!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Console.WriteLine("Tạo tài khoản không thành công!");
                return false;
            }
        }

        public Account GetActiveAccountByUserName(string username)
        {
            Account account = null;
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var stringCmdGetAccount = $"select * from accounts where username = '{username}' and " +
                $"status = {(int)AccountSHBStatus.ACTIVE}";
            var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
            var readerGetAccount = cmdGetAccount.ExecuteReader();
            if (readerGetAccount.Read())
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
                    Role = (AccountSHBRole) readerGetAccount.GetInt32("role"),
                    Status = (AccountSHBStatus) readerGetAccount.GetInt32("status")
                };
            }
            cnn.Close();
            return account;
        }
    }
}