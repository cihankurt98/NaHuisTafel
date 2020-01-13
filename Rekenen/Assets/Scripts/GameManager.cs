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
    public Text correctScoreText;
    public Text incorrectScoreText;

    public TextAsset easyFile;
    public TextAsset normalFile;
    public TextAsset hardFile;

    public Image soundImage;
    public Sprite soundOn;
    public Sprite soundOff;
    public AudioClip correctSoundEffect;
    public AudioClip incorrectSoundEffect;

    public GameObject menuCanvas;
    public GameObject gameCanvas;

    // If you want to debug, remove the "//" next to each [SerializeField]
    //[SerializeField]
    private string lineFromFile;
    //[SerializeField]
    private List<string> linesFromFile;
    //[SerializeField]
    private int answer;
    //[SerializeField]
    private int wrongAnswer;
    //[SerializeField]
    private int correctAnswersCount;
    //[SerializeField]
    private int incorrectAnswersCount;
    //[SerializeField]
    private TextAsset textFile;


    void Start()
    {
        gameCanvas.SetActive(false); // leave MenuCanvas active
        linesFromFile = new List<string>();
    }

    private void InitGame()
    {
        ResetScore();
        ConvertFile();
        NextQuestion();
    }

    private void ResetScore()
    {
        correctAnswersCount = 0;
        incorrectAnswersCount = 0;
        correctScoreText.text = correctAnswersCount.ToString();
        incorrectScoreText.text = incorrectAnswersCount.ToString();
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
        if (linesFromFile.Count == 0 && EditorUtility.DisplayDialog("Uitgespeeld", "Gefeliciteerd! Op dit niveau zijn er geen vragen meer over.", "Terug naar het hoofdmenu"))
        {
            gameCanvas.SetActive(false);
            menuCanvas.SetActive(true);
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
            Debug.Log("Calculation Error" + lineFromFile);
        }
    }
    private void GetWrongAnswer()
    {
        // loop to prevent wronganswer being equal to answer (answer + randomresult 0)
        wrongAnswer = answer;
        while (wrongAnswer == answer)
        {
            wrongAnswer = answer + (Random.Range(-10, 11)); // Min = inclusive Max = exclusive
        }
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
        AudioSource audsrc = GetComponent<AudioSource>();

        if (givenAnswer == answer)
        {
            audsrc.clip = correctSoundEffect;
            correctAnswersCount++;
            correctScoreText.text = correctAnswersCount.ToString();
        }
        else
        {
            audsrc.clip = incorrectSoundEffect;
            incorrectAnswersCount++;
            incorrectScoreText.text = incorrectAnswersCount.ToString();
        }
        audsrc.Play();
        NextQuestion();
    }

    public void changeVolume(float value)
    {
        AudioSource audsrc = GetComponent<AudioSource>();
        audsrc.volume = value;

        // change sprite
        if (value == 0.0)
        {
            soundImage.sprite = soundOff;
        }
        else if (value >= 0.0 && soundImage.sprite == soundOff) // Makes sure that it only changes when going from 0.0 to something higher. Preventing extra workload.
        {
            soundImage.sprite = soundOn;
        }
        
    }

    public void changeDifficulty(int mode)
    {
        switch(mode)
        {
            case 0:
                textFile = easyFile;
                break;
            case 1:
                textFile = normalFile;
                break;
            case 2:
                textFile = hardFile;
                break;
        }

        // Deactivate menu and activate game
        menuCanvas.SetActive(false);
        gameCanvas.SetActive(true);

        InitGame();

    }
}
