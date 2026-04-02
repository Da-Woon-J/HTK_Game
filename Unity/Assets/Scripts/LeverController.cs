using UnityEngine;

public class LeverController : MonoBehaviour 
{
    public float minAngle = -30;
    public float maxAngle = -150;
    private float startAngle;

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

        startAngle = transform.rotation.x;
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
            float currentAngle = GetAngle(middlePalm);
            float targetAngle = currentAngle - startAngle;

            if (targetAngle > 180) targetAngle -= 360;

            float clampedAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);

            transform.localRotation = Quaternion.Euler(clampedAngle, 0, 0);
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

    float GetAngle(GameObject middlePalm)
    {
        Vector3 handPos = middlePalm.transform.position;
        Vector3 anchor = leverAnchor.transform.position;

        Vector3 direction = handPos - anchor;

        return Mathf.Atan2(direction.y, direction.z);
    }
}
