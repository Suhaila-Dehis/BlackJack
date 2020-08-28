using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingCard : MonoBehaviour
{
    public string cardName;
    public int value;
    public string suite;
    public Sprite cardSprite;

    public void SetPlayingCard(string cardName, int value, string suite, Sprite cardSprite)
    {
        this.cardName = cardName;
        this.value = value;
        this.suite = suite;
        this.cardSprite = cardSprite;
        this.GetComponent<SpriteRenderer>().sprite = this.cardSprite;

    }

}