using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGeneric : MonoBehaviour
{
    GameObject startTrans;
    Transform controller;
    bool pickedUp = false;
    char hand;


    public Quaternion RotationOffset;
    public Vector3 tempVec; 
    // Start is called before the first frame update
    void Start()
    {
        startTrans = new GameObject();

        startTrans.transform.position = transform.position;
        startTrans.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (hand == 'R')
        {
            if (OVRInput.Get(OVRInput.Button.Two) && MainGame.RightHandInUse)
            {
                MainGame.RightHandInUse = false;
                pickedUp = false;
                hand = ' ';
            }

        }
        if (hand == 'L')
        {
            if (OVRInput.Get(OVRInput.Button.Four) && MainGame.LeftHandInUse)
            {
                MainGame.LeftHandInUse = false;
                pickedUp = false;
                hand = ' ';
            }
        }



        if (pickedUp == true)
        {
            //transform = controller;

            transform.position = controller.position;

            RotationOffset.eulerAngles = tempVec;

            transform.rotation = controller.rotation *  RotationOffset;


        }
        else //remove/move!?
        {
            transform.position = startTrans.transform.position;
            transform.rotation = startTrans.transform.rotation;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RightHand" && MainGame.RightHandInUse == false)
        {
            pickedUp = true;
            this.controller = other.gameObject.transform;
            MainGame.RightHandInUse = true;
            hand = 'R';
        }

        if (other.gameObject.tag == "LeftHand" && MainGame.LeftHandInUse == false)
        {
            pickedUp = true;
            this.controller = other.gameObject.transform;
            MainGame.LeftHandInUse = true;
            hand = 'L';
        }

    }
}
