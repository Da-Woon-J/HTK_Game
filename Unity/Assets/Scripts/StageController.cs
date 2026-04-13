using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public CameraController camc;

    public GameObject Interactibles;
    Transform lever;
    Transform valve;
    Transform button;

    public int currentStage = 0;
    public int debugStage = 0;
    void Start()
    {
        lever = Interactibles.transform.Find("Lever");
        valve = Interactibles.transform.Find("Valve");
        button = Interactibles.transform.Find("TestButton");

        Debug.Log("stagecontroller started");
        DebugMainMenu();
    }
    private void Update()
    {
        DebugMoveCam();
    }

    void DebugMainMenu()
    {
        Debug.Log("main menu on");

    }
    void DebugMoveCam()
    {
        if (debugStage % 3 == 0)
        {
            camc.MoveCamCenter();
        }

        if (debugStage % 3 == 1)
        {
            camc.DebugCamMove(button);
        }

        if (debugStage % 3 == 2)
        {
            camc.DebugCamMove(valve);
        }
    }

    void DebugPauseMenu()
    {

    }

    void StageSelector()
    {
        Debug.Log("current stage:" + currentStage);

        if (currentStage == 0)
        {
            Debug.Log("clear tutorial to pass stage");
        }

        if (currentStage == 1)
        {
            Stage1();
        }
    }
    void Stage1()
    {

    }

}
