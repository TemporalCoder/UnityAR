using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // required when using UI elements in scripts
using TMPro;

public class quizStuff : MonoBehaviour
{
    public TMP_Text question;
    public TMP_Text[] buttonText;
    public Button nextButton;

    public List<quizQuestions> questions = new List<quizQuestions>();
    int currentQuestion = 0;    //pointer to question
    int score = 0;

    void Start()
    {
        updateQuestion();
        nextButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void updateQuestion()
    {
        question.text = questions[currentQuestion].Q;
        
        buttonText[0].text = questions[currentQuestion].A;
        buttonText[1].text = questions[currentQuestion].B;
        buttonText[2].text = questions[currentQuestion].C;
        buttonText[3].text = questions[currentQuestion].D;
    }

    public void clickIt(char answer)
    {
        currentQuestion++; //go to next question
        if (answer == questions[currentQuestion].answer)
        {
            score += 10;
        }
        else
        {

        }
    }

    public void answerClick(string answer)
    {        
        //No validation to prevent out of turn clicking!!!

        if (answer[0] == questions[currentQuestion].answer) //no validation from inspector input! 
        {
            score += 10;
            buttonText[4].text = "CORRECT: " + score.ToString() + " Points";            
        }
        else
        {
            buttonText[4].text = "That is Incorrect: " + score.ToString() + " Points";
        }

        currentQuestion++; //go to next question
        nextButton.interactable = true; //set next button clickable
    }

    public void nextClick()
    {
        updateQuestion(); //move to next button
        nextButton.interactable = false;
    }

}

[System.Serializable]
public class quizQuestions
{
    public string Q;
    public string A;
    public string B;
    public string C;
    public string D;
    public char answer;
}

