using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Vuforia;

public class TargetScreenCoords : MonoBehaviour
{

    private ImageTargetBehaviour mImageTargetBehaviour = null;
    private StateManager state = TrackerManager.Instance.GetStateManager();
    //public Text TargetPositionText; //Canvas 화면 좌측 하단의 텍스트 

    // Use this for initialization
    void Start()
    {
        mImageTargetBehaviour = GetComponent<ImageTargetBehaviour>();

        if (mImageTargetBehaviour == null)
        {
            Debug.Log("not found");
        }
    }


    void TargetCoordi()
    {
        Vector2 targetSize = mImageTargetBehaviour.GetSize(); //마커타겟의 사이즈를 구하고
        float targetAspect = targetSize.x / targetSize.y; //타겟의 종횡비를 구한다음

        Vector3 pointOnTarget = new Vector3(-0.5f, 0, -0.5f / targetAspect);
        Vector3 targetPointInWorldRef = transform.TransformPoint(pointOnTarget);

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(targetPointInWorldRef); //타겟의 월드좌표 값을 화면좌표 값으로 변환

        Debug.Log("target point in screen coords: " + screenPoint.x + ", " + screenPoint.y);
        //TargetPositionText.text = "x : " + screenPoint.x + "\n" + "y : " + screenPoint.y + "\n" + "z : " + screenPoint.z;
    }

    void TargetAxisCoordi()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mImageTargetBehaviour == null)
        {
            Debug.Log("not found");
        }

        TargetCoordi();

    }



}

