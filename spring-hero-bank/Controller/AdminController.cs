using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using spring_hero_bank.Entity;
using spring_hero_bank.Helper;
using spring_hero_bank.View;

namespace spring_hero_bank.Controller
{
    public class AdminController
    {
        //1. danh sách người dùng
        public List<Account> ListUser()
        {
            List<Account> listAccount = new List<Account>();
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var stringCmdGetAccount = $"select * from accounts";
            var cmdGetAccount = new MySqlCommand(stringCmdGetAccount, cnn);
            var readerGetAccount = cmdGetAccount.ExecuteReader();
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                if (readerGetAccount.Read())
                {
                    var account = new Account()
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
                else
                {
                    break;
                }
                
            }
            cnn.Close();
            ListUsers.ListAllUser(listAccount);
            return listAccount;
            
        }
        //2. Danh sách lịch sử giao dịch.
        public List<SHBTransaction> ListHistory()
        {
            List<SHBTransaction> listHistory = new List<SHBTransaction>();
            var cnn = ConnectionHelpers.GetConnection();
            cnn.Open();
            var stringCmdGetAccount = $"select * from transactions";
            var cmd = new MySqlCommand(stringCmdGetAccount, cnn);
            var getListHistory = cmd.ExecuteReader();
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                if (getListHistory.Read())
                {
                    var history = new SHBTransaction()
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
                    listHistory.Add(history);
                }
                else
                {
                    break;
                }
                
            }
            cnn.Close();
            ListUsers.ListAllHistory(listHistory);
            return listHistory;
        }
        //3. Tìm kiếm người dùng theo tên.
        public void SreachUserName()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Vui lòng nhập userName cần tìm kiếm: ");
            var sreachUserName = Console.ReadLine();
            var cnnSreach = ConnectionHelpers.GetConnection();
            cnnSreach.Open();
            var stringCmdGetUserName = $"select * from accounts where userName = '{sreachUserName}'";
            var cmdGetUserName = new MySqlCommand(stringCmdGetUserName, cnnSreach);
            var readerGetUserName = cmdGetUserName.ExecuteReader();
            if (readerGetUserName.Read())
            {
                Console.WriteLine("AccountNumber   " + "Username   " + "Email   " + "Số điện thoại   " + "Tên đầy đủ   " + "Quyền sử dụng   " + "Trạng thái   ");
                Console.WriteLine(readerGetUserName.GetString("accountNumber") + "   " + readerGetUserName.GetString("userName") + "   " +
                                  readerGetUserName.GetString("email") + "   " + readerGetUserName.GetString("phoneNumber") + "   " +
                                  readerGetUserName.GetString("fullName") + "   " + readerGetUserName.GetString("role") + "   " +
                                  readerGetUserName.GetString("status"));
            }
            else
            {
                Console.WriteLine("Không tìm thấy tài khoản có UserName: " + sreachUserName);
            }
            Console.WriteLine("Bạn có muốn tiếp tục tìm kiếm người dùng theo tên?");
            Console.WriteLine("1. Đồng ý.");
            Console.WriteLine("2. quay lại Menu chính.");
            try
            {
                var choice = int.Parse(Console.ReadLine());
                while (true)
                {
                    if (choice == 1)
                    {
                        SreachUserName();
                        break;
                    }

                    if (choice == 2)
                    {
                        GeneratorMenu.GenerateMenu();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            cnnSreach.Close();
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