using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RimColliderManager : MonoBehaviour {

    public Text ScoreText;
    int Score = 0;

	// Use this for initialization
	void Start () {
        ScoreText = GameObject.FindWithTag("Score").GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
        ScoreText.text = "Score : " + Score;
	
	}

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.name == "ball")
        {
            Score += 100;
        }
    }
}
