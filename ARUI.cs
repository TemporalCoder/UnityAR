using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ARUI : MonoBehaviour
{

    public List<string> infoText = new List<string>();
    public List<AudioClip> infoAudio = new List<AudioClip>();

    public Canvas canvas;
    public TMP_Text infoBox;

    AudioSource audio;

    int infoPointer = -1;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.tag == "earth")
                {
                    //do something!
                    displayCanvas();
                    infoPointer = 0;
                    displayAndPlayInfo();
                }

                if (hit.transform.tag == "mars")
                {
                    //do something!
                    displayCanvas();
                    infoPointer = 3;
                    displayAndPlayInfo();
                }
            }
        }
    }

    void displayAndPlayInfo()
    {
        infoBox.text = infoText[infoPointer];
        if (audio.isPlaying) { audio.Stop(); }
        audio.PlayOneShot(infoAudio[infoPointer], 1f);
    }

    public void nextInfo()
    {
        infoPointer++; // add 1
        displayAndPlayInfo();
        //validation!?
    }

    public void lastInfo()
    {

    }


    public void displayCanvas()
    {
        canvas.enabled = true;

    }

    public void hideCanvas()
    {
        canvas.enabled = false;
    }



}
