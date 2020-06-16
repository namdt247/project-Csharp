using System;
using spring_hero_bank.Controller;
using spring_hero_bank.Entity;

namespace spring_hero_bank.View
{
    public class GeneratorMenu
    {
        
        public static void GenerateMenu()
        {
            var controller = new AccountController();
            var account = new Account();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                Console.WriteLine("1. Đăng ký tài khoản.");
                Console.WriteLine("2. Đăng nhập hệ thống.");
                Console.WriteLine("3. Thoát.");
                Console.WriteLine("——————————————————-");
                Console.WriteLine("Nhập lựa chọn của bạn (1,2,3): ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        controller.Register();
                        break;
                    case 2:
                        controller.Login();
                        break;
                    case 3:
                        Console.WriteLine("Cảm ơn quý khách đã sử dụng dịch vụ của chúng tôi!");
                        break;
                }
                
                Console.ReadLine();
                if (choice == 3)
                {
                    break;
                }
            }

            if ((int)account.Role == 1)
            {
                GuestMenu.StartGuestMenu();
            } else if ((int)account.Role == 2)
            {
                AdminMenu.StartAdminMenu();
            } 
        }
    }
}