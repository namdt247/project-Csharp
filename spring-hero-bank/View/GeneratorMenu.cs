using System;

namespace spring_hero_bank.View
{
    public class GeneratorMenu
    {
        public static void MenuUser()
        {
            Console.WriteLine("---Ngân hàng Spring Hero Bank ---");
            Console.WriteLine("1. Đăng ký tài khoản.");
            Console.WriteLine("2. Đăng nhập hệ thống.");
            Console.WriteLine("3. Thoát.");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Nhập lựa chọn của bạn (1,2,3): ");
        }

        public static void MenuGuest()
        {
            Console.WriteLine("---Ngân hàng Spring Hero Bank ---");
            Console.WriteLine("Chào mừng \"Xuân Hùng\" quay trở lại. Vui lòng chọn thao tác.");
            Console.WriteLine("1. Gửi tiền.");
            Console.WriteLine("2. Rút tiền.");
            Console.WriteLine("3. Chuyển khoản.");
            Console.WriteLine("4. Truy vấn số dư.");
            Console.WriteLine("5. Thay đổi thông tin cá nhân.");
            Console.WriteLine("6. Thay đổi thông tin mật khẩu.");
            Console.WriteLine("7. Truy vấn lịch sử giao dịch.");
            Console.WriteLine("8. Thoát.");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Nhập lựa chọn của bạn (Từ 1 đến 8): ");
        }

        public static void AdminMenu()
        {
            Console.WriteLine("---Ngân hàng Spring Hero Bank ---");
            Console.WriteLine("Chào mừng Admin \"Xuân Hùng\" quay trở lại. Vui lòng chọn thao tác.");
            Console.WriteLine("1. Danh sách người dùng.");
            Console.WriteLine("2. Danh sách lịch sử giao dịch.");
            Console.WriteLine("3. Tìm kiếm người dùng theo tên.");
            Console.WriteLine("4. Tìm kiếm người dùng theo số tài khoản.");
            Console.WriteLine("5. Tìm kiếm người dùng theo số điện thoại.");
            Console.WriteLine("6. Thêm người dùng mới.");
            Console.WriteLine("7. Khoá và mở tài khoản người dùng.");
            Console.WriteLine("8. Tìm kiếm lịch sử giao dịch theo số tài khoản.");
            Console.WriteLine("9. Thay đổi thông tin tài khoản.");
            Console.WriteLine("10. Thay đổi thông tin mật khẩu.");
            Console.WriteLine("11. Thoát.");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Nhập lựa chọn của bạn (Từ 1 đến 11): ");
        }
    }
}