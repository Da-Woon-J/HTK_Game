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
    Vector3 offsetX;
    Vector3 zeroZ;

    Vector3 centerPanelPos;
    Vector3 rightPanelPos;
    void Start()
    {
        offsetX = new Vector3(panelMoveDis, 0, 0);
        zeroZ = new Vector3(1, 1, 0);

        centerPanelPos = new Vector3(0, 0, -10);
        rightPanelPos = new Vector3(0 + panelMoveDis, 0, -10);

        initHandsPos = hands.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DebugCamMove(Transform obj)
    {
        Vector3 targetCamPos = Vector3.Scale(obj.position, zeroZ) + centerPanelPos;
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetCamPos, Time.deltaTime * lerpScale);

        Vector3 targetHandPos = Vector3.Scale(obj.position, zeroZ);
        hands.transform.position = Vector3.Lerp(hands.transform.position, targetHandPos, Time.deltaTime * lerpScale);
    }
    public void MoveCamCenter()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, centerPanelPos, Time.deltaTime * lerpScale);
        hands.transform.position = Vector3.Lerp(hands.transform.position, initHandsPos, Time.deltaTime * lerpScale);
    }

    void MoveCam()
    {
        //Moving to right panel

        if (true)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, rightPanelPos, Time.deltaTime * lerpScale);
            hands.transform.position = Vector3.Lerp(hands.transform.position, initHandsPos + offsetX, Time.deltaTime * lerpScale);
        }
    }
}
