using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text wordIndicator;
    public Text lettersIndicator;

    private HangmanController hangman;
    private string word;
    private char[] revealed;
    private bool gameCompleted;

    // Start is called before the first frame update
    void Start()
    {
        hangman = GameObject.FindGameObjectWithTag("Player").GetComponent<HangmanController>();
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        SetWord("test");
    }

    public void ResetGame()
    {
        gameCompleted = false;
        Next();
    }

    private void UpdateWordIndicator()
    {
        string displayed = "";

        for (int i = 0; i < revealed.Length; i++)
        {
            char c = revealed[i];
            if (c == 0)
            {
                c = '_';
            }
            displayed += ' ';
            displayed += c;
        }
    }

    private void SetWord(string word)
    {
        this.word = word;
        revealed = new char[word.Length];
        lettersIndicator.text = "" + word.Length;

        UpdateWordIndicator();
    }

}
