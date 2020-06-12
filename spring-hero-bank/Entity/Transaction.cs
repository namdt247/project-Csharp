using System;

namespace spring_hero_bank.Entity
{
    public class Transaction
    {
        public string TransactionCode { get; set; }
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public TransactionType Type { get; set; } // 1. withdraw || 2. deposit || 3. tranfer
        public double Amount { get; set; }
        public double Fee { get; set; }
        public string Message { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TransactionStatus Status { get; set; } // 1. pending || 2. done || 0. fails
    }

    public enum TransactionStatus
    {
        PENDING = 1, DONE = 2, FAILS = 0
    }

    public enum TransactionType
    {
        WITHDRAW = 1, DEPOSIT = 2, TRANFER = 3
    }
}