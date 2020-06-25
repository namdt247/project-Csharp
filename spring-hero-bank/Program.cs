
﻿using System;
 using System.Text;
 using spring_hero_bank.Controller;
 using spring_hero_bank.Entity;
using spring_hero_bank.View;

namespace spring_hero_bank
{
    internal class Program
    {
        public static Account currentLogin;
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            GuestMenu.GenerateGuestMenu();
        }
    }
}