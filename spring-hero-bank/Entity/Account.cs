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
        public AccountSHBRole Role { get; set; } // 1. guest || 2. admin
        public string FullName { get; set; }
        public string Email { get; set; }
        public AccountSHBStatus Status { get; set; } // 1. active || 2. lock || -1. delete

        public override string ToString()
        {
            return $"Account : Role = {(AccountSHBRole) Role}";
        }
    }
    
    

    public enum AccountSHBRole
    {
        GUEST = 1, ADMIN = 2
    }

    public enum AccountSHBStatus
    {
        ACTIVE = 1, LOCK = 2, DELETE = -1
    }
}