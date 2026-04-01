using UnityEngine;

public class LeverController : MonoBehaviour 
{
    GameObject leverAnchor;
    public GameObject leftMiddlePalm;
    public GameObject rightMiddlePalm;
    SphereCollider lmdpCollider;
    SphereCollider rmdpCollider;

    public HandTracking htk;

    bool isGrabbing = false;

    private void Start()
    {
        leverAnchor = transform.Find("LeverAnchor").gameObject;

        lmdpCollider = leftMiddlePalm.GetComponent<SphereCollider>();
        rmdpCollider = rightMiddlePalm.GetComponent<SphereCollider>();
    }
    void Update()
    {
        string lhGesture = htk.lhGesture;
        string rhGesture = htk.rhGesture;
        LeverAction(leftMiddlePalm, lhGesture);
        LeverAction(rightMiddlePalm, rhGesture);
    }

    void LeverAction(GameObject middlePalm, string gesture)
    {
        //if (gesture != "'Grab'")
        //{
        //    return;
        //}

        if (isGrabbing)
        {
            transform.position = middlePalm.transform.position;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider == lmdpCollider)
        {
            Debug.Log("hittin left palm");
            isGrabbing = true;
        }
        if (collision.collider == rmdpCollider)
        {
            Debug.Log("hittin right palm");
            isGrabbing = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider == lmdpCollider) isGrabbing = false;
        if (collision.collider == rmdpCollider) isGrabbing = false;
    }
}
