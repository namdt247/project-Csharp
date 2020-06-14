using System;
using spring_hero_bank.Entity;
using spring_hero_bank.Helper;
using spring_hero_bank.Model;

namespace spring_hero_bank.Controller
{
    public class GuestController
    {
        private PasswordHelper _passwordHelper = new PasswordHelper();
        private AccountModel _accountModel = new AccountModel();

        public Account Login()
        {
            Console.WriteLine("Login...");
            Console.WriteLine("Please enter your username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            var password = Console.ReadLine();
            var account = _accountModel.GetActiveAccountByUsername(username);
            // mã hoá pass ng dùng nhập vào kèm theo muối trong database và so sánh kết quả với password đã mã hoá ở trong db.
            if (account != null
                && _passwordHelper.ComparePassword(password, account.Salt, account.PasswordHash))
            {
                return account;
            }
            return null;
        }

        public bool Register()
        {
            try
            {
                Console.WriteLine("-- Tạo tài khoản mới --");
                Console.WriteLine("Vui lòng nhập : ");
                var username = Console.ReadLine();
                // validate username.
                Console.WriteLine("Please enter your password: ");
                var password = Console.ReadLine();
                Console.WriteLine("Please enter your fullname: ");
                var fullName = Console.ReadLine();
                Console.WriteLine("Please enter your emails: ");
                var email = Console.ReadLine();
                Console.WriteLine("Please choose status: ");
                var strStatus = Console.ReadLine();
                var status = int.Parse(strStatus);

                var salt = _passwordHelper.GenerateSalt();
                var passwordHash = _passwordHelper.MD5Hash(password + salt);
                // 1. md5 hash string.
                // 2. generate random string.
                var account = new Account()
                {
                    Username = username,
                    PasswordHash = passwordHash,
                    Salt = salt,
                    FullName = fullName,
                    Email = email,
                    Status = (AccountStatus) status
                };
                // làm việc với model để lưu vào database.
                _accountModel.Save(account);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}