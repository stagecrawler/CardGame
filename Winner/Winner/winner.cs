if (args.Length > 0)
{
    CardGamePlayer pg = new CardGamePlayer(args[1], args[3]);
}
else
{
    CardGamePlayer pg = new CardGamePlayer();
    pg.Run();
}



public class CardGamePlayer
{
    public List<CardHolder> CardHolders { get; protected set; } = new List<CardHolder>();
    public CardGame GameToPlay { get; protected set; }
    public string OutputText { get; protected set; }
    public string InputFile { get; protected set; }
    public string OutputFile { get; protected set; }
    public string Error { get; protected set; }
    public bool IsValid { get; protected set; }
    public bool IsTest { get; set; }

    public CardGamePlayer()
    {
        InputFile = "D:\\Projects\\Dirivco\\Winner\\Winner\\bin\\Debug\\net6.0\\CardGameInputFile.txt";
        OutputFile = "D:\\Projects\\Dirivco\\Winner\\Winner\\bin\\Debug\\net6.0\\CardGameOutputFile.txt";
    }

    public CardGamePlayer(string inputFilePath, string outputFilePath)
    {
        InputFile = inputFilePath;
        OutputFile = outputFilePath;
    }

    public void Run()
    {
        try
        {
            ReadInFile();
            GameToPlay = new WinnerCardGame(CardHolders);
            OutputText = GameToPlay.Play();
        }
        catch(Exception ex)
        {
            if (IsTest)
                OutputText = ex.Message;
            else
                OutputText = "ERROR";
        }
        WriteResultToFile();
    }

    private void ReadInFile()
    {
        string[] lines = System.IO.File.ReadAllLines(InputFile);
        foreach (string line in lines)
        {
            CardHolder ch = new CardHolder(line);
            if (ch.IsValid)
                CardHolders.Add(ch);
            else
            {
                IsValid = true;
                Error += $"There was an issue with the file:{Environment.NewLine} {ch.Error}";
                break;
            }
        }
    }

    private void WriteResultToFile()
    {
        string file = OutputFile;
        File.WriteAllText(file, OutputText);
    }
}

public class WinnerCardGame : CardGame
{
    public List<CardHolder> CardHolders { get; protected set; }
    public string Error { get; set; } = String.Empty;
    public bool IsValid { get; set; } = true;
    public string OutputText { get; protected set; }
    public bool IsTest { get; set; } = false;

    public WinnerCardGame(List<CardHolder> cardHolders)
    {
        CardHolders = cardHolders;
    }

    public string Play()
    {
        ValidateCardHolders();
        if (IsValid)
            RunTheGame();
        else
            CreateErrorMessageOutput();

        return OutputText;
    }

    private void ValidateCardHolders()
    {
        List<CardHolder> invalidCardHolders = CardHolders.FindAll(x => x.IsValid == false);
        if(invalidCardHolders.Count != 0)
        {
            Error = invalidCardHolders[0].Error;
            IsValid = false;
        }
    }

    private void RunTheGame()
    {
        int maxScore = CardHolders.Max(x => x.HandValue);
        List<CardHolder> winners = CardHolders.FindAll(x => x.HandValue == maxScore);

        if (winners.Count == 1)
            CreateOutputForOneWinner(winners);
        else
            CreateOutputFormultipleWinners(winners);
    }

    private void CreateOutputForOneWinner(List<CardHolder> winners)
    {
        OutputText = $"{winners[0].Name}:{winners[0].HandValue}";
    }

    private void CreateErrorMessageOutput()
    {
        if (IsTest)
            OutputText = Error;
        else
            OutputText = "ERROR";

        IsValid = false;
    }

    private void CreateOutputFormultipleWinners(List<CardHolder> winners)
    {
        foreach (CardHolder ch in winners)
            OutputText += $"{ch.Name},";

        if (!String.IsNullOrEmpty(OutputText) && OutputText[OutputText.Length - 1] == ',')
            OutputText = OutputText.Substring(0, OutputText.Length - 1);

        OutputText += $":{winners[0].HandValue}";
    }
}

public class CardHolder
{
    public string Name { get; private set; }
    public List<Card> Hand { get; private set; } = new List<Card>();
    public int HandValue { get; private set; } = 0;
    public string Error { get; private set; } = String.Empty;
    public bool IsValid { get; private set; } = true;

    public CardHolder(string cardInput)
    {
        ValidateCardInput(cardInput);
        if (IsValid)
        {
            Name = cardInput.Split(':')[0];
            string cs = cardInput.Split(':')[1];
            CreateHand(cs);
            GenerateTotalValue();
        }
    }

    private void ValidateCardInput(string cardInput)
    {
        if (!cardInput.Contains(":"))
        {
            IsValid = false;
            Error = $"Invalid Input File! Name should be separated from cards via a colon.{Environment.NewLine}";
        }

        if(!cardInput.Contains(","))
        {
            IsValid=false;
            Error += $"Invalid Input File! Cards must be seperated by commas.{Environment.NewLine}";
        }

        if(!IsValid)
            Error += $"Please make sure that input are structured as following:Name1:AH,3C,8C,2S,JD {Environment.NewLine}";
    }

    public int GenerateTotalValue()
    {
        foreach(Card card in Hand)
            HandValue += card.CardValue;

        return HandValue;
    }

    private void CreateHand(string cs)
    {
        string[] cards = cs.Split(',');
        if (cards.Length == 5)
        {
            foreach (string card in cards)
            {
                Card newCard = new Card(card);
                newCard.CalculateValue();
                if (newCard.IsValid)
                {
                    Hand.Add(newCard);
                }
                else
                {
                    Error += newCard.Error;
                    IsValid = false;
                }
            }
        }
        else
        {
            Error += $"A hand must consist of 5 cards!";
            IsValid = false;
        }
    }
}

public class Card
{
    public Suit MySuit { get; private set; } = Suit.invalid;
    public int  CardValue { get; private set; } = 0;
    public int FaceValue { get; private set; } = 0;
    public string Error { get; private set; } = String.Empty;
    public bool IsValid { get; private set; } = true;
    public string RawValue { get; private set; } = String.Empty;

    public Card(string card)
    {
        RawValue = card;
       
    }

    public void CalculateValue()
    {
        List<string> parsedCard = ParseCard();
        if (IsValid)
        {
            SetCardValue(parsedCard[0]);
            if (IsValid)
                SetCardSuit(parsedCard[1]);

            if (IsValid)
                SetTotalCardValue();
        }
    }

    private List<string> ParseCard()
    {
        List<string> retList = new List<string>();
        if (RawValue.Length < 2 || RawValue.Length > 3)
        {
            Error = $"Invalid Card. Please verify your file is formated correctly.{Environment.NewLine}";
            IsValid = false;
            return retList;
        }

        if (RawValue.Length == 3 )
        {
            retList.Add(RawValue.Substring(0, 2));
            retList.Add(RawValue.Substring(RawValue.Length - 1, 1));
        }
        else
        {
            retList.Add(RawValue[0].ToString());
            retList.Add(RawValue[1].ToString());
        }
        return retList;
    }

    private void SetTotalCardValue()
    {
        CardValue = FaceValue * (int)MySuit;
    }

    private void SetCardSuit(string v)
    {
        switch (v)
        {
            case "C": MySuit = Suit.clubs; break;
            case "D": MySuit = Suit.diamonds; break;
            case "H": MySuit = Suit.hearts; break;
            case "S": MySuit = Suit.spades; break;
            default: MySuit = Suit.invalid; break;
        }
        if (MySuit == Suit.invalid)
        {
            IsValid = false;
            Error += $"Invalid Suit: Must be: (S = Spades, H = Hearts, D = Diamonds and C = Clubs).{Environment.NewLine}";
        }
    }

    private void SetCardValue(string v)
    {
        if (v.All(char.IsDigit))
            FaceValue = int.Parse(v.ToString());
        else
        {
            switch(v)
            {
                case "J": FaceValue = 11; break;
                case "Q": FaceValue = 12; break;
                case "K": FaceValue = 13; break;
                case "A": FaceValue = 1; break;
                default : FaceValue = 0; break;
            }
        }

        if(FaceValue == 0 || FaceValue > 13)
        {
            IsValid = false;
            Error += $"Invalid Card Value: Must be a number between 1 and 10 or a J (11), Q (12), K (13) or an A (1){Environment.NewLine}";
        }
    }
}

public interface CardGame
{
    string Play();
}

public enum Suit
{
    invalid = 0,
    clubs = 1,
    diamonds = 2,
    hearts = 3, 
    spades = 4
}

