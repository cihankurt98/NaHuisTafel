using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int playerTurn; // 0 = player X and 1 = player O
    public int turnCounter; // counts the number of played turns
    public GameObject[] turnIcons; // Holding the arrow turn icons for both players
    public Sprite[] playerIcons; // 0 = player X icon and 1 = player O icon
    public Button[] tictactoeSpaces; // playable spaces for the game
    void Start()
    {
        GameSetup();
    }

    void Update()
    {
        
    }

    void GameSetup()
    {
        playerTurn = 0; // Player X always starts.
        turnCounter = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
    }
}
