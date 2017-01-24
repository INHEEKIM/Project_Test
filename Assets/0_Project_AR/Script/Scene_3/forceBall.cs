using UnityEngine;
using System.Collections;

public class forceBall : MonoBehaviour {

    public Transform cameraPosition;
    GameObject ball;
    public float power;

	// Use this for initialization
	void Start () {
        ball = this.gameObject;
        power = 100.0f;
        Vector3 mousePos = Input.mousePosition;
        cameraPosition = Camera.main.transform;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            ball.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
            rigidbody.velocity = cameraPosition.forward * power;

        }

        //미로 퍼즐 찾은 것이 있음 그것을 참고할 것
        if (Input.touchCount > 0)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            Vector3 theTouch = new Vector3(touchPos.x, touchPos.y, -5f);

            ball.transform.position = Camera.main.ScreenToWorldPoint(theTouch);
            Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
            rigidbody.velocity = cameraPosition.forward * power;

        }
	
	}
}
