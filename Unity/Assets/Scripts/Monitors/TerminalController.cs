using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class TerminalController : MonoBehaviour
{
    public TextMeshPro terminalText;

    public UIInputManager uiim;
    public StageController stc;

    [NonSerialized] public bool isTerminalWriting = false;

    const int kTimeLimit = 10;

    public int timeLeft = kTimeLimit;

    string[] textLines; //슬라이싱한 문자열을 배열 형태로 다시 저장한다.
    string originalText; //커서를 추가하기 전의 문자열을 저장한다.
    int menuNum; //메뉴 번호를 저장한다.
    int lastCursorIndex; //마지막으로 커서가 가리키던 textLine의 인덱스를 저장한다.

    WaitForSeconds wait02; //0.2초 대기
    WaitForSeconds wait05; //0.5초 대기

    private void Awake()
    {
        wait02 = new WaitForSeconds(0.2f);
        wait05 = new WaitForSeconds(0.5f);
        menuNum = 0;
        lastCursorIndex = 0;
    }
    private void Start()
    {
        StartCoroutine(UpdateTime());
    }

    private void Update()
    {
        UpdateTerminal();
    }

    void UpdateTerminal()
    {
        if (stc.isGameStarted)
        {
            terminalText.fontSize = 14f;
            terminalText.text = "점수: " + stc.currentScore + "\n" + "남은 시간: " + timeLeft + "\n" + "밸브는 시계방향으로.";
        }
        else if(stc.isQuitCheck)
        {
            terminalText.fontSize = 16f;
            terminalText.text = "정말 나가시겠습니까?\n붉은 버튼을 한번 더 눌러 종료합니다.";
        }
        else
        {
            terminalText.fontSize = 14f;
            terminalText.text = "최고점수: " + stc.currentScore + "\n\n초록 버튼을 눌러 게임 시작.\n\n붉은 버튼을 눌러 게임 종료.";
        }
    }

    IEnumerator UpdateTime()
    {
        while (true)
        {
            if (timeLeft <= 0)
            {
                //시간이 다 지났으므로 시간을 초기화해준다
                Debug.Log("시간다됐다.");
                timeLeft = kTimeLimit;

                stc.userInput = ""; //몰라 씨발 싹다 초기화해
                stc.isGameStarted = false;
                stc.isTimeOver = true;

                yield return new WaitForSeconds(1f);
            }

            if (!stc.isGameStarted)
            {
                yield return null;
                continue;
            }

            yield return new WaitForSeconds(1f);
            timeLeft -= 1;
        }
    }
    void Loop()
    {
        if (uiim.terminalUp.WasPressedThisFrame())
        {
            ChangeCursor("up");
        }
        if (uiim.terminalDown.WasPressedThisFrame())
        {
            ChangeCursor();
        }
        if (uiim.confirm.WasPressedThisFrame())
        {
            if (isTerminalWriting) return;
            switch (menuNum)
            {
                case 1:
                    StartCoroutine(NewGameTerminal());
                    break;
            }
        }
    }

    void ChangeCursor(string direction = "down")
    {
        Debug.Log("change cursor");
        string tText = terminalText.text;
        textLines = tText.Split('\n');

        int maxMenu = DetectMenuNum(textLines);
        Debug.Log("maxMenu: " + maxMenu);

        if (direction == "down") menuNum = menuNum + 1;
        else menuNum = menuNum - 1;

        for (int i = 0; i < textLines.Length; i++)
        {
            if (menuNum > 4) menuNum = 1;
            if (menuNum < 1) menuNum = 4;

            Debug.Log(menuNum);
            if (textLines[i].Contains($"[{menuNum}]"))
            {
                if (originalText != null)
                {
                    textLines[lastCursorIndex] = originalText; //이전커서 삭제
                } 

                Debug.Log("target index: " + i);
                originalText = textLines[i];
                lastCursorIndex = i;
                textLines[i] = textLines[i] + " <";
            }
        }

        tText = String.Join("\n", textLines);
        terminalText.text = tText;
    }

    int DetectMenuNum(string[] str)
    {
        int maxMenu = 0;

        foreach (string line in str)
        {
            if (Regex.IsMatch(line, @"^\[[\d]\]"))
            {
                maxMenu += 1;
            }
        }

        return maxMenu;
    }

    IEnumerator CursorIdle()
    {
        while (true)
        {
            if (isTerminalWriting)
            {
                yield return null;
                continue;
            }
            if (textLines[lastCursorIndex] == originalText)
            {
                textLines[lastCursorIndex] = originalText + " <";
            }
            else
            {
                textLines[lastCursorIndex] = originalText;
            }
            terminalText.text = String.Join("\n", textLines);
            yield return wait05;
        }
    }



    //이 밑으로는 구버전이다.
    IEnumerator BootSequence()
    {
        isTerminalWriting=true;
        terminalText.text = "";

        yield return wait05;
        yield return TypeLine("머신-스피릿에게 영광을.\n\n", 0.02f);

        yield return wait02;
        yield return TypeLine("> 로그인 성공.\n", 0.02f);

        yield return wait02;
        yield return TypeLine("> 실행 루틴을 선택하십시오:\n", 0.02f);

        yield return wait02;
        yield return TypeLine("> 조작은 모니터 왼쪽의 버튼으로 할 수 있습니다.\n\n", 0.02f);

        yield return wait02;
        yield return TypeLine("[1] 새로 하기\n", 0.02f);
        yield return TypeLine("[2] 이어서 하기(미구현)\n", 0.02f);
        yield return TypeLine("[3] 옵션(미구현)\n", 0.02f);
        yield return TypeLine("[4] 종료", 0.02f);

        ChangeCursor();
        yield return wait05;
        isTerminalWriting = false;
    }

    IEnumerator NewGameTerminal()
    {
        string text = "게임을 시작합니다..\n\n";
        yield return InitTerminal(text);
    }

    IEnumerator InitTerminal(string text) //터미널 초기화후 새 문장 입력
    {
        Debug.Log("InitTerminal Started");

        terminalText.text = "";
        isTerminalWriting = true;
        yield return TypeLine(text);
        yield return wait05;
        textLines = terminalText.text.Split("\n");

        lastCursorIndex = textLines.Length-1;
        originalText = textLines[lastCursorIndex];
        terminalText.text = String.Join("\n", textLines);
        isTerminalWriting = false;

        Debug.Log("InitTerminal Ended");
    }

    IEnumerator TypeLine(string line, float delay)
    {
        foreach (char c in line)
        {
            terminalText.text += c;
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator TypeLine(string line)
    {
        foreach (char c in line)
        {
            terminalText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
    }
}