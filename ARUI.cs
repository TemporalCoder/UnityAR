using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class ARUI : MonoBehaviour
{

    public List<string> infoText = new List<string>();
    public List<AudioClip> infoAudio = new List<AudioClip>();
    public List<Texture2D> imageList = new List<Texture2D>();

    public Canvas canvas;
    public TMP_Text infoBox;
    public RawImage rawImage;



    AudioSource audio;

    bool earthScaled = false;
    Vector3 earthScaleOg;

    bool marsScaled = false;
    Vector3 marsScaleOg;



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

            if (Physics.Raycast(ray, out hit, 50))
            {
                if (hit.transform.tag == "Earth")
                {
                    //do something!
                    displayCanvas();
                    infoPointer = 0;
                    displayAndPlayInfo();
                    if (earthScaled == false)
                    {
                        earthScaled = true;
                        earthScaleOg = hit.transform.localScale;
                        Vector3 scale = earthScaleOg * 1.2f;
                        hit.transform.localScale = scale;
                    }
                }
                else
                {
                    if (earthScaled == true)
                    {
                        GameObject temp = GameObject.FindGameObjectWithTag("Earth");
                        temp.transform.localScale = earthScaleOg;
                        earthScaled = false; 
                    }
                }
                


                if (hit.transform.tag == "Mars")
                {
                    //do something!
                    displayCanvas();
                    infoPointer = 3;
                    displayAndPlayInfo();

                    if (marsScaled == false)
                    {
                        marsScaled = true;
                        marsScaleOg = hit.transform.localScale;
                        Vector3 scale = marsScaleOg * 1.2f;
                        hit.transform.localScale = scale;
                    }
                }
                else
                {
                    if (marsScaled == true)
                    {
                        GameObject temp = GameObject.FindGameObjectWithTag("Mars");
                        temp.transform.localScale = marsScaleOg;
                        marsScaled = false;
                    }
                }
            }
            else
            {
                if (marsScaled == true)
                {
                    GameObject temp = GameObject.FindGameObjectWithTag("mars");
                    temp.transform.localScale = marsScaleOg;
                    marsScaled = false;
                }
                if (earthScaled == true)
                {
                    GameObject temp = GameObject.FindGameObjectWithTag("earth");
                    temp.transform.localScale = earthScaleOg;
                    earthScaled = false;
                }
            }
           
        }
    }

    void displayAndPlayInfo()
    {
        infoBox.text = infoText[infoPointer];
        if (audio.isPlaying) { audio.Stop(); }
        audio.PlayOneShot(infoAudio[infoPointer], 1f);

        rawImage.texture = imageList[infoPointer];
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
