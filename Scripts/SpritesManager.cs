using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesManager : MonoBehaviour
{
    public Sprite[] SpritesInGame;
    public Sprite getSpritesByName(string SpriteName)
    {
        for (int i = 0; i < SpritesInGame.Length; i++)
        {
            if (SpritesInGame[i].name == SpriteName)
            {
                return SpritesInGame[i];
            }
        }
        return null;
    }
}