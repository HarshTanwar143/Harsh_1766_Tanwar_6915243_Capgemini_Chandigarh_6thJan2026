using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem
{
    public class SavingsAccount : BankAccount
    {
        private decimal interestRate;

        public SavingsAccount(string acctNum, string holder, decimal initialBal, decimal interestRate)
            : base(acctNum, holder, initialBal)
        {
            this.interestRate = interestRate;
        }


        public void ApplyInterest()
        {
            decimal interest = balance * (interestRate / 100);
            balance += interest;
            Console.WriteLine($"Interest of ${interest} applied. New Balance: ${balance}");
        }

        public override void DisplayAccountInfo()
        {
            Console.WriteLine("\n--- Savings Account Details ---");
            Console.WriteLine($"Account Number: {AccountNumber}"); 
            Console.WriteLine($"Balance:        ${balance}");      
            Console.WriteLine($"Interest Rate:  {interestRate}%");
        }
    }
}