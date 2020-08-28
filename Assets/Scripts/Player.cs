using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public string playerName;
    public int playerCash;
    public int playerBet;
    public int playerHand;
    public List<PlayingCard> Playercards;

    public int CalculateHandValue()
    {
        playerHand = 0;
        foreach (var card in Playercards)
        {
            playerHand += card.value;
        }
        return playerHand;
    }

}