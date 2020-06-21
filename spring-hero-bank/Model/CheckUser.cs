using System;
using System.Text;
using MySql.Data.MySqlClient;
using spring_hero_bank.Helper;

namespace spring_hero_bank.Model
{
    public class CheckUser
    {
        private PasswordHelper _passwordHelper = new PasswordHelper();
        
        public String ValidateUsername(string username)
        {
            string account = null;
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var strGetUsername =
                $"select * from accounts where userName = '{username}'";
            var cmdGetUsername = new MySqlCommand(strGetUsername, cnn);
            var usernameReader = cmdGetUsername.ExecuteReader();
            Console.OutputEncoding = Encoding.UTF8;
            if (usernameReader.Read())
            {
                Console.WriteLine("Vui lòng nhập username của bạn: ");
                account = Console.ReadLine();
            }
            cnn.Close();
            return account;
        }
        public String ValidateAccountNumber(string accountNumber)
        {
            String account_number = null;
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var strGetAccount =
                $"select * from accounts where accountNumber = '{accountNumber}'";
            var cmdGetAccountNumber = new MySqlCommand(strGetAccount, cnn);
            var accountReader = cmdGetAccountNumber.ExecuteReader();
            if (accountReader.Read())
            {
                var firstAccountNumber = "9704";
                account_number = firstAccountNumber + _passwordHelper.GenerateAccountNumber();
            }
            cnn.Close();
            return account_number;
        }
    }
}