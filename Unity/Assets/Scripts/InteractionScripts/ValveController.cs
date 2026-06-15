using UnityEngine;

public class ValveController : MonoBehaviour
{
    public StageController stc;
    public HandTracking htk;

    public GameObject rh9;
    public GameObject lh9;
    Collider rh9Collider;
    Collider lh9Collider;

    // 현재 연결된 물리적 고무줄(SpringJoint)을 저장합니다.
    private SpringJoint currentJoint = null;

    Rigidbody valvebody;

    [Header("허접 전용 밸브 회전 설정♡")]
    public Vector3 rotationAxis = Vector3.forward; // 밸브가 회전하는 로컬 축 (보통 Z나 Y축이야!)
    public float rotationSpeedThreshold = 0.1f; // 회전으로 인정할 최소 속도 (라디안/초)
    public float requiredTurnTime = 0.3f; // 조건을 달성하기 위해 돌려야 하는 시간 (초)

    private float accumulatedDis = 0f; //속도 누적

    private void Start()
    {
        accumulatedDis = 0f;
        rh9Collider = rh9.GetComponent<Collider>();
        lh9Collider = lh9.GetComponent<Collider>();

        valvebody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 1. 손을 놨을 때 물리 연결 해제 로직
        if (currentJoint != null && htk.rhGesture != "'Grab'")
        {
            Destroy(currentJoint);
            currentJoint = null;
            Debug.Log("알림: 물리 연결을 해제했습니다.");

            accumulatedDis = 0f;
        }

        // 2. 밸브 회전 조건 체크 (매 프레임마다 감시한다구~)
        CheckValveRotation();
    }

    private void CheckValveRotation()
    {
        // 복잡한 방향 변환 다 빼버리고, 네가 확인한 월드 Z축 각속도를 바로 꽂아버려!♡
        float turnSpeed = valvebody.angularVelocity.z;

        accumulatedDis += turnSpeed;
        if (accumulatedDis > 800f)
        {
            Debug.Log("와우! 반시계방향으로 많이 돌렸네여");

            stc.userInput = "ValveCcw";
            accumulatedDis = 0f; //초기화 해준다.
        }

        if (accumulatedDis < -800f)
        {
            Debug.Log("와우! 시계방향으로 많이 돌렸네여");

            stc.userInput = "ValveCw";
            accumulatedDis = 0f; //초기화 해준다.
        }
    }

    // 큐브가 밸브 영역에 들어왔을 때 실행됩니다.
    private void OnTriggerStay(Collider other)
    {
        if (other == rh9Collider && currentJoint == null && htk.rhGesture == "'Grab'")
        {
            currentJoint = rh9.AddComponent<SpringJoint>();
            currentJoint.connectedBody = gameObject.GetComponent<Rigidbody>();

            currentJoint.autoConfigureConnectedAnchor = false;

            currentJoint.anchor = Vector3.zero;
            currentJoint.connectedAnchor = valvebody.transform.InverseTransformPoint(rh9.transform.position);

            currentJoint.spring = 5000f;   // 당기는 힘의 세기
            currentJoint.damper = 2500f;    // 덜덜거림을 방지하는 저항
            currentJoint.breakForce = Mathf.Infinity; // 연결이 끊어지지 않게 설정

            Debug.Log("알림: 밸브 본체와 물리 연결이 완료되었습니다.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other == rh9Collider && currentJoint != null)
        {
            Destroy(currentJoint);
            currentJoint = null;
            Debug.Log("알림: 물리 연결을 해제했습니다.");
        }
    }
}