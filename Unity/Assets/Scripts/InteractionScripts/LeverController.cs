using System;
using UnityEngine;

public class LeverController : MonoBehaviour
{
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
    Collider rmdpCollider;
    Collider lmdpCollider;
    void Start()
    {
        rmdpCollider = rmdp.GetComponent<Collider>();
        lmdpCollider = lmdp.GetComponent<Collider>();
    }

    void Update()
    {
        Debug.Log(transform.localPosition.y);
        if (transform.localPosition.y < minYPos + 0.7f)
        {
            isLeverDown = true;
            Debug.Log("Lever is Down");
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

    void LeverLogic(Collider collider)
    {
        Vector3 op = collider.transform.position;
        Vector3 localPos = transform.parent.InverseTransformPoint(op);
        float constY = Mathf.Clamp(localPos.y, minYPos, maxYPos);
        float smoothY = Mathf.Lerp(transform.localPosition.y, constY, Time.deltaTime * 10f);

        transform.localPosition = new Vector3(transform.localPosition.x, smoothY, transform.localPosition.z);
    }
}