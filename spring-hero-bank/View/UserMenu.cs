using System;
using spring_hero_bank.Controller;
using spring_hero_bank.Helper;

namespace spring_hero_bank.View
{
    public class UserMenu
    {
        public static void GenerateGuestMenu()
        {
            UserController userController = new UserController();
            ShowPageHelper showPageHelper = new ShowPageHelper();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---------- Ngân hàng Spring Hero Bank ----------");
                Console.WriteLine($"Chào mừng \"{Program.currentLogin.Username}\" quay trở lại. Vui lòng chọn thao tác.");
                Console.WriteLine("1. Gửi tiền.");
                Console.WriteLine("2. Rút tiền.");
                Console.WriteLine("3. Chuyển khoản.");
                Console.WriteLine("4. Truy vấn số dư.");
                Console.WriteLine("5. Thay đổi thông tin cá nhân.");
                Console.WriteLine("6. Thay đổi thông tin mật khẩu.");
                Console.WriteLine("7. Truy vấn lịch sử giao dịch.");
                Console.WriteLine("8. Thoát.");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Nhập lựa chọn của bạn (Từ 1 đến 8): ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        userController.DepositController();
                        break;
                    case 2:
                        userController.WithdrawController();
                        break;
                    case 3:
                        userController.TransferController();
                        break;
                    case 4:
                        userController.BalanceQueryController();
                        break;
                    case 5:
                        userController.ChangeUserController();
                        break;
                    case 6:
                        userController.ChangePasswordController();
                        break;
                    case 7:
                        var listTransactions = userController.TransactionHistoryController();
                        if (listTransactions == null || listTransactions.Count == 0)
                        {
                            Console.WriteLine("Không có lịch sử giao dịch");
                        }
                        else
                        {
                            showPageHelper.ListAllTransactionHistory(listTransactions);
                        }
                        break;
                    case 8:
                        Console.WriteLine("Hẹn gặp lại.");
                        return;
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