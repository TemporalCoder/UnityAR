using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIandSound : MonoBehaviour
{

    public TMP_Text infoBox;

    public AudioClip Marsclip1;
    public AudioClip Marsclip2;
    public AudioClip Marsclip3;

    AudioSource audio;

    string UIState = "void"; //starting point
    int infoID = 1; //pointer to info text


    string marsText1 = "Mars, our closest neighbour and possibly humanities next home";
    string marsText2 = "Mars is also known as the Red Planet \n" 
                        +"Mars is named after the Roman god of war\n"
                        +"Mars is smaller than Earth with a diameter of 4217 miles";
    string marsText3 = "toDo";



    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();     
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
                if (hit.transform.tag == "mars")
                {                   
                    UIState = "Mars";
                    infoID = 1;
                    infoBox.text = marsText1;
                    audio.PlayOneShot(Marsclip1, 1);
                }

                if (hit.transform.tag == "earth")
                {
                    infoBox.text = "Earth \n  Mostly Harmless!";
                    UIState = "Earth";
                }


                if (hit.transform.tag == "marsInfo")
                {
                    Destroy(hit.transform.gameObject);
                    infoBox.text = "Select a Planet";
                    UIState = "void";

                }


                if (hit.transform.tag == "earthInfo")
                {
                    Destroy(hit.transform.gameObject);
                    infoBox.text = "Select a Planet";
                    UIState = "void";

                }

            }
        }
    }

    public void nextButton()
    {
        if(audio.isPlaying)
        {
            audio.Stop();
        }
        
        if (UIState == "Mars")
        {
            switch(infoID)
            { 
                case(1):
                    infoID = 2;
                    infoBox.text = marsText2;
                    audio.PlayOneShot(Marsclip2, 1f);
                    break;
                case (2):        
                    infoID = 3;
                    infoBox.text = marsText3;
                    //audio.PlayOneShot(Marsclip3, 1f); //To Add
                    break;
                default:
                    break;
            }
        }

        if (UIState == "Earth")
        {

        }
    }
}
