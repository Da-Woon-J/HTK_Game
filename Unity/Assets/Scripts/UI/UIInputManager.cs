using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIInputManager : UIManager
{
    public StageController stc;

    public UIInput uiAction;
    InputAction clickAction;
    InputAction hoverAction;
    InputAction quitAction;
    InputAction pauseAction;
    InputAction debugSceneMoveAction;
    [NonSerialized] public InputAction confirm;
    [NonSerialized] public InputAction terminalUp;
    [NonSerialized] public InputAction terminalDown;

    WaitForSeconds wait1sec = new WaitForSeconds(1);

    public bool debug = false;

    public bool isPaused = false;

    void Awake()
    {
        uiAction = new UIInput();
        clickAction = uiAction.UI.click;
        hoverAction = uiAction.UI.hover;
        quitAction = uiAction.UI.DebugQuit;
        pauseAction = uiAction.UI.DebugPause;
        confirm = uiAction.UI.Confirm;
        debugSceneMoveAction = uiAction.UI.DebugCamMove;
        terminalUp = uiAction.UI.TerminalUp;
        terminalDown = uiAction.UI.TerminalDown;
    }
    void Start()
    {
        uiAction.Enable();
    }
    void Update()
    {
        DebugUIActions();
    }

    void DebugUIActions()
    {
        if (debugSceneMoveAction.WasPressedThisFrame())
        {
            DebugSceneMove();
        }

        if (pauseAction.WasPressedThisFrame())
        {
            PauseGame();
        }
        if (quitAction.WasPressedThisFrame())
        {
            QuitGame();
        }

    }

    void StartGame()
    {
        Debug.Log(this + " :game started");
        uiStageStarted = true;
    }

    void DebugSceneMove()
    {
        Debug.Log("debug scene move");
        stc.debugStage += 1;
    }

    void QuitGame()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

    void PauseGame()
    {
        if (isPaused == true)
        {
            Time.timeScale = 1;
            isPaused = false;
            return;
        }

        Time.timeScale = 0;
        isPaused = true;
    }

    private IEnumerator MousePos()
    {
        while (true)
        {
            Vector2 mousePos = hoverAction.ReadValue<Vector2>();
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (debug)
            {
                Debug.Log(mouseWorldPos);
                yield return wait1sec;
            }
            yield return null;
        }
    }

}
