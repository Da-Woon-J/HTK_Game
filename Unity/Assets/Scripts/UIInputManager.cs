using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIInputManager : MonoBehaviour
{
    UIInput uiAction;
    InputAction clickAction;
    InputAction hoverAction;

    WaitForSeconds wait1sec = new WaitForSeconds(1);
    public bool debug = false;
    void Awake()
    {
        uiAction = new UIInput();
        clickAction = uiAction.UI.click;
        hoverAction = uiAction.UI.hover;
    }
    
    void Start()
    {
        Debug.Log("UIInputManager started");

        StartCoroutine(MousePos());
    }

    void Update()
    {
        
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
