using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text questionText;
    public Text answerAText;
    public Text answerBText;

    public TextAsset textFile;

    // Debugging
    [SerializeField]
    private string lineFromFile;
    [SerializeField]
    private List<string> linesFromFile;
    [SerializeField]
    private int answer;
    [SerializeField]
    private int wrongAnswer;
    [SerializeField]
    private int correctAnswersCount;
    [SerializeField]
    private int wrongAnswersCount;

    void Start()
    {
        linesFromFile = new List<string>();
        ConvertFile();
        NextQuestion();
    }

    private void ConvertFile()
    {
        string[] tempLinesFromFile = textFile.text.Split('\n'); // Needed because List and Split are not compatible
        foreach (string i in tempLinesFromFile)
        {
            linesFromFile.Add(i);
        }
    }

    private void TakeRandomLine()
    {
        if (linesFromFile.Count == 0  && EditorUtility.DisplayDialog("Uitgespeeld" ,"Gefeliciteerd! Op dit niveau zijn er geen vragen meer over.","Terug naar het hoofdmenu"))
        {
          
        }

        // Generate a random integer within the range of the list. Search for that index within the list. 
        // Take that string and remove it afterwards to avoid getting the same one over and over again.
        int lineNumber = Random.Range(0, (linesFromFile.Count - 1));
        lineFromFile = linesFromFile[lineNumber];
        linesFromFile.Remove(lineFromFile);
    }

    private void GetCorrectAnswer()
    {
        ExpressionEvaluator.Evaluate<int>(lineFromFile, out answer);
        if (answer == 0)
        {
            Debug.Log("fail " + lineFromFile);
            //GetCorrectAnswer(); // Try to calculate again, since the file should not contain any mistakes. ExpressionEvaluator is sometimes returning an error without a reason.
        }
    }
    private void GetWrongAnswer()
    {
        wrongAnswer = answer + (Random.Range(-10, 11)); // Min = inclusive Max = exclusive
    }

    private void DisplayQuestion()
    {
        questionText.text = lineFromFile;
        if (Random.Range(0, 2) == 0)
        {
            answerAText.text = answer.ToString();
            answerBText.text = wrongAnswer.ToString();
        }
        else
        {
            answerBText.text = answer.ToString();
            answerAText.text = wrongAnswer.ToString();
        }
    }
    private void NextQuestion()
    {
        TakeRandomLine();
        GetCorrectAnswer();
        GetWrongAnswer();
        DisplayQuestion();
    }

    public void answerGiven(Text givenAnswerText)
    {
        int givenAnswer;
        int.TryParse(givenAnswerText.text, out givenAnswer);

        if (givenAnswer == answer)
        {
            // Play sound
            correctAnswersCount++;
        }
        else
        {
            // Play sound
            wrongAnswersCount++;
        }
        NextQuestion();
    }


}
