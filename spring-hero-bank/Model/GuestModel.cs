using System;
using MySql.Data.MySqlClient;
using spring_hero_bank.Entity;
using spring_hero_bank.Helper;
using spring_hero_bank.View;

namespace spring_hero_bank.Model
{
    public class GuestModel
    {
        private PasswordHelper _passwordHelper = new PasswordHelper();
        public bool Save(Account account)
        {
            try
            {
                var cnn = ConnectionHelpers.GetConnection();
                cnn.Open();
                var strCmdRegister = $"insert into accounts (accountNumber, balance, userName, passwordHash, phoneNumber, salt, role, fullName, email, status) values ('{account.AccountNumber}', {Convert.ToDouble(account.Balance)}, '{account.Username}', '{account.PasswordHash}', '{account.PhoneNumber}', '{account.Salt}', " +
                                     $"{Convert.ToInt32(account.Role)}, '{account.FullName}', '{account.Email}', {Convert.ToInt32(account.Status)})";
                var cmdRegister = new MySqlCommand(strCmdRegister, cnn);
                cmdRegister.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Account GetActiveAccountByUserName(string username)
        {
            Account account = null;
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var stringCmdGetAccount = $"select * from accounts where username = '{username}' and " +
                $"status = {(int)AccountStatus.ACTIVE}";
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
                    Role = (AccountRole) readerGetAccount.GetInt32("role"),
                    Status = (AccountStatus) readerGetAccount.GetInt32("status")
                };
            }
            cnn.Close();
            return account;
        }
        
        public string ValidateUsername(string username)
        {
            string newUserName = null;
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var strGetUsername =
                $"select * from accounts where userName = '{username}'";
            var cmdGetUsername = new MySqlCommand(strGetUsername, cnn);
            var usernameReader = cmdGetUsername.ExecuteReader();
            if (usernameReader.Read())
            {
                Console.WriteLine("Username đã tồn tại, vui lòng nhập lại user của bạn: ");
                newUserName = Console.ReadLine();
            }
            cnn.Close();
            return newUserName;
        }
        public string ValidateAccountNumber(string accountNumber)
        {
            string newAccountNumber = null;
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var strGetAccount =
                $"select * from accounts where accountNumber = '{accountNumber}'";
            var cmdGetAccountNumber = new MySqlCommand(strGetAccount, cnn);
            var accountReader = cmdGetAccountNumber.ExecuteReader();
            if (accountReader.Read())
            {
                var firstAccountNumber = "9704";
                newAccountNumber = firstAccountNumber + _passwordHelper.GenerateAccountNumber();
            }
            cnn.Close();
            return newAccountNumber;
        }
    }
}