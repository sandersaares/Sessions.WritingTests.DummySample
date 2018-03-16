using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DummySample
{
    class Program
    {
        const string AccountFilename = "BankSafe.secure";

        static int Main(string[] args)
        {
            // Usage:
            // * withdraw 1234 - withdraw the amount.
            // * deposit 1234 - deposit the amount.
            // * anything else - just display balance.

            Console.WriteLine("Welcome to Cloud Bank!");

            BankAccount account = null;

            // If the file exists on disk, load it.
            if (File.Exists(AccountFilename))
                using (var file = File.OpenRead(AccountFilename))
                    account = new BinaryFormatter().Deserialize(file) as BankAccount;

            // Otherwise, we start with empty bank account.
            if (account == null)
                account = new BankAccount();

            try
            {
                if (args.Length == 2)
                {
                    switch (args[0])
                    {
                        case "withdraw":
                            {
                                var amount = int.Parse(args[1], CultureInfo.InvariantCulture);
                                account.Withdraw(amount);

                                Console.WriteLine($"Withdrew {amount:C}");
                            }

                            break;
                        case "deposit":
                            {
                                var amount = int.Parse(args[1], CultureInfo.InvariantCulture);
                                account.Deposit(amount);

                                Console.WriteLine($"Deposited {amount:C}");
                            }

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                Console.WriteLine($"Your balance is {account.Balance:C}.");
            }

            // If we got this far, save the account.
            using (var file = File.Create(AccountFilename))
                new BinaryFormatter().Serialize(file, account);

            return 0;
        }
    }
}
