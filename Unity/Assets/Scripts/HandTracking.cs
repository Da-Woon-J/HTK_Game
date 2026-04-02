using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public class HandTracking : MonoBehaviour
{
    public bool debug = false;
    public float xyScale;
    public float zScale;

    public UdpReceiver ur;
    public GameObject[] leftHandObjs;
    public GameObject[] rightHandObjs;
    public GameObject leftMiddlePalm;
    public GameObject rightMiddlePalm;

    public int argNum;

    [NonSerialized] public string[] lhPoints;
    [NonSerialized] public string[] rhPoints;
    [NonSerialized] public string lhGesture;
    [NonSerialized] public string rhGesture;
    

    public void HandTrack()
    {
        if (ur == null || ur.bytes == null) return;
        if (ur.isHandData == false) return;
        (lhPoints, rhPoints) = ProcessByteHand(ur.bytes);
        if (debug)
        {
            Debug.Log(lhPoints[33]);
            Debug.Log(rhPoints[33]);
        }
        if (lhPoints[33] == null) { }
        else {
            HandObjsSync(lhPoints, leftHandObjs);
            Vector3 lhoPos0 = leftHandObjs[0].transform.position;
            Vector3 lhoPos9 = leftHandObjs[9].transform.position;
            Vector3 lmpPos = (lhoPos0 + lhoPos9) / 2;
            leftMiddlePalm.transform.position = lmpPos;

            lhGesture = lhPoints[64];
            Debug.Log(lhPoints[64]);
        }

        if (rhPoints[33] == null) { }
        else {
            HandObjsSync(rhPoints, rightHandObjs);
            Vector3 rhoPos0 = rightHandObjs[0].transform.position;
            Vector3 rhoPos9 = rightHandObjs[9].transform.position;
            Vector3 rmpPos = (rhoPos0 + rhoPos9) / 2;
            rightMiddlePalm.transform.position = rmpPos;

            rhGesture = rhPoints[64];
            //Debug.Log(lhPoints[64]);
        }
    }

    (string[], string[]) ProcessByteHand(byte[] bytes)
    {
        string data = Encoding.Default.GetString(bytes);

        //인코딩한 데이터 일단 확인
        //Debug.Log(data);

        data = data.Replace("[", " ")
                        .Replace("]", "")
                        .Replace(" ", "");

        //가공한 데이터 확인
        //Debug.Log(data);

        string[] points = data.Split(",");

        //points[0~62] : landmark position
        //points[63] : left or right
        //points[64] : gesture

        string[] lhPoints = new string[argNum];
        string[] rhPoints = new string[argNum];

        //Debug.Log(data);
        //Debug.Log(points.Length);

        if (points.Length == argNum) //only one hand
        {
            if (points[63] == "'Left'")
            {
                //Debug.Log("left hand");
                lhPoints = points;
            }
            else if (points[63] == "'Right'")
            {
                //Debug.Log("right hand");
                rhPoints = points;
            }
        }

        else if (points.Length == argNum*2) // two hands
        {
            //Debug.Log("two hands");
            //Debug.Log(points[63]);
            //Debug.Log(points[127]);

            for (int i = 0; i< argNum; i++)
            {
                lhPoints[i] = points[i];
            }
            for (int i = 0; i< argNum; i++)
            {
                rhPoints[i] = points[i+argNum];
            }

        }

        return (lhPoints, rhPoints);
    }
    void HandObjsSync(string[] points, GameObject[] handPointObjs)
    {
        for (int i = 0; i < 21; i++)
        {
            float x = float.Parse(points[i * 3]) * xyScale;
            float y = 0 - float.Parse(points[i * 3 + 1]) * xyScale;
            float z = float.Parse(points[i * 3 + 2]) * zScale;

            handPointObjs[i].transform.localPosition = new Vector3(x, y, z);
        }
    }
}