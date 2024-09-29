using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GPSBearing : MonoBehaviour
{
    [SerializeField]
    private char unit = 'K';

    public TMP_Text debugTxt;
    GPSLoc startLoc = new GPSLoc();
    GPSLoc currLoc = new GPSLoc();


    public GameObject compass2d;
    public GameObject targetHand;

    public GameObject arrow3d;
    public GameObject redarrow3d;

    public double targetLon = 53.37923299069936; 
    public double targetLat = -1.128706531442641;

    public bool gps_ok = false;

    float timeDelay = 0.25f; //delay update, reduces jitter

    float magNorth = 0;
    float trueNorth = 0;
    float compassAcc = 0; //compass accuracy 


    IEnumerator Start()
    {
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location not enabled on device or app does not have permission to access location");
            debugTxt.text = "Location not enabled on device or app does not have permission to access location";
        }
        // Starts the location service.
        Input.location.Start();
        Input.compass.enabled = true;


        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            debugTxt.text += "\nTimed Out";
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            debugTxt.text += ("\nUnable to determine device location");

            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            debugTxt.text
                = "\nLocation: \nLat: " + Input.location.lastData.latitude
                + " \nLon: " + Input.location.lastData.longitude
                + " \nAlt: " + Input.location.lastData.altitude
                + " \nH_Acc: " + Input.location.lastData.horizontalAccuracy
                + " \nTime: " + Input.location.lastData.timestamp;

            gps_ok = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gps_ok)
        {
            debugTxt.text = "GPS:...";

            debugTxt.text
                = "\nLocation: \nLat: " + Input.location.lastData.latitude
                + " \nLon: " + Input.location.lastData.longitude
                + " \nH_Acc: " + Input.location.lastData.horizontalAccuracy;


            currLoc.lat = Input.location.lastData.latitude;
            currLoc.lon = Input.location.lastData.longitude;


            //get compass data
            timeDelay -= Time.deltaTime; //update every 1/4 second - reduces jitter, could average instead? 
            if (timeDelay < 0)
            {
                timeDelay = 0.25f; //reset timer
                trueNorth = Math.Abs(Input.compass.trueHeading);
                magNorth = Input.compass.magneticHeading;
                compassAcc = Input.compass.headingAccuracy;

                //update UI 
                arrow3d.transform.localEulerAngles = new Vector3(0, -(float)trueNorth, 0);
                compass2d.transform.localEulerAngles = new Vector3(0, 0, (float)trueNorth);

            }

            //Distance to target from current location
            double distanceBetween = distance((double)currLoc.lat, (double)currLoc.lon, targetLon, targetLat, 'K');
            debugTxt.text += "\nDistance: " + distanceBetween;

            //Get the bearing to the target
            double bearing = getBearing(currLoc.lat, currLoc.lon, targetLat, targetLon);

            debugTxt.text += "\nBearing to Target " + bearing;

            //calculate the offset angle 
            float waypointDir = (float)bearing - trueNorth;
            redarrow3d.transform.localEulerAngles = new Vector3(90, waypointDir, 0);
            targetHand.transform.localEulerAngles = new Vector3(0, 0, -waypointDir);
            debugTxt.text += "\nRelative Angle " + waypointDir;

       
            //output compass stuff
            debugTxt.text += "\nMag North " + magNorth;
            debugTxt.text += "\nTrue North: " + trueNorth;
            debugTxt.text += "\nHeadAcc: " + compassAcc;
        }
    }

    double getBearing(GPSLoc loc1, GPSLoc loc2)
    {
        double angle = getBearing(loc1.lat, loc1.lon, loc2.lat, loc2.lon);
        return angle;
    }


    double getBearing(double lat, double lon, double lat2, double lon2)
    {

        double dy = lat2 - lat;
        double dx = Math.Cos(Math.PI / 180 * lat) * (lon2 - lon);
        double angle = Math.Atan2(dy, dx);

        angle = angle * 180 / Math.PI;
        angle = 360 - Math.Abs(angle);

        return angle;
    }

    double calcBearing(GPSLoc loc1, GPSLoc loc2)
    {
        //https://mapscaping.com/how-to-calculate-bearing-between-two-coordinates/
        //bearing = atan2(sin(long2-long1)*cos(lat2), cos(lat1)*sin(lat2) – sin(lat1)*cos(lat2)*cos(long2-long1))

        double bearing = 0;
        double long1 = loc1.lon;
        double long2 = loc2.lon;
        double lat1 = loc1.lat;
        double lat2 = loc2.lat;

        double y = Math.Sin(long2 - long1) * Math.Cos(lat2);
        double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(long2 - long1);

        bearing = Math.Atan2(y, x);


        return bearing;
    }

    //https://www.geodatasource.com/resources/tutorials/how-to-calculate-the-distance-between-2-locations-using-c/
    private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
    {
        if ((lat1 == lat2) && (lon1 == lon2))
        {
            return 0;
        }
        else
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }
    }

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //::  This function converts decimal degrees to radians             :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private double deg2rad(double deg)
    {
        return (deg * Math.PI / 180.0);
    }

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //::  This function converts radians to decimal degrees             :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private double rad2deg(double rad)
    {
        return (rad / Math.PI * 180.0);
    }
}
