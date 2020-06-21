using System;
using System.Text;
using spring_hero_bank.Controller;

namespace spring_hero_bank.View
{
    public class GuestMenu
    {
        public static void StartGuestMenu()
        {
            var controller = new AdminController();
            var guest_controller = new GuestController();
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                Console.WriteLine("Chào mừng quay trở lại. Vui lòng chọn thao tác.");
                Console.WriteLine("1. Gửi tiền.");
                Console.WriteLine("2. Rút tiền.");
                Console.WriteLine("3. Chuyển khoản.");
                Console.WriteLine("4. Truy vấn số dư.");
                Console.WriteLine("5. Thay đổi thông tin cá nhân.");
                Console.WriteLine("6. Thay đổi thông tin mật khẩu.");
                Console.WriteLine("7. Truy vấn lịch sử giao dịch.");
                Console.WriteLine("8. Thoát.");
                Console.WriteLine("——————————————————-");
                Console.WriteLine("Nhập lựa chọn của bạn (Từ 1 đến 8): ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        guest_controller.SendMoney();
                        break;
                    case 2:
                        guest_controller.Withdraw();
                        break;
                    case 3:
                        guest_controller.Transfer();
                        break;
                    case 4:
                        guest_controller.Deposit();
                        break;
                    case 5:
                        controller.ChangeAccountInformation();
                        break;
                    case 6:
                        controller.ResetPassword();
                        break;
                    case 7:
                        controller.SreachHistoryAccountNumber();
                        break;
                    case 8:
                        Console.WriteLine("Hẹn gặp lại.");
                        break;
                }

                Console.ReadLine();
                if (choice == 8)
                {
                    break;
                }
            }
        }
    }
}