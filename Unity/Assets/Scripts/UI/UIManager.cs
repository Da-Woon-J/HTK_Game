using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject optionsPanel;

    [NonSerialized] public bool uiStageStarted = false;
    [NonSerialized] public bool uiPause = false;
    [NonSerialized] public bool uiExitGame = false;
    void Start()
    {
        optionsPanel.SetActive(false);
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }
}