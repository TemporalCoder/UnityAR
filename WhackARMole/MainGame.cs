using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainGame : MonoBehaviour
{
    [Header("Popup Settings")]
    public TMP_Text gameData;

    GameObject[] eggs;
    float popupTimer = 1;
    static public int score = 0;
    public float GameTime = 30;

    static public bool LeftHandInUse = false;
    static public bool RightHandInUse = false;

    


    // Start is called before the first frame update
    void Start()
    {
        eggs = GameObject.FindGameObjectsWithTag("Egg");
    }

    // Update is called once per frame
    void Update()
    {
        GameTime -= Time.deltaTime;
        if(GameTime<0)
        {
            SceneManager.LoadScene("WAMStartScreen");
        }
        updateMoles();
        updateGUI();
    }

    void updateGUI()
    {
        string buffer = "Time: " + GameTime.ToString("00.0")
                            + "\nScore: " + score;        
        gameData.text = buffer; 
    }

    void updateMoles()
    {
        //edit change times for difficulty. 
        popupTimer -= Time.deltaTime;

        if (popupTimer < 0)
        {
            int rnd = Random.Range(0, eggs.Length);
            var script = eggs[rnd].GetComponent<EggJump>();
            script.pop();

            popupTimer = Random.Range(1, 3);
        }
    }
}
