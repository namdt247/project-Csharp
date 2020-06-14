using System;
using MySql.Data.MySqlClient;

namespace spring_hero_bank.Helper
{
    public class ConnectionHelpers
    {
        private const string DatabaseServer = "127.0.0.1";
        private const string DatabaseName = "spring-hero-bank";
        private const string DatabaseUid = "root";
        private const string DatabasePassword = "";
        private static MySqlConnection _connection;

        public static MySqlConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection(
                    $"SERVER={DatabaseServer};DATABASE={DatabaseName};UID={DatabaseUid};PASSWORD={DatabasePassword}");
                Console.WriteLine("Ket noi thanh cong");
            }

            return _connection;
        }
    }
}