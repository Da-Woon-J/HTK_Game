using UnityEngine;

public class HandCylinderDrawer : MonoBehaviour
{
    [System.Serializable]
    public class FingerChain
    {
        public string name;
        public Transform[] nodes; // 연결할 랜드마크들 (0, 1, 2, 3, 4 등)
        [HideInInspector] public GameObject[] cylinders;
    }

    public FingerChain[] fingers;
    public float thickness = 0.02f; // 원통 두께
    public Material boneMaterial;   // 원통에 입힐 재질

    void Start()
    {
        foreach (var finger in fingers)
        {
            // 마디 사이의 개수만큼 원통 생성 (점 5개면 원통 4개)
            finger.cylinders = new GameObject[finger.nodes.Length - 1];
            for (int i = 0; i < finger.cylinders.Length; i++)
            {
                GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                cylinder.name = finger.name + "_Bone_" + i;
                cylinder.transform.SetParent(this.transform);
                
                // 콜라이더가 필요 없으면 삭제 (성능 최적화)
                Destroy(cylinder.GetComponent<CapsuleCollider>());
                
                if (boneMaterial != null)
                    cylinder.GetComponent<Renderer>().material = boneMaterial;
                
                finger.cylinders[i] = cylinder;
            }
        }
    }

    void LateUpdate()
    {
        foreach (var finger in fingers)
        {
            for (int i = 0; i < finger.cylinders.Length; i++)
            {
                Transform start = finger.nodes[i];
                Transform end = finger.nodes[i + 1];

                if (start != null && end != null)
                {
                    UpdateCylinder(finger.cylinders[i], start.position, end.position);
                }
            }
        }
    }

    void UpdateCylinder(GameObject cylinder, Vector3 startPos, Vector3 endPos)
    {
        Vector3 dir = endPos - startPos;
        float distance = dir.magnitude;

        // 1. 위치: 두 점의 중간 지점
        cylinder.transform.position = startPos + (dir / 2.0f);

        // 2. 회전: 방향에 맞춰 정렬 (유니티 원통은 Y축이 위아래임)
        cylinder.transform.up = dir;

        // 3. 스케일: 두께는 thickness, 길이는 거리의 절반 (유니티 기본 원통 높이가 2임)
        cylinder.transform.localScale = new Vector3(thickness, distance / 2.0f, thickness);
    }
}