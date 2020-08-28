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


    public Text playerOneNametext, playerOneCashText, playerOneHandText,dealerHandText;
    public InputField playerOneBetValue;

    void Start()
    {
        GenerateDeck();
        Shuffle(cardsDeck);
        LoadPlayerData();
        StartGame();
        CheckGameConditions();
    }

    void LoadPlayerData()
    {
        // player one
        //Player player= PlayerPrefsManager.LoadPlayerData();
        playerOne.name = "Player 1";
        playerOne.playerCash = 1000;

        playerOne.playerHand = 0;
        playerOne.Playercards = new List<PlayingCard>();

        playerOneNametext.text = playerOne.name;
        playerOneCashText.text = playerOne.playerCash.ToString();
        playerOneBetValue.text = playerOne.playerBet.ToString();
        // dealer

        dealer.name = "Dealer";
        dealer.playerHand = 0;
        dealer.Playercards = new List<PlayingCard>();
    }

    void StartGame()
    {
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
            card.transform.position= dealerCards.transform.position;
            card.transform.localScale = Vector3.one;
            card.transform.position = new Vector3(card.transform.position.x + distanceValue, card.transform.position.y, card.transform.position.z);
            distanceValue += 1;
        }
    }
    void CheckGameConditions()
    {

    }

    public void Hit()
    {
        // give player 1 card and reCalculate 
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
