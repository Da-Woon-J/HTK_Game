using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LeverController : MonoBehaviour
{
    public StageController stc;
    public HandTracking htk;

    public GameObject rmdp;
    public GameObject lmdp;
    //최대 y값과 최소 y값이다.
    public float maxYPos;
    public float minYPos;

    //레버가 출력하는 신호
    [NonSerialized] public bool isLeverDown; 

    string lhGesture;
    string rhGesture;
    //내부에서 쓸 변수

    private bool isActive = false;

    Collider rmdpCollider;
    Collider lmdpCollider;
    void Start()
    {
        rmdpCollider = rmdp.GetComponent<Collider>();
        lmdpCollider = lmdp.GetComponent<Collider>();
    }

    void Update()
    {
        //Debug.Log(transform.localPosition.y);
        if (transform.localPosition.y < minYPos + 0.7f)
        {
            isLeverDown = true;
            Debug.Log("Lever is Down");
            stc.userInput = "LeverDown";
        }
        else
        {
            isLeverDown = false;
        }
        //액티브 상태면 콜라이더 벗어나도 계속 따라옴 기모찌
        if (isActive && htk.rhGesture == "'Grab'")
        {
            LeverLogic(rmdpCollider);
        }
        else
        {
            isActive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (htk.rhGesture == "'Grab'")
        {
            isActive = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == rmdpCollider && htk.rhGesture == "'Grab'")
        {
            LeverLogic(rmdpCollider);
        }
        else if (other == lmdpCollider && htk.lhGesture == "'Grab'")
        {
            LeverLogic(lmdpCollider);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, maxYPos-0.2f, transform.localPosition.z); //초기화
    }

    void LeverLogic(Collider collider)
    {
        Vector3 op = collider.transform.position;
        Vector3 localPos = transform.parent.InverseTransformPoint(op);
        float constY = Mathf.Clamp(localPos.y, minYPos, maxYPos);

        transform.localPosition = new Vector3(transform.localPosition.x, constY, transform.localPosition.z);
    }
}