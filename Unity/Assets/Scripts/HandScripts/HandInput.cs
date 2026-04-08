using System;
using System.Collections;
using UnityEditor.Build;
using UnityEngine;

public class HandInput : MonoBehaviour
{
    public HandTracking ht;

    public GameObject pinchSound;

    WaitForSeconds WaitOneSec = new WaitForSeconds(1f);

    [NonSerialized] public bool isLPinching = false;
    [NonSerialized] public bool isRPinching = false;
    [NonSerialized] public bool isLGrabbing = false;
    [NonSerialized] public bool isRGrabbing = false;
    public void InputLoop()
    {
        string[] lhPoints = ht.lhPoints;
        string[] rhPoints = ht.rhPoints;

        GestureDetect(lhPoints, "'Pinch'", ref isLPinching);
        GestureDetect(rhPoints, "'Pinch'", ref isRPinching);

        GestureDetect(lhPoints, "'Grab'", ref isLGrabbing);
        GestureDetect(rhPoints, "'Grab'", ref isRGrabbing);
    }
    public void GestureDetect(string[] handPoints, string tarGesture, ref bool isDoing)
    {

        if (handPoints == null || handPoints.Length < 64) return;

        string gesture = handPoints[64];
        if (gesture == tarGesture)
        {
            if (!isDoing)
            {
                //Debug.Log("HandInput: " + tarGesture);
                pinchSound.GetComponent<AudioSource>().Play();
                isDoing = true;
            }
        }
        else
        {
            isDoing = false;
        }
    }

}
