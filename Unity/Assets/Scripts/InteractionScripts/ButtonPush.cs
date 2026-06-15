using System;
using System.Collections;
using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;

public class ButtonPush : MonoBehaviour
{
    public StageController stc;

    public GameObject indexTip;
    SphereCollider idtCollider;

    public float buttonSpeed = 0.5f;
    public float buttonDepth;

    Vector3 initPos;
    Vector3 endPos;

    Coroutine activeRoutine;

    void Start()
    {
        idtCollider = indexTip.GetComponent<SphereCollider>();
        initPos = transform.position;
        endPos = new Vector3(transform.position.x, transform.position.y - buttonDepth, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision) //버튼 접촉 = 버튼 눌림
    {
        if (collision.collider == idtCollider)
        {
            if (activeRoutine != null) StopCoroutine(activeRoutine);
            activeRoutine = StartCoroutine(PushAnimation());

            stc.userInput = this.name;
            Debug.Log(this.name);
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
