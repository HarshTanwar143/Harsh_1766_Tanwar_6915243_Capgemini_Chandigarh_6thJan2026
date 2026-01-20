using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem
{
    public class BankAccount
    {
        private string accountNumber;
        private string accountHolder;
        protected decimal balance; 

        public BankAccount(string accountNumber, string accountHolder, decimal initialBalance)
        {
            this.accountNumber = accountNumber;
            this.accountHolder = accountHolder;
            this.balance = initialBalance;
        }

        public string AccountNumber
        {
            get 
            { 
                return accountNumber; 
            }
        }


        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                balance += amount;
                Console.WriteLine($"Deposited ${amount}. New Balance: ${balance}");
            }
            else
            {
                Console.WriteLine("Deposit amount must be positive.");
            }
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= balance)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew ${amount}. Remaining Balance: ${balance}");
            }
            else
            {
                Console.WriteLine("Insufficient funds or invalid amount.");
            }
        }

        public virtual void DisplayAccountInfo()
        {
            Console.WriteLine("\nACCOUNT DETAILS OF THE PERSON::");
            Console.WriteLine($"Account Type:   Base Account");
            Console.WriteLine($"Account Number: {accountNumber}");
            Console.WriteLine($"Holder Name:    {accountHolder}");
            Console.WriteLine($"Current Balance: ${balance}");
        }
    }
}