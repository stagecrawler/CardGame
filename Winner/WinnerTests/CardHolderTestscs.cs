namespace WinnerTests
{
    [TestClass]
    public class CardHolderTestscs
    {
        [TestMethod]
        public void CardHolderHandValueTest()
        {
            //Arrange
            string cardInput = "JohnnyBoy Johnson:AH,3C,8C,2S,JD";
            CardHolder cardHolder = new CardHolder(cardInput);

            //Assert
            string expectedName = "JohnnyBoy Johnson";
            Assert.AreEqual(expectedName, cardHolder.Name);
            Assert.AreEqual(44, cardHolder.HandValue);
        }

        [TestMethod]
        public void CardHolderInvalidInputNoColonTest()
        {
            //Arrange
            string cardInput = "JohnnyBoy Johnson'AH,3C,8C,2S,JD";
            CardHolder cardHolder = new CardHolder(cardInput);

            //Assert
            string expectedError = $"Invalid Input File! Name should be separated from cards via a colon.{Environment.NewLine}" + 
                $"Please make sure that input are structured as following:Name1:AH,3C,8C,2S,JD {Environment.NewLine}";
            Assert.AreEqual(false, cardHolder.IsValid);
            Assert.AreEqual(expectedError, cardHolder.Error);
        }

        [TestMethod]
        public void CardHolderInvalidInputNoCommas()
        {
            //Arrange
            string cardInput = "JohnnyBoy Johnson:AH.3C.8C.2S.JD";
            CardHolder cardHolder = new CardHolder(cardInput);

            //Assert
            string expectedError = $"Invalid Input File! Cards must be seperated by commas.{Environment.NewLine}" +
                $"Please make sure that input are structured as following:Name1:AH,3C,8C,2S,JD {Environment.NewLine}";
            Assert.AreEqual(false, cardHolder.IsValid);
            Assert.AreEqual(expectedError, cardHolder.Error);
        }

        [TestMethod]
        public void CardHolderNotEnoughCards()
        {
            //Arrange
            string cardInput = "JohnnyBoy Johnson:AH,3C,8C,2S";
            CardHolder cardHolder = new CardHolder(cardInput);

            //Assert
            string expectedError = "A hand must consist of 5 cards!";
            Assert.AreEqual(false, cardHolder.IsValid);
            Assert.AreEqual(expectedError, cardHolder.Error);
        }

        [TestMethod]
        public void CardHolderTooManyCards()
        {
            //Arrange
            string cardInput = "JohnnyBoy Johnson:AH,3C,8C,2S,4C,5C";
            CardHolder cardHolder = new CardHolder(cardInput);

            //Assert
            string expectedError = "A hand must consist of 5 cards!";
            Assert.AreEqual(false, cardHolder.IsValid);
            Assert.AreEqual(expectedError, cardHolder.Error);
        }
    }
}
