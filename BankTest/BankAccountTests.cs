using Bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankTest {
	[TestClass]
	public class UnitTest1 {

		[TestMethod]
		public void Debit_WithValidAmount_UpdateBalance() {
			// Arrange
			double beginningBalance = 11.99;
			double debitAmount = 3.55;
			double expected = 7.44;
			BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

			// Act
			account.Debit(debitAmount);

			// Assert
			double actual = account.Balance;
			Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
		}
	}
}
