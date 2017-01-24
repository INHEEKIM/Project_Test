using UnityEngine;
using System.Collections;

public class DrawingScript : MonoBehaviour {

    public GameObject trailPrefab;
    GameObject thisTrail;
    Vector3 StartPos;
    Plane objPlane;

    void Start()
    {
        objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);
    }

    void Update()
    {

        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
            (Input.GetMouseButtonDown(0)))
        {

            thisTrail = (GameObject)Instantiate(trailPrefab, this.transform.position, Quaternion.identity);
            thisTrail.transform.parent = transform;


            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDis;
            if (objPlane.Raycast(mRay, out rayDis))
            {
                StartPos = mRay.GetPoint(rayDis);
            }

        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) ||
            (Input.GetMouseButton(0)))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDis;
            if (objPlane.Raycast(mRay, out rayDis))
            {
                thisTrail.transform.position = mRay.GetPoint(rayDis);
            }
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) ||
            (Input.GetMouseButtonUp(0)))
        {
            if (Vector3.Distance(thisTrail.transform.position, StartPos) < 0.1)
            {
                Destroy(thisTrail);
            }
        }
    }
}
