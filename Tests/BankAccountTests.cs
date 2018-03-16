using DummySample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public sealed class BankAccountTests
    {
        public BankAccountTests()
        {
            _account = new BankAccount();
        }

        // This is the main instance we run tests on.
        private readonly BankAccount _account;

        [TestMethod]
        public void NewAccount_HasZeroBalance()
        {
            Assert.AreEqual(0, _account.Balance);
        }

        [TestMethod]
        public void DepositAndWithdraw_DoWhatItSays()
        {
            _account.Deposit(123);
            Assert.AreEqual(123, _account.Balance);

            _account.Withdraw(50);
            Assert.AreEqual(73, _account.Balance);
        }

        [TestMethod]
        public void Deposit_WithNegativeValue_ThrowsExceptions()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _account.Deposit(-1));
        }

        [TestMethod]
        public void Withdraw_WithNegativeValue_ThrowsExceptions()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _account.Withdraw(-1));
        }

        [TestMethod]
        public void Withdraw_MoreThanBalance_ThrowsExceptions()
        {
            _account.Deposit(200);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _account.Withdraw(201));
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(1000000)]
        [DataRow(1000000000)]
        [DataRow(1234567890)]
        public void Deposit_DifferentAmounts_WorksFine(int amount)
        {
            _account.Deposit(amount);

            Assert.AreEqual(amount, _account.Balance);
        }
    }
}
