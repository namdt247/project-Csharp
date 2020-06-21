using System;
using System.Collections.Generic;
using System.Text;
using spring_hero_bank.Controller;
using spring_hero_bank.Entity;

namespace spring_hero_bank.View
{
    public class ListUsers
    {
        public static int firtPage = 1;
        public static int currentPageIndex = 1;
        public static void ListAllUser(List<Account> listAccount)
        {
            int pageSize = 10;
            Console.OutputEncoding = Encoding.UTF8;
            var pageNumber = listAccount.Count % pageSize != 0 ? listAccount.Count / pageSize + 1 : listAccount.Count / pageSize;
            if (firtPage == 1)
            {
                Console.WriteLine("--- Danh sách tài khoản ---");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("#   " + "AccountNumber   " + "Username   " + "Email   " + "Số điện thoại   " + "Tên đầy đủ   " + "Quyền sử dụng   " + "Trạng thái   ");
                var lengthPage = pageNumber > 0 ? currentPageIndex*10 : listAccount.Count;
                if (pageNumber > 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine((i+1) + "   "+ listAccount[i].AccountNumber + "   " + listAccount[i].Username + "   " +
                                          listAccount[i].Email + "   " + listAccount[i].PhoneNumber + "   " +
                                          listAccount[i].FullName + "   " + listAccount[i].Role + "   " +
                                          listAccount[i].Status);
                    }
                    Console.WriteLine("Số hàng trên trang: " + lengthPage + ", Số trang: 1/" + pageNumber);
                    Console.WriteLine("Vui lòng nhấp phím '>' để sang trang tiếp");
                    Console.WriteLine("Vui lòng nhấp phím '<' để quay lại trang trước");
                }
                else
                {
                    for (int i = 0; i < listAccount.Count; i++)
                    {
                        Console.WriteLine(i + "   " + listAccount[i].AccountNumber + "   " + listAccount[i].Username + "   " +
                                          listAccount[i].Email + "   " + listAccount[i].PhoneNumber + "   " +
                                          listAccount[i].FullName + "   " + listAccount[i].Role + "   " +
                                          listAccount[i].Status);
                    }
                    Console.WriteLine("Số hàng trên trang: " + lengthPage + ", Số trang: 1/" + pageNumber);
                }
            }
            else
            {
                Console.WriteLine("#   " + "AccountNumber   " + "Username   " + "Email   " + "Số điện thoại   " + "Tên đầy đủ   " + "Quyền sử dụng   " + "Trạng thái   ");
                if (currentPageIndex != pageNumber)
                {
                    for (int j = (currentPageIndex-1)*10 + 1; j < currentPageIndex*10; j++)
                    {
                        Console.WriteLine((j+1) + "   " + listAccount[j].AccountNumber + "   " + listAccount[j].Username + "   " + listAccount[j].Email + "   " + listAccount[j].PhoneNumber + "   " + listAccount[j].FullName + "   " + listAccount[j].Role + "   " + listAccount[j].Status);
                    }
                    Console.WriteLine("Số hàng trên trang: " + 10 + ", Số trang: "+ currentPageIndex +"/" + pageNumber);
                    Console.WriteLine("Vui lòng nhấp phím '>' để sang trang tiếp");
                    Console.WriteLine("Vui lòng nhấp phím '<' để quay lại trang trước");
                }
                else
                {
                    for (int j = (currentPageIndex-1)*10 + 1; j < listAccount.Count; j++)
                    {
                        Console.WriteLine(j + "   " + listAccount[j].AccountNumber + "   " + listAccount[j].Username + "   " + listAccount[j].Email + "   " + listAccount[j].PhoneNumber + "   " + listAccount[j].FullName + "   " + listAccount[j].Role + "   " + listAccount[j].Status);
                    }
                    Console.WriteLine("Số hàng trên trang: " + (listAccount.Count - (currentPageIndex - 1)*10) + ", Số trang: "+ pageNumber +"/" + pageNumber);
                    Console.WriteLine("Vui lòng nhấp phím '>' để sang trang tiếp");
                    Console.WriteLine("Vui lòng nhấp phím '<' để quay lại trang trước");
                }
            }
            
            if (listAccount.Count > 10)
            {
                var keyB = Console.ReadKey(true).Key;
                var returnLoop = 0;
                while (true)
                {
                    if (keyB == ConsoleKey.OemPeriod)
                    {
                        firtPage = 2;
                        currentPageIndex++;
                        if (currentPageIndex > pageNumber)
                        {
                            currentPageIndex = 1;
                        }
                        ListAllUser(listAccount);
                        break;
                    }
                    if (keyB == ConsoleKey.OemComma)
                    {
                        Console.WriteLine("5");
                        firtPage = 2;
                        break;
                        
                    }
                    if (keyB == ConsoleKey.Escape)
                    {
                        GeneratorMenu.GenerateMenu();
                        break;
                    }
                }
            }
        }
    }
}