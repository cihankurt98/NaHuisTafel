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
    
    public void TicTacToeButton(int whichButton)
    {
        tictactoeSpaces[whichButton].image.sprite = playerIcons[playerTurn]; // Change empty sprite to X or O
        tictactoeSpaces[whichButton].interactable = false; // Button can not be used after pressed

       // playerTurn = playerTurn == 0 ? 1 : 0; // Change turn depending on who's turn it actually is.

        if (playerTurn == 0)
        {
            playerTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            playerTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }
}
