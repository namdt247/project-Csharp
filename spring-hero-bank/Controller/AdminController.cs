using System;
using MySql.Data.MySqlClient;
using spring_hero_bank.Entity;
using spring_hero_bank.Helper;

namespace spring_hero_bank.Controller
{
    public class AdminController
    {
        //1. danh sách người dùng
        public String ListUser()
        {
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var stringCmdGetAccount = $"select * from accounts";
            var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
            var readerGetAccount = cmdGetAccount.ExecuteReader();
            var renderAccount = "";
            foreach (var account in readerGetAccount)
            {
                Console.WriteLine("132423");
            }
            cnn.Close();
            return "Số tài khoản";
        }
        //2. Danh sách lịch sử giao dịch.
        public void ListHistory()
        {
            Console.WriteLine("2");
        }
        //3. Tìm kiếm người dùng theo tên.
        public void SreachUserName()
        {
            Console.WriteLine("3");
        }
        //4. Tìm kiếm người dùng theo số tài khoản.
        public void SreachAccountNumber()
        {
            Console.WriteLine("4");
        }
        //5. Tìm kiếm người dùng theo số điện thoại.
        public void SreachPhoneNumber()
        {
            Console.WriteLine("5");
        }
        //7. Khoá và mở tài khoản người dùng.
        public void LockUser()
        {
            Console.WriteLine("7");
        }
        //8. Tìm kiếm lịch sử giao dịch theo số tài khoản.
        public void SreachHistoryAccountNumber()
        {
            Console.WriteLine("8");
        }
        //9. Thay đổi thông tin tài khoản.
        public void ChangeAccountInformation()
        {
            Console.WriteLine("9");
        }
        //10. Thay đổi thông tin mật khẩu.
        public void ResetPassword()
        {
            Console.WriteLine("10");
        }
    }
}