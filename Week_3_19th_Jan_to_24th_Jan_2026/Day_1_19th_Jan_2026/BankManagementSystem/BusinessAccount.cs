using System;
using System.Collections.Generic;
using System.Text;


namespace BankManagementSystem
{
    public class BusinessAccount : BankAccount
    {
        private decimal transactionFee = 5.0m; 

        public BusinessAccount(string acctNum, string holder, decimal initialBal)
            : base(acctNum, holder, initialBal)
        {
        }

        public override void Withdraw(decimal amount)
        {
            decimal totalDeduction = amount + transactionFee;

            if (amount > 0 && totalDeduction <= balance)
            {
                balance -= totalDeduction;
                Console.WriteLine($"Withdrew ${amount} (Fee: ${transactionFee}). Remaining Balance: ${balance}");
            }
            else
            {
                Console.WriteLine("Insufficient funds to cover withdrawal + transaction fee.");
            }
        }
    }
}
