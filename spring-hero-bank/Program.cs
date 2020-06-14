using System;
using spring_hero_bank.Controller;
using spring_hero_bank.Entity;
using spring_hero_bank.Helper;
using spring_hero_bank.View;

namespace spring_hero_bank
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            AccountController accountController = new AccountController();
            Account account = accountController.Login();
            Console.WriteLine(account.ToString());
        }
    }
}