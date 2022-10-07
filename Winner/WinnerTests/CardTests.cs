namespace WinnerTests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void CardValueTest()
        {
            //Arrange
            Card testCard = new Card("AH");

            //Act
            testCard.CalculateValue();

            //Assert
            Assert.AreEqual(3, testCard.CardValue);
        }

        [TestMethod]
        public void CardInvalidSuitTest()
        {
            //Arrange
            Card testCard = new Card("AP");

            //Act
            testCard.CalculateValue();

            //Assert
            Assert.AreEqual(0, testCard.CardValue);
            Assert.IsFalse(testCard.IsValid);
            string expectedErrorMsg = $"Invalid Suit: Must be: (S = Spades, H = Hearts, D = Diamonds and C = Clubs).{Environment.NewLine}";
            Assert.AreEqual(expectedErrorMsg, testCard.Error);
        }

        [TestMethod]
        public void CardFaceValueTest()
        {
            //Arrange
            Card testCard = new Card("6C");

            //Act
            testCard.CalculateValue();

            //Assert
            Assert.AreEqual(6, testCard.CardValue);
        }

        [TestMethod]
        public void CardInvalidCardLengthTooLongTest()
        {
            //Arrange
            Card testCard = new Card("336C");

            //Act
            testCard.CalculateValue();

            //Assert
            Assert.AreEqual(0, testCard.CardValue);
            Assert.IsFalse(testCard.IsValid);
            string expectedErrorMsg = $"Invalid Card. Please verify your file is formated correctly.{Environment.NewLine}";
            Assert.AreEqual(expectedErrorMsg, testCard.Error);
        }

        [TestMethod]
        public void CardInvalidCardLengthTooShortTest()
        {
            //Arrange
            Card testCard = new Card("3");

            //Act
            testCard.CalculateValue();

            //Assert
            Assert.AreEqual(0, testCard.CardValue);
            Assert.IsFalse(testCard.IsValid);
            string expectedErrorMsg = $"Invalid Card. Please verify your file is formated correctly.{Environment.NewLine}";
            Assert.AreEqual(expectedErrorMsg, testCard.Error);
        }

        [TestMethod]
        public void CardInvalidFaceValueTest()
        {
            //Arrange
            Card testCard = new Card("33C");

            //Act
            testCard.CalculateValue();

            //Assert
            Assert.AreEqual(0, testCard.CardValue);
            Assert.IsFalse(testCard.IsValid);
            string expectedErrorMsg = $"Invalid Card Value: Must be a number between 1 and 10 or a J (11), Q (12), K (13) or an A (1){Environment.NewLine}";
            Assert.AreEqual(expectedErrorMsg, testCard.Error);
        }
    }
}