using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIInputManager : UIManager
{
    public StageController stc;

    UIInput uiAction;
    InputAction clickAction;
    InputAction hoverAction;
    InputAction quitAction;
    InputAction pauseAction;
    InputAction startAction;
    InputAction debugSceneMoveAction;

    WaitForSeconds wait1sec = new WaitForSeconds(1);
    public bool debug = false;
    void Awake()
    {
        uiAction = new UIInput();
        clickAction = uiAction.UI.click;
        hoverAction = uiAction.UI.hover;
        quitAction = uiAction.UI.DebugQuit;
        pauseAction = uiAction.UI.DebugPause;
        startAction = uiAction.UI.DebugStart;
        debugSceneMoveAction = uiAction.UI.DebugCamMove;
    }
    void Start()
    {
        Debug.Log("UIInputManager started");
    }

    void Update()
    {
        if(startAction.WasPressedThisFrame())
        {
            Debug.Log(this + " :game started");
            uiStageStarted = true;
        }
        if (debugSceneMoveAction.WasPressedThisFrame())
        {
            Debug.Log("debug scene move");
            stc.debugStage += 1;
        }
    }
    void OnEnable()
    {
        uiAction.Enable();
    }

    private void OnDisable()
    {
        uiAction.Disable();
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
