using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using spring_hero_bank.Entity;
using spring_hero_bank.Helper;
using spring_hero_bank.Model;

namespace spring_hero_bank.Controller
{
    public class AdminController
    {
        
        PasswordHelper _passwordHelper = new PasswordHelper();
        GuestModel _guestModel = new GuestModel();
        AdminModel _adminModel = new AdminModel();
        
        // 1. Danh sách người dùng
        public List<Account> ListUserController()
        {
            return _adminModel.ListUser();
        }
        
        // 2. Danh sach lich su giao dich
        public List<SHBTransaction> ListTransactionController()
        {
            return _adminModel.ListTransaction();
        }
        
        // 3. Tim kiem nguoi dung theo ten
        public Account GetAccountByName()
        {
            Console.WriteLine("Vui lòng nhập tên cần tìm kiếm: ");
            var name = Console.ReadLine();
            return _adminModel.GetUserByAccountName(name);
        }
        
        // 4. Tim kiem nguoi dung theo stk
        public Account GetAccountByAccountNumber()
        {
            Console.WriteLine("Vui lòng nhập stk cần tìm kiếm: ");
            var accountNumber = Console.ReadLine();
            return _adminModel.GetUserByAccountAccountNumber(accountNumber);
        }
        
        // 5. Tim kiem nguoi dung theo sdt
        public Account GetAccountByPhone()
        {
            Console.WriteLine("Vui lòng nhập sđt cần tìm kiếm: ");
            var phoneNumber = Console.ReadLine();
            return _adminModel.GetUserByPhone(phoneNumber);
        }
        
        // 6. Them nguoi dung moi
        public bool AdminRegister()
        {
            try
            {
                Console.WriteLine("----- Thêm người dùng mới -------");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Vui lòng nhập username: ");
                var username = Console.ReadLine();
                Console.WriteLine("Vui lòng nhập password: ");
                var password = Console.ReadLine();
                Console.WriteLine("Vui lòng nhập số điện thoại: ");
                var phoneNumber = Console.ReadLine();
                Console.WriteLine("Vui lòng nhập tên đầy đủ: ");
                var fullName = Console.ReadLine();
                Console.WriteLine("Vui lòng nhập email: ");
                var email = Console.ReadLine();
                Console.WriteLine("Vui lòng nhập kiểu tài khoản: (1.User and 2.Admin)");
                var role = int.Parse(Console.ReadLine());

                var salt = _passwordHelper.GenerateSalt();
                var passwordHash = _passwordHelper.MD5Hash(password + salt);

                var firstAccountNumber = "9704";
                var accountNumber = firstAccountNumber + _passwordHelper.GenerateAccountNumber();

                while (true)
                {
                    var checkUsername = _guestModel.ValidateUsername(username);
                    var checkAccount = _guestModel.ValidateAccountNumber(accountNumber);

                    if (checkUsername != null)
                    {
                        username = checkUsername;
                    }
                    
                    if (checkAccount != null)
                    {
                        accountNumber = checkAccount;
                    }

                    if (checkUsername == null && checkAccount == null)
                    {
                        var account = new Account()
                        {
                            AccountNumber = accountNumber,
                            Username = username,
                            Balance = 0,
                            PasswordHash = password,
                            PhoneNumber = phoneNumber,
                            Role = (AccountRole) role,
                            FullName = fullName,
                            Email = email,
                            Status = AccountStatus.ACTIVE,
                        };
                        _guestModel.Save(account);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        
        // 7. Khoa va mo tai khoan
        public bool ChangeUserStatus()
        {
            Console.WriteLine("Nhập tên stk cần tìm: ");
            var accountNumber = Console.ReadLine();
            var account = _adminModel.GetUserByAccountAccountNumber(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Không tìm thấy tài khoản");
                return false;
            }
            Console.WriteLine(account.ToString());
            return _adminModel.ChangStatusUser(accountNumber, (int) account.Status);
        }
        
        // 8. Tim kiem lich su giao dich theo so tai khoan
        public List<SHBTransaction> GetTransactionByAccountNumber()
        {
            Console.WriteLine("Nhập số tài khoản cần tìm: ");
            var accountNumber = Console.ReadLine();
            var account = _adminModel.GetUserByAccountAccountNumber(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Không tìm thấy tài khoản");
                return null;
            }

            return _adminModel.TransactionHistory(accountNumber);
        }
        
        // 9. Thay doi thong tin tai khoan
        public void EditAccount()
        {
            Console.WriteLine("---- Thay đổi thông tin cá nhân ----");
            Console.WriteLine("Vui lòng nhập email của bạn: ");
            var email = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập số điện thoại của bạn: ");
            var phoneNumber = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập tên đầy đủ của bạn: ");
            var fullName = Console.ReadLine();
            _adminModel.EditAccount(phoneNumber, fullName, email);
        }
        
        // 10. Thay doi mat khau
        public void PasswordChange()
        {
            Console.WriteLine("---- Thay đổi mật khẩu tài khoản ----");
            Console.WriteLine("Vui lòng nhập mật khẩu: ");
            var password = Console.ReadLine();
            Console.WriteLine("Vui lòng xác nhận lại mật khẩu: ");
            var confirmPassword = Console.ReadLine();
            if (password == confirmPassword)
            {
                _adminModel.ChangePassword(password);
            }
            else
            {
                Console.WriteLine("Xác nhận mật khẩu không khớp!");
            }
        }
    }
}