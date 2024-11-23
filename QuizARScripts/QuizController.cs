using UnityEngine;

public class QuizController : MonoBehaviour
{

    public GameObject TopicUI;
    public GameObject QuizUI;

    public int GameMode = 1; // 1 - Topic Select , 2 - Quiz
                      


    /// </summary>
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameMode == 1)
        {
            TopicUI.SetActive(true);
            QuizUI.SetActive(false);
        }
        if(GameMode == 2)
        { 
            TopicUI.SetActive(false);
            QuizUI.SetActive(true);
        }
    }
    void selectTopic()
    {

    }

    void doQuiz()
    {

    }
}
