using UnityEngine;

public class HandInput : MonoBehaviour
{
    bool isPinching = false;

    public float PinchDetect(GameObject[] handPoints)
    {
        GameObject indexTip = handPoints[8];
        GameObject indexRoot = handPoints[5];
        GameObject thumbTip = handPoints[4];

        Vector3 itPos = indexTip.transform.position;
        Vector3 irPos = indexRoot.transform.position;
        Vector3 ttPos = thumbTip.transform.position;

        float pinchDis = Vector3.Distance(itPos, ttPos);
        float indexLen = Vector3.Distance(itPos, irPos);

        //Debug.Log("pinch distance:" + pinchDis);
        return pinchDis;
    }
    public void PinchOnetime(GameObject[] handPoints)
    {
        float dis = PinchDetect(handPoints);

        if (dis == 0) return;
        if(dis < 0.5 && !isPinching)
        {
            Debug.Log("pinched");
            isPinching = true;
        }
        else if (dis > 0.5)
        {
            isPinching = false;
        }
        else { }
    }

    public void PinchHold(GameObject[] handPoints)
    {
        float dis = PinchDetect(handPoints);
        if(dis < 0.5)
        {
            Debug.Log("are ya Draggin");
        }
    }

    public void PushDetect()
    {

    }
}
