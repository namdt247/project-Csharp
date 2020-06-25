using System;
using System.Collections.Generic;
using spring_hero_bank.Entity;
using spring_hero_bank.Model;

namespace spring_hero_bank.Controller
{
    public class UserController
    {
        private UserModel userModel = new UserModel();
        // 1. gửi tiền
        public void DepositController()
        {
            Console.WriteLine("Vui lòng nhập số tiền muốn gửi vào tài khoản: ");
            var amount = double.Parse(Console.ReadLine());
            userModel.Deposit(Program.currentLogin.AccountNumber, amount);
        }
        
        // 2. rút tiền
        public void WithdrawController()
        {
            Console.WriteLine("Vui lòng nhập số tiền muốn : ");
            var amount = double.Parse(Console.ReadLine());
            userModel.Withdraw(Program.currentLogin.AccountNumber, amount);
        }
        
        // 3. chuyển khoản
        public void TransferController()
        {
            Console.WriteLine("Vui lòng nhập số tài khoản chuyển : ");
            var receiverAccountNumber = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập số tiền: ");
            var amount = double.Parse(Console.ReadLine());
            userModel.Tranfer(Program.currentLogin.AccountNumber, receiverAccountNumber, amount);
        }
        // 4. truy vấn số dư
        public void BalanceQueryController()
        {
            userModel.BalanceQuery(Program.currentLogin.Username);
        }
        
        // 5. Thay doi thong tin ca nhan
        public void ChangeUserController()
        {
            Console.WriteLine("---- Thay đổi thông tin cá nhân ----");
            Console.WriteLine("Vui lòng nhập email của bạn: ");
            var email = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập số điện thoại của bạn: ");
            var phoneNumber = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập tên đầy đủ của bạn: ");
            var fullName = Console.ReadLine();
            userModel.ChangeUser(email, phoneNumber, fullName);
        }
        
        // 6. Thay doi mat khau
        public void ChangePasswordController()
        {
            Console.WriteLine("---- Thay đổi mật khẩu tài  ----");
            Console.WriteLine("Vui lòng nhập mật khẩu: ");
            var password = Console.ReadLine();
            Console.WriteLine("Vui lòng xác nhận lại mật khẩu: ");
            var confirmPassword = Console.ReadLine();
            if (password == confirmPassword)
            {
                userModel.ChangePassword(password);
            }
            else
            {
                Console.WriteLine("Xác nhận mật khẩu không khớp!");
            }
        }
        
        // 7. Truy van lich su giao dich
        public List<SHBTransaction> TransactionHistoryController()
        {
            return userModel.TransactionHistory();
        }
    }
}