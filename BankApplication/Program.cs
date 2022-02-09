using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
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
        public void credit(decimal x)
        {
            Console.WriteLine("Current Balance: $" + Balance);
            balance += x;
            Console.WriteLine("Deposit Amount: $" + x);
            Console.WriteLine("Current Balance: $" + balance + "\n");
        }

        //widthdraw money
        public bool debit(decimal x)
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

    public class CheckingAccount : Account
    {
        private decimal fee;

        public CheckingAccount(decimal x = 0, decimal f = 0) : base(x)
        {
            fee = f;
        }

        public void credit(decimal x)
        {
            base.credit(x);
            Console.WriteLine("!Fee!");
            base.debit(fee);
            Console.WriteLine("Current Balance: $" + Balance + "\n");
        }

        //widthdraw money
        public bool debit(decimal x)
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


    class Program
    {
        static void Main(string[] args)
        {
            SavingsAccount savings = new SavingsAccount(1000.0m, 0.01m);

            savings.credit(10);
            savings.debit(10);
            savings.debit(1000000);


            CheckingAccount checking = new CheckingAccount(1000.0m, 0.5m);

            checking.credit(10);
            checking.debit(10);
            checking.debit(1000000);

            
            Console.WriteLine("END");

        }
    }
}
