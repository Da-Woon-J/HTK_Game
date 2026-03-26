using System.Collections;
using UnityEngine;

public class HandInput : MonoBehaviour
{
    public HandTracking ht;

    public GameObject pinchSound;

    WaitForSeconds WaitOneSec = new WaitForSeconds(1f);

    bool isLPinched = false;
    bool isRPinched = false;

    public void InputLoop()
    {
        string[] lhPoints = ht.lhPoints;
        string[] rhPoints = ht.rhPoints;

        PinchInput(lhPoints, ref isLPinched);
        PinchInput(rhPoints, ref isRPinched);
    }
    public void PinchInput(string[] handPoints, ref bool isPinched)
    { 
        
        if (handPoints.Length < 64 || handPoints == null)
        {
            return;
        }

        string gesture = handPoints[64];
        if (gesture == "'Pinch'")
        {
            if (!isPinched)
            {
                Debug.Log("HandInput: Pinch");
                pinchSound.GetComponent<AudioSource>().Play();
                isPinched = true;
            }
        }
        else
        {
            isPinched = false;
        }
    }

    public void PushDetect()
    {

    }
}
