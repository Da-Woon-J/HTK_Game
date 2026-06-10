using UnityEngine;

public class ValveController : MonoBehaviour
{
    public HandTracking htk;

    public GameObject rh9;
    public GameObject lh9;
    Collider rh9Collider;
    Collider lh9Collider;

    // 현재 연결된 물리적 고무줄(SpringJoint)을 저장합니다.
    private SpringJoint currentJoint = null;

    Rigidbody valvebody;

    private void Start()
    {
        rh9Collider = rh9.GetComponent<Collider>();
        lh9Collider = lh9.GetComponent<Collider>();

        valvebody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (currentJoint != null && htk.rhGesture != "'Grab'")
        {
            Destroy(currentJoint);
            currentJoint = null;
            Debug.Log("알림: 물리 연결을 해제했습니다.");
            return;
        }
    }

    // 큐브가 밸브 영역에 들어왔을 때 실행됩니다.
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Hello");
        // 1. 닿은 물체의 태그가 "Valve"인지 확인하고, 현재 비어있는지 체크합니다.
        if (other == rh9Collider && currentJoint == null && htk.rhGesture == "'Grab'")
        {
            
            // 2. [핵심 코드] 닿은 부위(Collider)가 속한 "진짜 물리 본체(Rigidbody)"를 가져옵니다.
            // 자식 오브젝트를 만져도 부모에 있는 물리 설정(Rigidbody)을 자동으로 찾아줍니다.

                // 3. 물리 본체를 성공적으로 찾았다면 연결을 시작합니다.
                // 4. 내 큐브에 고무줄(SpringJoint) 기능을 추가합니다.
            currentJoint = rh9.AddComponent<SpringJoint>();
            // 5. 고무줄의 끝을 밸브의 물리 본체(valveBody)에 연결합니다.
            currentJoint.connectedBody = gameObject.GetComponent<Rigidbody>();

            currentJoint.autoConfigureConnectedAnchor = false;

            currentJoint.anchor = Vector3.zero;
            currentJoint.connectedAnchor = valvebody.transform.InverseTransformPoint(rh9.transform.position);
            
            // 6. 물리적인 당기는 힘과 저항력을 설정합니다.
            currentJoint.spring = 5000f;   // 당기는 힘의 세기
            currentJoint.damper = 2500f;    // 덜덜거림을 방지하는 저항
            currentJoint.breakForce = Mathf.Infinity; // 연결이 끊어지지 않게 설정

            Debug.Log("알림: 밸브 본체와 물리 연결이 완료되었습니다.");

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other == rh9Collider)
        {
            Destroy(currentJoint);
            currentJoint = null;
            Debug.Log("알림: 물리 연결을 해제했습니다.");
        }
    } 
}