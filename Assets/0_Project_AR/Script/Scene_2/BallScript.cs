using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;

public class BallScript : MonoBehaviour {


    public GameObject spawnPoint;
    public GameObject plane;

    public GameObject ImageTarget;

    public int score = 0;
    public Text scoreText;



    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        scoreText.text = "Score : " + score;


        if (transform.position.y < plane.transform.position.y - 50)
        {
            transform.position = spawnPoint.transform.position;
        }
	
	}
    /*void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.CompareTag("Cubic"))
        {
            Destroy(obj.gameObject);
        }
    }*/

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.CompareTag("Cubic"))
        {
            Destroy(obj.gameObject);
            score += 100;
        }
    }
}
