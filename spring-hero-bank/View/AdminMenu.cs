using System;
using System.Collections.Generic;
using spring_hero_bank.Controller;
using spring_hero_bank.Helper;

namespace spring_hero_bank.View
{
    public class AdminMenu
    {
        public static void GenerateAdminMenu()
        {
            var adminController = new AdminController();
            ShowPageHelper showPageHelper = new ShowPageHelper();
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("------------ Ngân hàng Spring Hero Bank ------------");
                Console.WriteLine($"Chào mừng Admin \"{Program.currentLogin.Username}\" quay trở lại. Vui lòng chọn thao tác.");
                Console.WriteLine("1. Danh sách người dùng.");
                Console.WriteLine("2. Danh sách lịch sử giao dịch.");
                Console.WriteLine("3. Tìm kiếm người dùng theo tên.");
                Console.WriteLine("4. Tìm kiếm người dùng theo số tài khoản.");
                Console.WriteLine("5. Tìm kiếm người dùng theo số điện thoại.");
                Console.WriteLine("6. Thêm người dùng mới.");
                Console.WriteLine("7. Khoá và mở tài khoản người dùng.");
                Console.WriteLine("8. Tìm kiếm lịch sử giao dịch theo số tài khoản.");
                Console.WriteLine("9. Thay đổi thông tin tài khoản.");
                Console.WriteLine("10. Thay đổi thông tin mật khẩu.");
                Console.WriteLine("11. Thoát.");
                Console.WriteLine("——————————————————-");
                Console.WriteLine("Nhập lựa chọn của bạn (Từ 1 đến 11): ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        var listUser = adminController.ListUserController();
                        if (listUser == null || listUser.Count == 0)
                        {
                            Console.WriteLine("Không có danh sách tài ");
                        }
                        else
                        {
                            showPageHelper.ListAllUser(listUser);
                        }
                        break;
                    case 2:
                        var listTran = adminController.ListTransactionController();
                        if (listTran == null || listTran.Count == 0)
                        {
                            Console.WriteLine("Không có danh sách giao dịch");
                        }
                        else
                        {
                            showPageHelper.ListAllTransactionHistory(listTran);
                        }
                        break;
                    case 3:
                        var accountByName = adminController.GetAccountByName();
                        if (accountByName == null)
                        {
                            Console.WriteLine("Không tìm thấy tài khoản phù hợp");
                        }
                        else
                        {
                            Console.WriteLine(accountByName.ToString());
                        }
                        break;
                    case 4:
                        var accountByAccNumber = adminController.GetAccountByAccountNumber();
                        if (accountByAccNumber == null)
                        {
                            Console.WriteLine("Không tìm thấy tài khoản phù hợp");
                        }
                        else
                        {
                            Console.WriteLine(accountByAccNumber.ToString());
                        }
                        break;
                    case 5:
                        var accountByPhone = adminController.GetAccountByPhone();
                        if (accountByPhone == null)
                        {
                            Console.WriteLine("Không tìm thấy tài khoản phù hợp");
                        }
                        else
                        {
                            Console.WriteLine(accountByPhone.ToString());
                        }
                        break;
                    case 6:
                        if (adminController.AdminRegister())
                        {
                            Console.WriteLine("Thêm tài khoản thành công!");
                        }
                        else
                        {
                            Console.WriteLine("Thêm tài khoản không thành công vui lòng thử lại!");
                        }
                        break;
                    case 7:
                        if (adminController.ChangeUserStatus())
                        {
                            Console.WriteLine("Thao tác thành công!");
                        }
                        break;
                    case 8:
                        var listTransaction = adminController.GetTransactionByAccountNumber();
                        foreach (var transaction in listTransaction)
                        {
                            Console.WriteLine(transaction.ToString());
                        }
                        break;
                    case 9:
                        adminController.EditAccount();
                        break;
                    case 10:
                        adminController.PasswordChange();
                        break;
                    case 11:
                        Console.WriteLine("Hẹn gặp lại.");
                        return;
                }
                Console.ReadLine();
                if (choice == 11)
                {
                    break;
                }
            }
        }
    }
}