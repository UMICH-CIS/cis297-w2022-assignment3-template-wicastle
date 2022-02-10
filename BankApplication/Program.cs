
/*
 * William Castle
 * CIS 297
 * Assigment 3
 * Banking Application
 * Febuary 9, 2022
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    /// <summary>
    /// Account is base class to all other accounts and holds balance
    /// has methods credit and depit for chaging value of balance
    /// balance can never be below 0
    /// </summary>
    public class Account
    {
        private decimal balance;

        //constuctor
        public Account(decimal x = 0)
        {
            if(x < 0)
            {
                throw new ArgumentOutOfRangeException("Balance",x,"Balance must be >= to 0");
            }
            balance = x;
        }

        //get balance
        public decimal Balance
        {
            get
            {
                return balance;
            }
        }

        //deposit money
        public virtual void credit(decimal x)
        {
            Console.WriteLine("Current Balance: $" + Balance);
            balance += x;
            Console.WriteLine("Deposit Amount: $" + x);
            Console.WriteLine("Current Balance: $" + balance + "\n");
        }

        //widthdraw money
        public virtual bool debit(decimal x)
        {
            if ((balance - x) >= 0)
            {
                Console.WriteLine("Current Balance: $" + Balance);
                balance -= x;
                Console.WriteLine("Widthdrawn: $" + x);
                Console.WriteLine("Current Balance: $" + balance + "\n");

                return true;
            }
            else
            {
                Console.WriteLine("Debit amount exceeded account balance");
                Console.WriteLine("Current Balance: $" + balance + "\n");

                return false;
            } 
        }
    }

    /// <summary>
    /// Savings account is child of account and adds intrest
    /// credit and debit are the same
    /// calculating intrest returns the intrest due which it would then be credited to the balance
    /// </summary>
    public class SavingsAccount : Account
    {
        private decimal intrest;

        public SavingsAccount(decimal x = 0, decimal percent = 0) : base(x)
        {
            intrest = percent;
        }

        public decimal calculateIntrest()
        {
            return intrest * Balance;
        }
    }

    /// <summary>
    /// Checkings account adds fee which is applied whenever a transaction is made
    /// fee is calculated with the transaction so balance will never go below 0
    /// overrides credit and debit to add a fee to the method
    /// </summary>
    public class CheckingAccount : Account
    {
        private decimal fee;

        public CheckingAccount(decimal x = 0, decimal f = 0) : base(x)
        {
            fee = f;
        }

        public override void credit(decimal x)
        {
            base.credit(x);
            Console.WriteLine("!Fee!");
            base.debit(fee);
            Console.WriteLine("Current Balance: $" + Balance + "\n");
        }

        //widthdraw money
        public override bool debit(decimal x)
        {
            if ((Balance - (x + fee)) >= 0)
            {
                Console.WriteLine("!Fee!");
                base.debit(fee); 
                base.debit(x);

                return true;
            }
            else
            {
                Console.WriteLine("Debit + Fee amount exceeded account balance");
                Console.WriteLine("Current Balance: $" + Balance + "\n");

                return false;
            }
        }

    }

    public class AccountTest
    {
        public static void Main(string[] args)
        {
            // create array of accounts
            Account[] accounts = new Account[4];

            // initialize array with Accounts
            accounts[0] = new SavingsAccount(25M, .03M);
            accounts[1] = new CheckingAccount(80M, 1M);
            accounts[2] = new SavingsAccount(200M, .015M);
            accounts[3] = new CheckingAccount(400M, .5M);

            // loop through array, prompting user for debit and credit amounts
            for (int i = 0; i < accounts.Length; i++)
            {
                Console.WriteLine($"Account {i + 1} balance: {accounts[i].Balance:C}");

                Console.Write($"\nEnter an amount to withdraw from Account {i + 1}: ");
                decimal withdrawalAmount = decimal.Parse(Console.ReadLine());

                accounts[i].debit(withdrawalAmount); // attempt to debit

                Console.Write($"\nEnter an amount to deposit into Account {i + 1}: ");
                decimal depositAmount = decimal.Parse(Console.ReadLine());

                // credit amount to Account
                accounts[i].credit(depositAmount);

                // if Account is a SavingsAccount, calculate and add interest
                if (accounts[i] is SavingsAccount)
                {
                    // downcast
                    SavingsAccount currentAccount = (SavingsAccount)accounts[i];

                    decimal interestEarned = currentAccount.calculateIntrest();
                    Console.WriteLine($"Adding {interestEarned:C} interest to Account {i + 1} (a SavingsAccount)");
                    currentAccount.credit(interestEarned);
                }

                Console.WriteLine($"\nUpdated Account {i + 1} balance: {accounts[i].Balance:C}\n\n");
            }
        }
    }


    /*
    /// <summary>
    /// driver of program, holds main looping function for app
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            SavingsAccount savings = new SavingsAccount(1000.0m, 0.01m);
            CheckingAccount checking = new CheckingAccount(1000.0m, 0.5m);

            var accounts = new List<Account>() { savings, checking };

            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Enter Command:");
                Console.WriteLine("-------------------");
                Console.WriteLine("1) Savings Account");
                Console.WriteLine("2) Checkings Account");
                Console.WriteLine("3) Proccess Accounts");
                Console.WriteLine("4) Quit App");
                Console.WriteLine("-------------------");
                Console.Write("Command: ");
                int senti = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (senti)
                {
                    //savings
                    case 1:
                        bool loop_save = true;
                        while (loop_save)
                        {
                            Console.WriteLine("-------------------");
                            Console.WriteLine("SAVINGS ACCOUNT");
                            Console.WriteLine("-------------------");
                            Console.WriteLine("Enter Command:");
                            Console.WriteLine("-------------------");
                            Console.WriteLine("1) Deposit");
                            Console.WriteLine("2) Widthdraw");
                            Console.WriteLine("3) Back");
                            Console.WriteLine("-------------------");
                            int senti_save = int.Parse(Console.ReadLine());
                            Console.Clear();

                            switch (senti_save)
                            {
                                //Deposit
                                case 1:
                                    Console.WriteLine("Enter Ammount to Deposit");
                                    Console.Write("$: ");
                                    decimal d = decimal.Parse(Console.ReadLine());
                                    accounts[0].credit(d);
                                    break;
                                //widthdraw
                                case 2:
                                    Console.WriteLine("Enter Ammount to Widthdraw");
                                    Console.Write("$: ");
                                    decimal w = decimal.Parse(Console.ReadLine());
                                    accounts[0].debit(w);
                                    break;
                                //quit
                                case 3:
                                    loop_save = false;
                                    break;
                            }
                        }
                        break;
                    //checkings
                    case 2:
                        bool loop_checking = true;
                        while (loop_checking)
                        {
                            Console.WriteLine("-------------------");
                            Console.WriteLine("CHECKINGS ACCOUNT");
                            Console.WriteLine("-------------------");
                            Console.WriteLine("Enter Command:");
                            Console.WriteLine("-------------------");
                            Console.WriteLine("1) Deposit");
                            Console.WriteLine("2) Widthdraw");
                            Console.WriteLine("3) Back");
                            Console.WriteLine("-------------------");
                            int senti_check = int.Parse(Console.ReadLine());
                            Console.Clear();

                            switch (senti_check)
                            {
                                //Deposit
                                case 1:
                                    Console.WriteLine("Enter Ammount to Deposit");
                                    Console.Write("$: ");
                                    decimal d = decimal.Parse(Console.ReadLine());
                                    accounts[1].credit(d);
                                    break;
                                //widthdraw
                                case 2:
                                    Console.WriteLine("Enter Ammount to Widthdraw");
                                    Console.Write("$: ");
                                    decimal w = decimal.Parse(Console.ReadLine());
                                    accounts[1].debit(w);
                                    break;
                                //quit
                                case 3:
                                    loop_checking = false;
                                    break;
                            }
                        }
                        break;
                    //Process intrest
                    case 3:
                        foreach(var current in accounts)
                        {
                            if(current is SavingsAccount)
                            {
                                var save = (SavingsAccount)current;
                                current.credit(save.calculateIntrest());

                                Console.WriteLine("Savings Account: $" + save.Balance);
                            }
                            else
                            {
                                Console.WriteLine("Checkings Account: $" + current.Balance);
                            }
                        }
                        break;
                    //quit
                    case 4:
                        loop = false;
                        break;
                }
            }
        }
    }
    */
}
