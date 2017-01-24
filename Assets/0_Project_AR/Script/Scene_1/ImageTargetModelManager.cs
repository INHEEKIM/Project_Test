using UnityEngine;
using System.Collections;
using Vuforia;

public class ImageTargetModelManager : MonoBehaviour, ITrackableEventHandler
{

    private TrackableBehaviour mTrackableBehaviour;


    private bool isRenderer = false;
    private GameObject pikachu;
    private GameObject squirtle;
    private GameObject charmander;
    //private GameObject ball;

    private int callNumber = 0;

    // Use this for initialization
    void Start () {

        pikachu = GameObject.Find("Pikachu");
        squirtle = GameObject.Find("Squirtle");
        charmander = GameObject.Find("Charmander");
        // ball = GameObject.Find("Sphere");

        pikachu.SetActive(false);
        squirtle.SetActive(false);
        charmander.SetActive(false);

        //
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        //
    }

    public void callModels(int number)
    {
        callNumber = number;
    }

    void Update()
    {
        if (isRenderer) // 모델들이 랜더링 상태이면
        {
            //ball.SetActive(true);
            // ball.GetComponent<Rigidbody>().useGravity = true;

            if (callNumber == 1)
            {
                squirtle.SetActive(true);
                pikachu.SetActive(false);
                charmander.SetActive(false);
                squirtle.GetComponent<Animator>().SetBool("jump", true);
                squirtle.GetComponent<Animator>().SetBool("disappear", false);
            }
            else if (callNumber == 2)
            {
                squirtle.SetActive(false);
                pikachu.SetActive(true);
                charmander.SetActive(false);

                pikachu.GetComponent<Animator>().SetBool("jump", true);
                pikachu.GetComponent<Animator>().SetBool("disappear", false);
            }
            else if (callNumber == 3)
            {
                squirtle.SetActive(false);
                pikachu.SetActive(false);
                charmander.SetActive(true);


                charmander.GetComponent<Animator>().SetBool("jump", true);
                charmander.GetComponent<Animator>().SetBool("disappear", false);
            }

        }
        else if (isRenderer == false)
        {
            //ball.SetActive(false);
            // ball.transform.position = new Vector3(0, 0.2f, 0);
            // ball.GetComponent<Rigidbody>().useGravity = false;

            squirtle.SetActive(false);
            pikachu.SetActive(false);
            charmander.SetActive(false);

            squirtle.GetComponent<Animator>().SetBool("jump", false);
            squirtle.GetComponent<Animator>().SetBool("disappear", true);

            pikachu.GetComponent<Animator>().SetBool("jump", false);
            pikachu.GetComponent<Animator>().SetBool("disappear", true);

            charmander.GetComponent<Animator>().SetBool("jump", false);
            charmander.GetComponent<Animator>().SetBool("disappear", true);
        }
    }

    public void OnTrackableStateChanged(
                                TrackableBehaviour.Status previousStatus,
                                TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    private void OnTrackingFound()
    {

        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }

        if (mTrackableBehaviour.TrackableName == "pok_mon_clear_file")
        {

        }

        isRenderer = true;
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found _ Modelfind");
    }


    private void OnTrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

        if (mTrackableBehaviour.TrackableName == "pok_mon_clear_file")
        {

        }

        isRenderer = false;
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost _ Modellost");
    }

}
