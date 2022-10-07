namespace WinnerTests
{
    [TestClass]
    public class PlayCardGameTests
    {
        [TestMethod]
        public void ValidCardGameTest()
        {
            //Arrange
            List<CardHolder> testHands = new List<CardHolder>();
            testHands.Add(new CardHolder("Pete:AH,3C,8C,2S,JD"));
            testHands.Add(new CardHolder("Sam:KD,QH,10C,4C,AC"));
            testHands.Add(new CardHolder("Joe:6S,8D,3D,JH,2D"));
            testHands.Add(new CardHolder("Lilly:5H,3S,KH,AS,9D"));
            testHands.Add(new CardHolder("Jenny:JS,3H,2H,2C,4D"));

            //Act
            WinnerCardGame cardGame = new WinnerCardGame(testHands);
            cardGame.IsTest = true;
            string actualResult = cardGame.Play();

            //Assert
            Assert.IsTrue(cardGame.IsValid);
            string expectedResult = "Lilly:88";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ValidCardGamePeteWinsTest()
        {
            //Arrange
            List<CardHolder> testHands = new List<CardHolder>();
            testHands.Add(new CardHolder("Pete:AH,3C,8S,2S,KS"));
            testHands.Add(new CardHolder("Sam:KD,QH,10C,4C,AC"));
            testHands.Add(new CardHolder("Joe:6S,8D,3D,JH,2D"));
            testHands.Add(new CardHolder("Lilly:5H,3S,KH,AS,9D"));
            testHands.Add(new CardHolder("Jenny:JS,3H,2H,2C,4D"));

            //Act
            WinnerCardGame cardGame = new WinnerCardGame(testHands);
            cardGame.IsTest = true;
            string actualResult = cardGame.Play();

            //Assert
            Assert.IsTrue(cardGame.IsValid);
            string expectedResult = "Pete:98";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ValidCardGameMultipleWinnerTest()
        {
            //Arrange
            List<CardHolder> testHands = new List<CardHolder>();
            testHands.Add(new CardHolder("Pete:AH,3C,8C,2S,JD"));
            testHands.Add(new CardHolder("Sam:AH,3C,8C,2S,JD"));
            testHands.Add(new CardHolder("Joe:AH,3C,8C,2S,JD"));
            testHands.Add(new CardHolder("Lilly:AH,3C,8C,2S,JD"));
            testHands.Add(new CardHolder("Jenny:AH,3C,8C,2S,JD"));

            //Act
            WinnerCardGame cardGame = new WinnerCardGame(testHands);
            cardGame.IsTest = true;
            string actualResult = cardGame.Play();

            //Assert
            Assert.IsTrue(cardGame.IsValid);
            string expectedResult = "Pete,Sam,Joe,Lilly,Jenny:44";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void InValidCardGameCardTooLongTest()
        {
            //Arrange
            List<CardHolder> testHands = new List<CardHolder>();
            testHands.Add(new CardHolder("Pete:AH,3C,8C,2S,JD"));
            testHands.Add(new CardHolder("Sam:KD,QH,10C,4C,AC"));
            testHands.Add(new CardHolder("Joe:6S,8D,3D,JH,2D"));
            testHands.Add(new CardHolder("Lilly:5H,443S,KH,AS,9D"));
            testHands.Add(new CardHolder("Jenny:JS,3H,2H,2C,4D"));

            //Act
            WinnerCardGame cardGame = new WinnerCardGame(testHands);
            cardGame.IsTest = true;
            string actualResult = cardGame.Play();

            //Assert
            Assert.AreEqual(false, cardGame.IsValid);
            string expectedErrorMsg = $"Invalid Card. Please verify your file is formated correctly.{Environment.NewLine}";
            Assert.AreEqual(expectedErrorMsg, actualResult);
        }

        [TestMethod]
        public void InValidCardGameCardTooManyCardsInHandTest()
        {
            //Arrange
            List<CardHolder> testHands = new List<CardHolder>();
            testHands.Add(new CardHolder("Pete:AH,3C,8C,2S,JD"));
            testHands.Add(new CardHolder("Sam:KD,QH,10C,4C,AC"));
            testHands.Add(new CardHolder("Joe:6S,8D,3D,JH,2D"));
            testHands.Add(new CardHolder("Lilly:5H,443S,KH,AS,9D"));
            testHands.Add(new CardHolder("Jenny:JS,3H,2H,2C,4D,5D,6D"));

            //Act
            WinnerCardGame cardGame = new WinnerCardGame(testHands);
            cardGame.IsTest = true;
            string actualResult = cardGame.Play();

            //Assert
            Assert.AreEqual(false, cardGame.IsValid);
            string expectedErrorMsg = $"Invalid Card. Please verify your file is formated correctly.{Environment.NewLine}";
            Assert.AreEqual(expectedErrorMsg, actualResult);
        }

        [TestMethod]
        public void InValidCardGameCardTooManyCardsInHandProductionTest()
        {
            //Arrange
            List<CardHolder> testHands = new List<CardHolder>();
            testHands.Add(new CardHolder("Pete:AH,3C,8C,2S,JD"));
            testHands.Add(new CardHolder("Sam:KD,QH,10C,4C,AC"));
            testHands.Add(new CardHolder("Joe:6S,8D,3D,JH,2D"));
            testHands.Add(new CardHolder("Lilly:5H,443S,KH,AS,9D"));
            testHands.Add(new CardHolder("Jenny:JS,3H,2H,2C,4D,5D,6D"));

            //Act
            WinnerCardGame cardGame = new WinnerCardGame(testHands);
            string actualResult = cardGame.Play();

            //Assert
            Assert.AreEqual(false, cardGame.IsValid);
            string expectedErrorMsg = $"ERROR";
            Assert.AreEqual(expectedErrorMsg, actualResult);
        }
    }
}
