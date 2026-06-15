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
    Transform rButton;
    Transform bButton;

    //현재 점수
    [NonSerialized]
    public int currentScore = 0;

    //유저 입력
    [NonSerialized]
    public string userInput;

    //게임 상태
    [NonSerialized]
    public bool isGameStarted = false;
    public bool isGameQuit = false;
    public bool isTimeOver = false;
    public bool isQuitCheck = false;

    public int debugStage = -1;
    void Start()
    {
        lever = Interactibles.transform.Find("Lever");
        valve = Interactibles.transform.Find("Valve");
        rButton = Interactibles.transform.Find("RedButton");
        bButton = Interactibles.transform.Find("BlueButton");

        Debug.Log("stagecontroller started");
        DebugMainMenu();
    }
    private void Update()
    {
        UpdateStage();
    }

    void UpdateStage()
    {
        if (userInput == "GreenButton" && !isGameStarted && !isTimeOver && !isQuitCheck) //게임시작 조건
        {
            currentScore = 0;
            userInput = ""; //초기화
            isGameStarted = true;
        }

        if (userInput == "RedButton" && !isGameStarted && !isTimeOver) //게임나가는 조건
        {
            userInput = "";
            isQuitCheck = true;  
        }

        if (userInput == "RedButton" && !isGameStarted && isQuitCheck)
        {
            Debug.Log("애플리케이션을 종료한다.");
            Application.Quit();
        }

        if (userInput == "GreenButton" && !isGameStarted && isQuitCheck)
        {
            userInput = "";
            isQuitCheck = false;
        }

        if (isTimeOver)
        {
            isGameStarted = false;
            isTimeOver = false; //초기화
        }
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
            camc.DebugCamMove(rButton);
        }

        if (debugStage % 3 == 2)
        {
            camc.DebugCamMove(valve);
        }
    }
} 
