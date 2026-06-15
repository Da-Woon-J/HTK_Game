using UnityEngine;
using System.Diagnostics;
using System;
using System.Collections;

public class Main : MonoBehaviour
{
    public HandTracking htk;
    public HandInput hi;

    Process notepad;
    Process htkPyProcess;

    WaitForSeconds WaitOneSec = new WaitForSeconds(1);
    void Start()
    {
        StartCoroutine(BootHtkPy());
        StartCoroutine(MainLoop());
    }

    IEnumerator MainLoop()
    {
        //yield return BootHtkPy();
        //파이썬 같이실행, 빌드할때만 쓰는걸루 로딩 오래걸려

        while (true)
        {
            htk.HandTrack();
            hi.InputLoop();
            yield return null;
        }
    }

    IEnumerator BootHtkPy()
    {
        ProcessStartInfo htk = new ProcessStartInfo();
        string pythonPath = "D:\\PycharmFiles\\HandTracking_Pycharm\\.venv2\\Scripts\\python.exe";
        string scriptPath = "D:\\HtkProjectMain\\Python\\main.py";

        htk.FileName = pythonPath;
        htk.Arguments = scriptPath;

        htkPyProcess = Process.Start(htk);

        yield return null;
    }

    IEnumerator BootNotepad()
    {
        string path = "C:\\Users\\user\\Desktop\\고통의 역사.txt";

        if (notepad == null)
        {
            notepad = Process.Start(path);
        }

        UnityEngine.Debug.Log("Already running boi");

        yield return null;
    }

    IEnumerator QuitHtkPy()
    {
        htkPyProcess.Kill();
        yield return null;
    }

    void OnApplicationQuit()
    {
        if (htkPyProcess == null)
        {
            return;
        }
        htkPyProcess.Kill();
    }
}
