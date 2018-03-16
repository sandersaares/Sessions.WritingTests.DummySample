using System;

namespace DummySample
{
    [Serializable]
    public sealed class BankAccount
    {
        public int Balance { get; private set; }

        public void Deposit(int amount)
        {
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "You cannot withdraw a negative amount.");

            if (amount > Balance)
                throw new ArgumentOutOfRangeException(nameof(amount), "You cannot withdraw more money than you have!");

            Balance -= amount;
        }
    }
}
