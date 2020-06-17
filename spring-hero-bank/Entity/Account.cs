namespace spring_hero_bank.Entity
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Salt { get; set; }
        public AccountRole Role { get; set; } // 1. guest || 2. admin
        public string FullName { get; set; }
        public string Email { get; set; }
        public AccountStatus Status { get; set; } // 1. active || 2. lock || -1. delete

    }
    
    public enum AccountRole
    {
        GUEST = 1, ADMIN = 2
    }

    public enum AccountStatus
    {
        ACTIVE = 1, LOCK = 2, DELETE = -1
    }
}