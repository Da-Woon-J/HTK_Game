using System;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public HandTracking htk;
    public GameObject rmdp;
    public GameObject lmdp;
    public float maxYPos;
    public float minYPos;

    Collider rmdpCollider;
    Collider lmdpCollider;
    string lhGesture;
    string rhGesture;
    void Start()
    {
        rmdpCollider = rmdp.GetComponent<Collider>();
        lmdpCollider = lmdp.GetComponent<Collider>();
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