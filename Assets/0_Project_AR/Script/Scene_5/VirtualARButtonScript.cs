using UnityEngine;
using System.Collections;
using Vuforia;

public class VirtualARButtonScript : MonoBehaviour, IVirtualButtonEventHandler{

    public GameObject obj;

    public AudioClip clip_1;
    private AudioSource souce_1;

	// Use this for initialization
	void Start () {
        obj = GameObject.Find("VirtualButton_1");
        obj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        souce_1 = obj.GetComponent<AudioSource>();

	}
    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        Debug.Log("virtual_on");
        souce_1.PlayOneShot(clip_1);
    }
    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        Debug.Log("virtual_off");
        //souce_1.PlayOneShot(clip_1);
    }
}
