using System;
using spring_hero_bank.Controller;
using spring_hero_bank.Entity;

namespace spring_hero_bank
{
    internal class Program
    {
        private static Account currentLogin;
        public static void Main(string[] args)
        {
            GuestController controller = new GuestController();
            while (true)
            {
                Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                Console.WriteLine("1. Đăng ký tài khoản.");
                Console.WriteLine("2. Đăng nhập hệ thống.");
                Console.WriteLine("3. Thoát.");
                Console.WriteLine("——————————————————-");
                Console.WriteLine("Nhập lựa chọn của bạn (1,2,3):");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        
                        break;
                    case 2:
                        // var account = controller.Login();
                        // if (account == null)
                        // {
                        //     Console.WriteLine("Login fails!");
                        //     return;
                        // }
                        // currentLogin = account;
                        // Console.WriteLine($"Login success! Welcome back {currentLogin.FullName}");
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }

            if (currentLogin.Role == 1)
            {
                Console.WriteLine("Admin menu.");
            }
            else
            {
                Console.WriteLine("User menu.");
            }
        }
    }
}