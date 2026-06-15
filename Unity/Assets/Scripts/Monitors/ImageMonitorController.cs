using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ImageMonitorController : MonoBehaviour
{
    public StageController stc;

    public GameObject[] planes;

    private int currentIdx = 0; //현재 이미지 인덱스
    
    private bool isUpdate = true; //이미지 업데이트 플래그

    void Start()
    {

    }

    void Update()
    {
        UpdateImageMonitor();
    }
    void UpdateImageMonitor()
    {
        if (stc.isTimeOver)
        {
            ShowImage(0);
        }
        if (!stc.isGameStarted) return;

        //업데이트 상태일 때
        if (isUpdate)
        {
            //랜덤 인덱스 생성
            int randidx = 0;
            while (randidx == currentIdx) //중복시 다시 랜더마이즈
            {
                randidx = Random.Range(1, planes.Length);
            }

            //이미지 업데이트
            ShowImage(randidx);
            
        }

        //한번 업데이트 했으면 일단 정지하고 정답을 체크
        isUpdate = false;
        if ((stc.userInput == "ValveCw") && (currentIdx == 1))
        {
            stc.currentScore += 1;
            stc.userInput = "";
            isUpdate = true;

            currentIdx = 0; //인덱스 초기화
        }
        if ((stc.userInput == "LeverDown") && (currentIdx == 2))
        {
            stc.currentScore += 1;
            stc.userInput = "";
            isUpdate = true;

            currentIdx = 0; //인덱스 초기화
        }
        if ((stc.userInput == "RedButton") && (currentIdx == 3))
        {
            stc.currentScore += 1;
            stc.userInput = "";
            isUpdate = true;

            currentIdx = 0; //인덱스 초기화
        }
        if ((stc.userInput == "GreenButton") && (currentIdx == 4))
        {
            stc.currentScore += 1;
            stc.userInput = "";
            isUpdate = true;

            currentIdx = 0; //인덱스 초기화
        }
    }

    void ShowImage(int targetIdx)
    {
        for (int i = 0; i < planes.Length; i++)
        {
            if (i == targetIdx)
            {
                currentIdx = i;
                planes[i].SetActive(true);
            }
            else
            {
                planes[i].SetActive(false);
            }
        }
    }

    IEnumerator WaitUserInput()
    {
        while (true)
        {
            

            yield return null;
        }
    }
}
