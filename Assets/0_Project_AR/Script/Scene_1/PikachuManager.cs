using UnityEngine;
using System.Collections;
using Vuforia;

public class PikachuManager : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;

    public GameObject Pikachu;

    public Animator animator;
    public static bool State = false;

    public void Trackable(bool state)
    {
        State = state;
    }

    public void OnTrackableStateChanged(
                                       TrackableBehaviour.Status previousStatus,
                                       TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Trackable(true);
        }
        else
        {
            Trackable(false);
        }
    }

    // Use this for initialization
    void Start () {
        Pikachu = GameObject.Find("Pikachu");

        //animator = GetComponent<Animator>();


        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

    }
	
	// Update is called once per frame
	void Update () {


    }
}
