using System;
using spring_hero_bank.Controller;
using spring_hero_bank.Entity;
using spring_hero_bank.View;

namespace spring_hero_bank
{
    internal class Program
    {
        public static Account currentNumber;
        public static void Main(string[] args)
        {
            GeneratorMenu.GenerateMenu();
        }
    }
}