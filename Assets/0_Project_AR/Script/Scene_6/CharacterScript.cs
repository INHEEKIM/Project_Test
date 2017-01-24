using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

    private Animator charAnimator = null;
    private string charAnimName = null;

    /// <summary>
    /// /
    /// 
    /// </summary>

    public GameObject cuboidMarker;

	// Use this for initialization
	void Start () {
        charAnimator = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {



        if (cuboidMarker.transform.localEulerAngles.y > 20 && cuboidMarker.transform.localEulerAngles.y < 200)
        {
            Debug.Log(cuboidMarker.transform.localEulerAngles.y);
            this.transform.Translate(new Vector3(5.0f, 0, 0) * Time.deltaTime);
            //this.transform.Rotate(new Vector3(0,90,0),Space.World);
            //charAnimator.SetBool("param_idletowalk", true);
            //charAnimator.SetBool("param_idletoidle", false);

        }
        else if (cuboidMarker.transform.localEulerAngles.y > 310 && cuboidMarker.transform.localEulerAngles.y < 340)
        {
            Debug.Log(cuboidMarker.transform.localEulerAngles.y);
            this.transform.Translate(new Vector3(-5.0f, 0, 0) * Time.deltaTime);
            //this.transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            //charAnimator.SetBool("param_idletowalk", true);
            //charAnimator.SetBool("param_idletoidle", false);
        }
        else if (cuboidMarker.transform.localEulerAngles.x > 20 && cuboidMarker.transform.localEulerAngles.x < 200)
        {
            Debug.Log(cuboidMarker.transform.localEulerAngles.x);
            this.transform.Translate(new Vector3(0, 0, -5.0f) * Time.deltaTime);
        }
        else if (cuboidMarker.transform.localEulerAngles.x > 310 && cuboidMarker.transform.localEulerAngles.x < 340)
        {
            Debug.Log(cuboidMarker.transform.localEulerAngles.x);
            this.transform.Translate(new Vector3(0, 0, 5.0f) * Time.deltaTime);
        }
        else
        {
            //this.transform.Rotate(new Vector3(0, 0, 0), Space.Self);
            //charAnimator.SetBool("param_idletoidle", true);
            //charAnimator.SetBool("param_idletowalk", false);
        }

        //charAnimator.SetBool("idle",true);
	
	}

    void SetAllAnimationFlagsToFalse()
    {
        charAnimator.SetBool("param_idletowalk", false);
        charAnimator.SetBool("param_idletorunning", false);
        charAnimator.SetBool("param_idletojump", false);
        charAnimator.SetBool("param_idletowinpose", false);
        charAnimator.SetBool("param_idletoko_big", false);
        charAnimator.SetBool("param_idletodamage", false);
        charAnimator.SetBool("param_idletohit01", false);
        charAnimator.SetBool("param_idletohit02", false);
        charAnimator.SetBool("param_idletohit03", false);
    }
}
