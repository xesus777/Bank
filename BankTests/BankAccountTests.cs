namespace BankTests
{
    using BankAccountNS;
    [TestClass]
    public sealed class BankAccountTests
    {
        // ==================== ТЕСТЫ ДЛЯ МЕТОДА DEBIT ====================

        /// <summary>
        /// Тест корректного списания: проверяет, что баланс уменьшается на правильную сумму.
        /// </summary>
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            account.Debit(debitAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        /// <summary>
        /// Тест: попытка списания суммы меньше нуля должна вызвать исключение ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act & Assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => account.Debit(debitAmount));
        }

        /// <summary>
        /// Тест: попытка списания суммы больше баланса должна вызвать исключение ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act & Assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => account.Debit(debitAmount));
        }

        /// <summary>
        /// Улучшенный тест: проверка, что при списании суммы больше баланса
        /// выбрасывается исключение с правильным сообщением.
        /// </summary>
        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange_WithCorrectMessage()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }
            Assert.Fail("The expected exception was not thrown.");
        }

        /// <summary>
        /// Улучшенный тест: проверка, что при списании суммы меньше нуля
        /// выбрасывается исключение с правильным сообщением.
        /// </summary>
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange_WithCorrectMessage()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = -5.00;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
                return;
            }
            Assert.Fail("The expected exception was not thrown.");
        }

        // ==================== ТЕСТЫ ДЛЯ МЕТОДА CREDIT ====================

        /// <summary>
        /// Тест корректного зачисления: проверяет, что баланс увеличивается на правильную сумму.
        /// </summary>
        [TestMethod]
        public void Credit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double creditAmount = 5.00;
            double expected = 16.99;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            account.Credit(creditAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not credited correctly");
        }

        /// <summary>
        /// Тест: попытка зачисления отрицательной суммы должна вызвать исключение ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double creditAmount = -5.00;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act & Assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => account.Credit(creditAmount));
        }

        /// <summary>
        /// Улучшенный тест: проверка, что при зачислении отрицательной суммы
        /// выбрасывается исключение с правильным сообщением.
        /// </summary>
        [TestMethod]
        public void Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange_WithCorrectMessage()
        {
            // Arrange
            double beginningBalance = 11.99;
            double creditAmount = -5.00;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            try
            {
                account.Credit(creditAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, "amount");
                return;
            }
            Assert.Fail("The expected exception was not thrown.");
        }

        /// <summary>
        /// Тест: попытка зачисления нулевой суммы (допустимо, не должно быть исключений).
        /// </summary>
        [TestMethod]
        public void Credit_WithZeroAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double creditAmount = 0.00;
            double expected = 11.99;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            account.Credit(creditAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Balance should not change when crediting zero");
        }

        /// <summary>
        /// Тест: попытка зачисления очень большой суммы (проверка переполнения не требуется, но допустимо).
        /// </summary>
        [TestMethod]
        public void Credit_WithLargeAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 100.00;
            double creditAmount = 1e10; // 10 миллиардов
            double expected = beginningBalance + creditAmount;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            account.Credit(creditAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not credited correctly with large amount");
        }
    }
}

