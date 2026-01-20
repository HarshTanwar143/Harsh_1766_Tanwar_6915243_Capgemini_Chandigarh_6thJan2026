namespace BankManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BankAccount> bankAccounts = new List<BankAccount>();

            SavingsAccount saver = new SavingsAccount("SAV001", "Harsh", 1000m, 5.0m);
            CheckingAccount checker = new CheckingAccount("CHK002", "Rahul", 500m, 200m);
            BusinessAccount business = new BusinessAccount("BUS003", "Tech Corp", 5000m);

            bankAccounts.Add(saver);
            bankAccounts.Add(checker);
            bankAccounts.Add(business);

            Console.WriteLine("=== BANK MANAGEMENT SYSTEM ===\n");


            // Savings 
            saver.DisplayAccountInfo();
            saver.Deposit(200);
            saver.ApplyInterest();

            // Checking
            checker.DisplayAccountInfo();
            checker.Withdraw(600); 

            // Business
            business.DisplayAccountInfo();
            business.Withdraw(1000);


            Console.WriteLine("\n\n=== FINAL ACCOUNT SUMMARIES ===");
            foreach (var acc in bankAccounts)
            {
                acc.DisplayAccountInfo();
            }

            Console.ReadLine();
        }
    }
}