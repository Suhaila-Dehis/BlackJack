using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<PlayingCard> cardsDeck;
    public Player playerOne, dealer;

    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

    public SpritesManager spritesManager;

    public GameObject CardPrefab, cardsParent,playerOneCards,dealerCards;
    int topOfDeckIndex=0; // index of first car in the deck


    public Text playerOneNametext, playerOneCashText, playerOneHandText,dealerHandText,gameStatusText;
    public InputField playerOneBetValue;
    public GameObject winLoseGameObject;
    void Start()
    {
        GenerateDeck();
        Shuffle(cardsDeck);
        LoadPlayerData();
        StartGame();
        //CheckGameConditions();
        
    }

    void LoadPlayerData()
    {
        // player one
        //Player player= PlayerPrefsManager.LoadPlayerData();
        playerOne.name = "Player 1";
        playerOne.playerCash = 100;
        playerOne.playerBet = 10;
        playerOne.playerHand = 0;
        playerOne.Playercards = new List<PlayingCard>();

        playerOneNametext.text = playerOne.name;
        playerOneCashText.text ="Cash $"+playerOne.playerCash.ToString();
        playerOneBetValue.text = playerOne.playerBet.ToString();
        // dealer

        dealer.name = "Dealer";
        dealer.playerHand = 0;
        dealer.Playercards = new List<PlayingCard>();
    }

    void StartGame()
    {
        topOfDeckIndex = 0;
        winLoseGameObject.SetActive(false);
        // hand 2 cards to each player
        playerOne.Playercards.Add(cardsDeck[topOfDeckIndex]);
        topOfDeckIndex++;
        playerOne.Playercards.Add(cardsDeck[topOfDeckIndex]);
        topOfDeckIndex++;

        // calculate hand value
        playerOneHandText.text = playerOne.CalculateHandValue().ToString();
        // set card position on screen
        int distanceValue = 0;
        foreach (var card in playerOne.Playercards)
        {
            card.transform.parent = playerOneCards.transform;
            card.transform.position = playerOneCards.transform.position;
            card.transform.localScale = Vector3.one;
            card.transform.position = new Vector3(card.transform.position.x + distanceValue, card.transform.position.y, card.transform.position.z);
            distanceValue += 1;
        }

        // 2 cards for dealer
        dealer.Playercards.Add(cardsDeck[topOfDeckIndex]);
        topOfDeckIndex++;

        dealer.Playercards.Add(cardsDeck[topOfDeckIndex]);
        topOfDeckIndex++;


        // calculate hand value
        dealerHandText.text = dealer.CalculateHandValue().ToString();
        // set card position on screen
        distanceValue = 0;
        foreach (var card in dealer.Playercards)
        {
            card.transform.parent = dealerCards.transform;
            card.transform.position = dealerCards.transform.position;
            card.transform.localScale = Vector3.one;
            card.transform.position = new Vector3(card.transform.position.x + distanceValue, card.transform.position.y, card.transform.position.z);
            distanceValue += 1;
        }

        // check for game  init calculations
        if (playerOne.playerHand > 21)
        {
            playerOne.playerCash -= playerOne.playerBet;
            DrawGameResult(false, false);
        }
        else if (dealer.playerHand > 21)
        {
            playerOne.playerCash += playerOne.playerBet;
            DrawGameResult(true, false);
        }
    }

    void CheckGameConditions()
    {
        bool iWin = false, draw = false;
        if (playerOne.playerHand > 21)
        {
            // lose
            iWin = false;
            draw = false;
        }
        else if (playerOne.playerHand == dealer.playerHand)
        {
            // draw
            iWin = false;
            draw = true;
        }
        else if (playerOne.playerHand < 21 && dealer.playerHand > 21)
        {
            // i win
            iWin = true;
            draw = false;

        }
        else if (playerOne.playerHand > dealer.playerHand)
        {
            // i win
            iWin = true;
            draw = false;
        }
        else if (playerOne.playerHand < 21 && (playerOne.HasAce() && playerOne.HasBlackJack()) && (!dealer.HasBlackJack() || !dealer.HasAce()))
        {
            // i win
            iWin = true;
            draw = false;
        }

        DrawGameResult(iWin, draw);

    }
    void DrawGameResult(bool iWin,bool draw)
    {
        if (iWin) // i win
        {
            playerOne.playerCash += playerOne.playerBet;
            gameStatusText.text = "Player One Wins";
        }
        else if (!iWin && !draw) // lose
        {
            playerOne.playerCash -= playerOne.playerBet;
            gameStatusText.text = "Player One Lose";
        }
        else 
        {
            gameStatusText.text = "Its a Draw";
        }
        winLoseGameObject.SetActive(true);

        playerOneCashText.text = playerOne.playerCash.ToString(); 
    }
    public void Hit()
    {
        // give player 1 card and reCalculate 
        playerOne.Playercards.Add(cardsDeck[topOfDeckIndex]);
        int lastCardIndex = playerOne.Playercards.Count-1;
        playerOne.Playercards[lastCardIndex].transform.parent = playerOneCards.transform;
        playerOne.Playercards[lastCardIndex].transform.localScale = Vector3.one;
        playerOne.Playercards[lastCardIndex].transform.position = new Vector3(playerOneCards.transform.position.x + playerOne.Playercards.Count-1, playerOne.Playercards[lastCardIndex].transform.position.y, playerOne.Playercards[lastCardIndex].transform.position.z);
        playerOneHandText.text = playerOne.CalculateHandValue().ToString();

        topOfDeckIndex++;
        // calculate game 
        CheckGameConditions();
    }

    public void Stand()
    {
        // dealer hits cards until 17
        while (dealer.playerHand < 17)
        {
            
            dealer.Playercards.Add(cardsDeck[topOfDeckIndex]);
            int lastCardIndex = dealer.Playercards.Count - 1;
            dealer.Playercards[lastCardIndex].transform.parent = dealerCards.transform;
            dealer.Playercards[lastCardIndex].transform.localScale = Vector3.one;
            dealer.Playercards[lastCardIndex].transform.position = new Vector3(dealerCards.transform.position.x + dealer.Playercards.Count - 1, dealer.Playercards[lastCardIndex].transform.position.y, dealer.Playercards[lastCardIndex].transform.position.z);
            dealerHandText.text = dealer.CalculateHandValue().ToString();
            topOfDeckIndex++;
        }
        // calculate game
        
        CheckGameConditions();        
    }

    public void RestartGame()
    {
        // remove players cards
        foreach (var card in playerOne.Playercards)
        {
            card.transform.parent = cardsParent.transform;
            card.transform.position = cardsParent.transform.position;
        }
        playerOne.Playercards.Clear();

        foreach (var card in dealer.Playercards)
        {
            card.transform.parent = cardsParent.transform;
            card.transform.position = cardsParent.transform.position;
        }

        dealer.Playercards.Clear();

        Shuffle(cardsDeck);
        StartGame();
    }

    public void OnBetValueChanged()
    {
        playerOne.playerBet = int.Parse(playerOneBetValue.text);
    }

    public void GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        cardsDeck = new List<PlayingCard>();
        int valueIndex = 0;
        for (int i = 0; i < 8; i++)         // generate 8 decks of cards
        {
            foreach (string s in suits)
            {
                foreach (string v in values)
                {
                    newDeck.Add(s + v);
                    string name = s + v;
                    if (v.Equals("K") || v.Equals("Q") || v.Equals("J"))
                    {
                        valueIndex = 10;
                    }
                    else
                    {
                        valueIndex++;
                    }
                    GameObject card = Instantiate(CardPrefab, cardsParent.transform);
                    card.gameObject.name = name;
                    PlayingCard playingCard = card.GetComponent<PlayingCard>();
                    playingCard.SetPlayingCard(name, valueIndex, s, spritesManager.getSpritesByName(name));
                    cardsDeck.Add(playingCard);
                }
                valueIndex = 0;
            }
        }
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

}
