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

    
    public bool HasBlackJack()
    {
        bool result = false;

        foreach (var card in Playercards)
        {
            if (card.name.Contains("J") || card.name.Contains("Q") || card.name.Contains("k"))
            {
                result = true;
                break;
            }

        }

        return result;
    }

    public bool HasAce()
    {
        bool result = false;

        foreach (var card in Playercards)
        {
            if (card.name.Contains("A"))
            {
                result = true;
                break;
            }
        }

        return result;
    }

}