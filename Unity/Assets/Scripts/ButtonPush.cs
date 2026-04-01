using System;
using System.Collections;
using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;

public class ButtonPush : MonoBehaviour
{
    public GameObject indexTip;
    SphereCollider idtCollider;

    public float buttonSpeed = 0.5f;

    Vector3 initPos;
    public Vector3 endPos;          

    Coroutine activeRoutine;

    void Start()
    {
        idtCollider = indexTip.GetComponent<SphereCollider>();
        initPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == idtCollider)
        {
            if (activeRoutine != null) StopCoroutine(activeRoutine);
            activeRoutine = StartCoroutine(PushAnimation());
        }
    }

    IEnumerator PushAnimation()
    {
        while (transform.position.y > endPos.y + 0.1)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * buttonSpeed);
            yield return null;
        }
        transform.position = endPos;

        while (transform.position.y < initPos.y - 0.1)
        {
            transform.position = Vector3.Lerp(transform.position, initPos, Time.deltaTime * buttonSpeed);
            yield return null;
        }
        transform.position = initPos;
        activeRoutine = null;
    }

    private void OnCollisionExit(Collision collision)
    {

    }
}
