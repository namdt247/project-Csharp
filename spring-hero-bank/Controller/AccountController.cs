using System;
using spring_hero_bank.Entity;
using spring_hero_bank.Helper;
using spring_hero_bank.Model;

namespace spring_hero_bank.Controller
{
    public class AccountController
    {
        private PasswordHelper _passwordHelper = new PasswordHelper();
        private AccountModel _accountModel = new AccountModel();
        
        public bool Register()
        {
            try
            {
                Console.WriteLine("Đăng ký tài ");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Vui lòng nhập username của bạn: ");
                var username = Console.ReadLine();
                Console.WriteLine("Vui lòng nhập password của bạn: ");
                var password = Console.ReadLine();
                Console.WriteLine("Vui lòng nhập email của bạn: ");
                var email = Console.ReadLine();
                Console.WriteLine("Vui lòng nhập số điện thoại của bạn: ");
                var phoneNumber = Console.ReadLine();
                Console.WriteLine("Vui lòng nhập tên đầy đủ của bạn: ");
                var fullName = Console.ReadLine();

                var salt = _passwordHelper.GenerateSalt();
                var passwordHash = _passwordHelper.MD5Hash(password + salt);

                var firstAccountNumber = "9704";
                var accountNumber = firstAccountNumber + _passwordHelper.GenerateAccountNumber();
            
                var account = new Account()
                {
                    AccountNumber = accountNumber,
                    Username = username,
                    Balance = 0,
                    PasswordHash = passwordHash,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Salt = salt,
                    FullName = fullName,
                    Role = AccountSHBRole.GUEST,
                    Status = AccountSHBStatus.ACTIVE,
                };

                _accountModel.Save(account);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Account Login()
        {
            Console.WriteLine("Đăng nhập hệ thống");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Vui lòng nhập username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập password: ");
            var password = Console.ReadLine();
            Account account = _accountModel.GetActiveAccountByUserName(username);
            if (account != null && _passwordHelper.ComparePassword(password, account.Salt, account.PasswordHash))
            {
                return account;
            }

            return null;
        }
    }
}