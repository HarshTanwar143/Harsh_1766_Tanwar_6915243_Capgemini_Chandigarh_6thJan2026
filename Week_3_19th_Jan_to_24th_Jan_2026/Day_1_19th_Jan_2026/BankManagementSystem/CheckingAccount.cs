using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem
{
    public class CheckingAccount : BankAccount
    {
        private decimal overdraftLimit;

        public CheckingAccount(string acctNum, string holder, decimal initialBal, decimal overdraftLimit)
            : base(acctNum, holder, initialBal)
        {
            this.overdraftLimit = overdraftLimit;
        }


        public override void Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= (balance + overdraftLimit))
            {
                balance -= amount;
                Console.WriteLine($"Withdrew ${amount} from Checking. Remaining Balance: ${balance}");
            }
            else
            {
                Console.WriteLine($"Transaction Failed. Exceeds overdraft limit of ${overdraftLimit}.");
            }
        }

        public override void DisplayAccountInfo()
        {
            Console.WriteLine("\n--- Checking Account Details ---");
            Console.WriteLine($"Account Number:  {AccountNumber}");
            Console.WriteLine($"Balance:         ${balance}");
            Console.WriteLine($"Overdraft Limit: ${overdraftLimit}");
        }
    }
}
