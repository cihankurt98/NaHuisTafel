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
    public int[] markedSpaces; // Identifies which space was marked by which player
    public Text winnerText; // Holds the text for the winner
    public GameObject[] winningLines; // Holds the lines for every possible solution
    public GameObject winnerPanel;
    public int xPlayerScore;
    public int oPlayerScore;
    public Text xPlayerScoreText;
    public Text oPlayerScoreText;
    public Button rematchButton;
    public Button restartButton;
    void Start()
    {
        GameSetup();
    }

    void Update()
    {
        
    }

    void GameSetup()
    {
        
        rematchButton.interactable = false;
        restartButton.interactable = false;
        playerTurn = 0; // Player X always starts.
        turnCounter = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
        for(int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100; // Can't initialize with 0, because 0 stands for player X. -1 Will cause logic errors.
        }
    }

    bool CheckWinner()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2]; // Horizontal 1
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5]; // Horizontal 2
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8]; // Horizontal 3
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6]; // Vertical 1
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7]; // Vertical 2
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8]; // Vertical 3
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8]; // Diagonal 1
        int s8 = markedSpaces[6] + markedSpaces[4] + markedSpaces[2]; // Diagonal 2

        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };

        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3 * (playerTurn + 1))
            {
                DisplayWinner(i);
                return true;
            }
        }
        return false;
    }

    void DisplayWinner(int indexIn)
    {
        winnerPanel.gameObject.SetActive(true);
        if (playerTurn == 0)
        {
            xPlayerScore++;
            xPlayerScoreText.text = xPlayerScore.ToString();
            winnerText.text = "Speler X heeft gewonnen!";
        }
        else if (playerTurn == 1)
        {
            oPlayerScore++;
            oPlayerScoreText.text = oPlayerScore.ToString();
            winnerText.text = "Speler O heeft gewonnen!";
        }

        winningLines[indexIn].SetActive(true);
        makeButtonsActive();
    }
    
    void Draw()
    {
        winnerPanel.SetActive(true);
        winnerText.text = "Het is een gelijkspel!";
        makeButtonsActive();
    }

    void makeButtonsActive()
    {
        rematchButton.interactable = true;
        restartButton.interactable = true;
    }

    public void TicTacToeButton(int whichButton)
    {
        tictactoeSpaces[whichButton].image.sprite = playerIcons[playerTurn]; // Change empty sprite to X or O
        tictactoeSpaces[whichButton].interactable = false; // Button can not be used after pressed

        markedSpaces[whichButton] = playerTurn + 1; // Mark the button with the value of the corresponding player. Need to do +1 because of possible logic errors
        turnCounter++;
        if (turnCounter > 4) // Winning is only possible after 4 turns, so there is no need to check before.
        {
            bool isWinner = CheckWinner();
            if (turnCounter == 9 && isWinner == false) // If 9 spaces are filled without having a winner, it will be a draw.
            {
                Draw();
            }
        }

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
    
    public void Rematch()
    {
        GameSetup();
        for (int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i].SetActive(false);
        }
        winnerPanel.SetActive(false);
    }

    public void Restart()
    {
        Rematch();
        xPlayerScore = 0;
        oPlayerScore = 0;
        xPlayerScoreText.text = xPlayerScore.ToString();
        oPlayerScoreText.text = oPlayerScore.ToString();
    }

}
