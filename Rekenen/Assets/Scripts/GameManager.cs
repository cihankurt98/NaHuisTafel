using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text questionText;
    public Text answerAText;
    public Text answerBText;
    public int wrongAnswersCount;
    public int correctAnswersCount;

    [SerializeField]
    private int a;
    [SerializeField]
    private int b;
    [SerializeField]
    private int correctAnswer;
    [SerializeField]
    private int wrongAnswer;
    [SerializeField]
    private int methodCase;
    [SerializeField]
    private string question;
    private Dictionary<int, string> methodDictionary;

    public void Start()
    {
        correctAnswer = 0;
        wrongAnswer = 0;
        methodCase = 0;
        methodDictionary = new Dictionary<int, string>();
        methodDictionary.Add(0, " + ");
        methodDictionary.Add(1, " - ");
        methodDictionary.Add(2, " * ");
        methodDictionary.Add(3, " / ");

        MakeQuestion();
    }
    private void MakeQuestion()
    {
        a = Random.Range(1, 100);
        b = Random.Range(1, 100);

        calculateCorrectAnswer();
        calculateWrongAnswer();

        string question; // Default construction = " " ;

        if (a >= b)
        {
            question = a.ToString() + methodDictionary[methodCase] + b.ToString();
        }
        else
        {
            question = b.ToString() + methodDictionary[methodCase] + a.ToString();
        }

        UpdateUI(question, correctAnswer.ToString(), wrongAnswer.ToString());

        methodCase = (methodCase == 3) ? (0) : (methodCase + 1);
    }

    private void calculateCorrectAnswer()
    {
        switch (methodCase)
        {
            case 0: // +
                correctAnswer = a + b;
                break;
            case 1: // -
                correctAnswer = (a >= b) ? (a - b) : (b - a);
                break;
            case 2: // *
                correctAnswer = a * b;
                break;
            case 3: // :
                correctAnswer = (a >= b) ? (a / b) : (b / a);
                break;
        }
    }

    private void calculateWrongAnswer()
    {
        wrongAnswer = Random.Range(correctAnswer - 10, correctAnswer + 10);
    }

    private void UpdateUI(string question, string correctAnswer, string wrongAnswer)
    {
        // Display question
        questionText.text = question;


        // Randomly assign the correct and wronganswers to the button texts.
        if (Random.Range(0, 1) == 0)
        {
            answerAText.text = correctAnswer;
            answerBText.text = wrongAnswer;
        }
        else
        {
            answerAText.text = correctAnswer;
            answerBText.text = wrongAnswer;
        }
    }

    public void NextQuestion(Text pressedButtonText)
    {
        int givenAnswer;
        int.TryParse(pressedButtonText.text, out givenAnswer);

        if (givenAnswer == correctAnswer)
        {
            correctAnswersCount++;
        }
        else
        {
            wrongAnswersCount++;
        }
        MakeQuestion();
    }

}
