using UnityEngine;

public class CameraController : MonoBehaviour
{
    public LeverController lvc;
    public Camera cam;

    public HandTracking htk;

    public GameObject hands;
    Vector3 initHandsPos;

    public float panelMoveDis;
    public float lerpScale;

    Vector3 centerPanelPos;
    Vector3 rightPanelPos;

    Vector3 offsetX;
    void Start()
    {
        offsetX = new Vector3(panelMoveDis, 0, 0);

        initHandsPos = hands.transform.position;

        centerPanelPos = new Vector3(0, 0, -10);
        rightPanelPos = new Vector3(0 + panelMoveDis, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        //Moving to right panel

        if (true)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, rightPanelPos, Time.deltaTime * lerpScale);
            hands.transform.position = Vector3.Lerp(hands.transform.position, initHandsPos + offsetX, Time.deltaTime * lerpScale);
        }

        //Moving to center panel

        //if (htk.rhGesture == "'Pinch'")
        //{
        //    cam.transform.position = Vector3.Lerp(cam.transform.position, centerPanelPos, Time.deltaTime * lerpScale);
        //    hands.transform.position = Vector3.Lerp(hands.transform.position, initHandsPos, Time.deltaTime * lerpScale);
        //}
    }
}
