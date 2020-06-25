using System;
using spring_hero_bank.Controller;
using spring_hero_bank.Entity;

namespace spring_hero_bank.View
{
    public class GuestMenu
    {
        public static void GenerateGuestMenu()
        {
            while (true)
            {
                GuestController guestController = new GuestController();
                Console.Clear();
                Console.WriteLine("--- Ngân hàng Spring Hero Bank ---");
                Console.WriteLine("1. Đăng ký tài khoản.");
                Console.WriteLine("2. Đăng nhập hệ thống.");
                Console.WriteLine("3. Thoát.");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Nhập lựa chọn của bạn (1,2,3): ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        if (guestController.Register())
                        {
                            Console.WriteLine("Đăng ký tài khoản thành công!");   
                        }
                        else
                        {
                            Console.WriteLine("Đăng ký tài khoản thất bại vui lòng thử lại!");
                        }
                        break;
                    case 2:
                        var account = guestController.Login();
                        if (account == null)
                        {
                            Console.WriteLine("Đăng nhập thất bại!");
                            break;
                        }
                        Program.currentLogin = account;
                        if ((int)Program.currentLogin.Role == 1)
                        {
                            UserMenu.GenerateGuestMenu();
                        }
                        else
                        {
                            AdminMenu.GenerateAdminMenu();
                        }
                        break;
                    case 3:
                        Console.WriteLine("Cảm ơn quý khách đã sử dụng dịch vụ của chúng tôi!");
                        break;
                    default:
                        Console.WriteLine("Vui lòng nhập lựa chọn từ 1-3.");
                        break;
                }

                Console.ReadLine();
                if (choice == 3)
                {
                    break;
                }
            }
        }
    }
}