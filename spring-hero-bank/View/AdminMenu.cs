using System;
using System.Text;
using spring_hero_bank.Controller;

namespace spring_hero_bank.View
{
    public class AdminMenu
    {
        public static void StartAdminMenu()
        {
            var controller = new AdminController();
            var account_controller = new AccountController();
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                    Console.WriteLine("Chào mừng Admin quay trở lại. Vui lòng chọn thao tác.");
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
                            controller.ListUser();
                            break;
                        case 2:
                            controller.ListHistory();
                            break;
                        case 3:
                            controller.SreachUserName();
                            break;
                        case 4:
                            controller.SreachAccountNumber();
                            break;
                        case 5:
                            controller.SreachPhoneNumber();
                            break;
                        case 6:
                            account_controller.Register();
                            break;
                        case 7:
                            controller.LockUser();
                            break;
                        case 8:
                            controller.SreachHistoryAccountNumber();
                            break;
                        case 9:
                            controller.ChangeAccountInformation();
                            break;
                        case 10:
                            controller.ResetPassword();
                            break;
                        case 11:
                            Console.WriteLine("Hẹn gặp lại.");
                            break;
                    }
                    Console.ReadLine();
                    if (choice == 11)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
        }
    }
}