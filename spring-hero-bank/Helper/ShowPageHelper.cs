using System;
using System.Collections.Generic;
using spring_hero_bank.Entity;
using spring_hero_bank.View;

namespace spring_hero_bank.Helper
{
    public class ShowPageHelper
    {
        public static int firtPage = 1;
        public static int currentPageIndex = 1;
        
        public void ListAllUser(List<Account> listAccount)
        {
            int pageSize = 10;
            var pageNumber = listAccount.Count < pageSize ? 0 : listAccount.Count / pageSize + 1;
            if (firtPage == 1)
            {
                Console.WriteLine("--- Danh sách tài khoản ---");
                Console.WriteLine("-----------------------------");
                // toString()
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
                        Console.WriteLine((i+1) + "   " + listAccount[i].AccountNumber + "   " + listAccount[i].Username + "   " +
                                          listAccount[i].Email + "   " + listAccount[i].PhoneNumber + "   " +
                                          listAccount[i].FullName + "   " + listAccount[i].Role + "   " +
                                          listAccount[i].Status);
                    }
                    Console.WriteLine("Số hàng trên trang: " + lengthPage + ", Số trang: 1/" + (pageNumber+1));
                }
            }
            else
            {
                Console.WriteLine("#   " + "AccountNumber   " + "Username   " + "Email   " + "Số điện thoại   " + "Tên đầy đủ   " + "Quyền sử dụng   " + "Trạng thái   ");
                if (currentPageIndex != pageNumber)
                {
                    for (int j = (currentPageIndex-1)*10; j < currentPageIndex*10; j++)
                    {
                        Console.WriteLine((j+1) + "   " + listAccount[j].AccountNumber + "   " + listAccount[j].Username + "   " + listAccount[j].Email + "   " + listAccount[j].PhoneNumber + "   " + listAccount[j].FullName + "   " + listAccount[j].Role + "   " + listAccount[j].Status);
                    }
                    Console.WriteLine("Số hàng trên trang: " + 10 + ", Số trang: "+ currentPageIndex +"/" + pageNumber);
                    Console.WriteLine("Vui lòng nhấp phím '>' để sang trang tiếp");
                    Console.WriteLine("Vui lòng nhấp phím '<' để quay lại trang trước");
                }
                else
                {
                    for (int j = (currentPageIndex-1)*10; j < listAccount.Count; j++)
                    {
                        Console.WriteLine((j+1) + "   " + listAccount[j].AccountNumber + "   " + listAccount[j].Username + "   " + listAccount[j].Email + "   " + listAccount[j].PhoneNumber + "   " + listAccount[j].FullName + "   " + listAccount[j].Role + "   " + listAccount[j].Status);
                    }
                    Console.WriteLine("Số hàng trên trang: " + (listAccount.Count - (currentPageIndex - 1)*10) + ", Số trang: "+ pageNumber +"/" + pageNumber);
                    Console.WriteLine("Vui lòng nhấp phím '>' để sang trang tiếp");
                    Console.WriteLine("Vui lòng nhấp phím '<' để quay lại trang trước");
                }
            }
            
            while (true)
            {
                var keyB = Console.ReadKey(true).Key;
                if (listAccount.Count > 10)
                {
                    if (keyB == ConsoleKey.RightArrow)
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
                    if (keyB == ConsoleKey.LeftArrow)
                    {

                        firtPage = 2;
                        currentPageIndex--;
                        if (currentPageIndex < 1)
                        {
                            currentPageIndex = pageNumber;
                        }
                        ListAllUser(listAccount);
                        break;

                    }
                }
                if (keyB == ConsoleKey.Escape)
                {
                    GuestMenu.GenerateGuestMenu();
                    break;
                }
            }
        }
        
        public void ListAllTransactionHistory(List<SHBTransaction> listHistory)
        {
            int pageSize = 10;
            var pageNumber = listHistory.Count < pageSize ? 0 : listHistory.Count / pageSize + 1;
            if (firtPage == 1)
            {
                Console.WriteLine("--- Lịch sử giao dịch ---");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("#   " + "TransactionCode   " + "SenderAccountNumber   " + "ReceiverAccountNumber   " + 
                                  "Type   " + "Amount   " + "Fee   " + "Message   " + "CreateAt   " + "UpdatedAt   " + "Status   ");
                var lengthPage = pageNumber > 0 ? currentPageIndex*10 : listHistory.Count;
                if (pageNumber > 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine((i+1) + "   "+ listHistory[i].TransactionCode + "   " + listHistory[i].SenderAccountNumber + "   " +
                                          listHistory[i].ReceiverAccountNumber + "   " + listHistory[i].Type + "   " +
                                          listHistory[i].Amount + "   " + listHistory[i].Fee + "   " + listHistory[i].Message + "   " +
                                          listHistory[i].CreateAt + "   " + listHistory[i].UpdatedAt + "   " + listHistory[i].Status);
                    }
                    Console.WriteLine("Số hàng trên trang: " + lengthPage + ", Số trang: 1/" + pageNumber);
                    Console.WriteLine("Vui lòng nhấp phím '>' để sang trang tiếp");
                    Console.WriteLine("Vui lòng nhấp phím '<' để quay lại trang trước");
                }
                else
                {
                    for (int i = 0; i < listHistory.Count; i++)
                    {
                        Console.WriteLine((i+1) + "   "+ listHistory[i].TransactionCode + "   " + listHistory[i].SenderAccountNumber + "   " +
                                          listHistory[i].ReceiverAccountNumber + "   " + listHistory[i].Type + "   " +
                                          listHistory[i].Amount + "   " + listHistory[i].Fee + "   " + listHistory[i].Message + "   " +
                                          listHistory[i].CreateAt + "   " + listHistory[i].UpdatedAt + "   " + listHistory[i].Status);
                    }
                    Console.WriteLine("Số hàng trên trang: " + lengthPage + ", Số trang: 1/" + (pageNumber+1));
                }
            }
            else
            {
                Console.WriteLine("#   " + "TransactionCode   " + "SenderAccountNumber   " + "ReceiverAccountNumber   " + 
                                  "Type   " + "Amount   " + "Fee   " + "Message   " + "CreateAt   " + "UpdatedAt   " + "Status   ");                if (currentPageIndex != pageNumber)
                {
                    for (int j = (currentPageIndex-1)*10; j < currentPageIndex*10; j++)
                    {
                        Console.WriteLine(j + "   "+ listHistory[j].TransactionCode + "   " + listHistory[j].SenderAccountNumber + "   " +
                                          listHistory[j].ReceiverAccountNumber + "   " + listHistory[j].Type + "   " +
                                          listHistory[j].Amount + "   " + listHistory[j].Fee + "   " + listHistory[j].Message + "   " +
                                          listHistory[j].CreateAt + "   " + listHistory[j].UpdatedAt + "   " + listHistory[j].Status);
                    }
                    Console.WriteLine("Số hàng trên trang: " + 10 + ", Số trang: "+ currentPageIndex +"/" + pageNumber);
                    Console.WriteLine("Vui lòng nhấp phím '>' để sang trang tiếp");
                    Console.WriteLine("Vui lòng nhấp phím '<' để quay lại trang trước");
                }
                else
                {
                    for (int j = (currentPageIndex-1)*10; j < listHistory.Count; j++)
                    {
                        Console.WriteLine(j + "   "+ listHistory[j].TransactionCode + "   " + listHistory[j].SenderAccountNumber + "   " +
                                          listHistory[j].ReceiverAccountNumber + "   " + listHistory[j].Type + "   " +
                                          listHistory[j].Amount + "   " + listHistory[j].Fee + "   " + listHistory[j].Message + "   " +
                                          listHistory[j].CreateAt + "   " + listHistory[j].UpdatedAt + "   " + listHistory[j].Status);
                    }
                    Console.WriteLine("Số hàng trên trang: " + (listHistory.Count - (currentPageIndex - 1)*10) + ", Số trang: "+ pageNumber +"/" + pageNumber);
                    Console.WriteLine("Vui lòng nhấp phím '>' để sang trang tiếp");
                    Console.WriteLine("Vui lòng nhấp phím '<' để quay lại trang trước");
                }
            }
            
            while (true)
            {
                var keyB = Console.ReadKey(true).Key;
                if (listHistory.Count > 10)
                {
                    if (keyB == ConsoleKey.RightArrow)
                    {
                        firtPage = 2;
                        currentPageIndex++;
                        if (currentPageIndex > pageNumber)
                        {
                            currentPageIndex = 1;
                        }
                        ListAllTransactionHistory(listHistory);
                        break;
                    }
                    if (keyB == ConsoleKey.LeftArrow)
                    {

                        firtPage = 2;
                        currentPageIndex--;
                        if (currentPageIndex < 1)
                        {
                            currentPageIndex = pageNumber;
                        }
                        ListAllTransactionHistory(listHistory);
                        break;

                    }
                }
                if (keyB == ConsoleKey.Escape)
                {
                    GuestMenu.GenerateGuestMenu();
                    break;
                }
            }
        }
    }
}
