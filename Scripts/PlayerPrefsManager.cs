using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsManager 
{


    //public string playerName { get; set; }
    //public string playerCash { get; set; }
    //public string playerBet { get; set; }
    //public string playerHand { get; set; }
    //public List<PlayingCard> Playercards;


    public static void SavePlayerData(Player playerData) {
        PlayerPrefs.SetString("name", playerData.name);
        PlayerPrefs.SetInt("cash", playerData.playerCash);
        PlayerPrefs.SetInt("bet", playerData.playerBet);        
    }

    public static Player LoadPlayerData()
    {
        Player player = new Player();
        player.name= PlayerPrefs.GetString("name", "Player 1");
        player.playerCash=PlayerPrefs.GetInt("cash", 100);
        player.playerBet= PlayerPrefs.GetInt("bet", 10);

        return player;
    }
}
