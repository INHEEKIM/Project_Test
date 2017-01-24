using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Vuforia;

public class Puzzle
{

    public GameObject Block;
    public int number;
    public bool state;

    public Puzzle(GameObject newObj, int newNumber, bool newState)
    {
        Block = newObj;
        number = newNumber;
        state = newState;
    }

}

public class PuzzleManager : MonoBehaviour
{

    public GameObject block;
    GameObject obj;
    bool objBool = false;

    Vector3 mouseBegan;
    Vector3 mouseMoved;
    Vector3 mouseEneded;
    List<Puzzle> Puzzlelist = new List<Puzzle>();

    float Speed = 0.3f;

    public void init()
    {
        /*for (int i = 0; i < 16; i++)
        {
            for(int z = 0; z > -4; z--)
            { 
                for(int x = 0; x<4; x++)
                {


                    Puzzlelist[i].Block.transform.Translate(new Vector3(x * 30, 0, z * 30));
                }
                   
            }
        }*/
        
        
    }
    // Use this for initialization
    void Start()
    {
        //block = GameObject.FindWithTag("Block");
        Vuforia.StateManager mStateManager = Vuforia.TrackerManager.Instance.GetStateManager();
        IEnumerable<Vuforia.TrackableBehaviour> activeTrackables = mStateManager.GetActiveTrackableBehaviours();
       

        int n = 0;
        for (int z = 0; z > -4; z--)
        {
            for (int x = 0; x < 4; x++)
            {


                n++;
                if (n == 16)
                {
                    Puzzle puzzle = new Puzzle(block, n, true);
                    puzzle.Block.name = "Block" + n; //블럭 고유 이름 + 넘버
                    Puzzlelist.Add(puzzle);

                }
                else {

                    Puzzle puzzle = new Puzzle(block, n, false);
                    puzzle.Block.name = "Block" + n; //블럭 고유 이름 + 넘버
                    GameObject child = Instantiate(puzzle.Block, new Vector3(x * 30, 0, z * 30), Quaternion.identity) as GameObject; //블럭 생성
                    child.transform.parent = transform;
                    Puzzlelist.Add(puzzle);


                }
                Debug.Log(Puzzlelist[n - 1].Block.name + " " + "number + " + Puzzlelist[n - 1].number + "State" + Puzzlelist[n - 1].state);

            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        //PC상에서 터치 테스트
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                obj = hit.collider.gameObject;
                if (hit.collider.CompareTag("Block"))
                {
                    Debug.Log(obj.name);
                }
            }

        }

        //모바일 상에서
        if (Input.touchCount > 0) //터치한 손가락 수
        {
            Vector2 pos = Input.GetTouch(0).position;    // 터치한 위치
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);
            Ray ray = Camera.main.ScreenPointToRay(theTouch);    // 터치한 좌표 레이로 바꿈
            

            RaycastHit hit;    // 정보 저장할 구조체 만들고

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))    // 레이저를 끝까지 쏴블자. 충돌 한넘이 있으면 return true다.
            {
                obj = hit.collider.gameObject;

                //Gameobject obj = hit.collider.gameobject;
                if (hit.collider.CompareTag("Block"))
                {

                    GameObject objchild = obj.transform.Find("Frame_Block").gameObject;
                    Debug.Log(obj.name);

                    //Vector3 screenPoint = Camera.main.WorldToScreenPoint(obj.transform.position);

                    if (Input.GetTouch(0).phase == TouchPhase.Began)    // 딱 처음 터치 할때 발생한다
                    {
                        
                        objchild.SetActive(true);
                        Debug.Log(obj.name + "Began");
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Moved)    // 터치하고 움직이면 발생한다.
                    {
                       
                        //Vector3 touchPosition = Input.GetTouch(0).position;



                        Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition; //마지막 프레임일 때 손가락의 움직임을 얻는다. deltaPosition 전 프레임 터치위치

                        //obj.transform.position = Camera.main.ScreenToWorldPoint( new Vector3(touchDeltaPosition.x, touchDeltaPosition.y, -Camera.main.transform.position.z));

                        obj.transform.Translate(touchDeltaPosition.x * Speed, 0, touchDeltaPosition.y * Speed);
                        Debug.Log(obj + "Moved");
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)    // 터치를 떼면 발생한다.
                    {
                        objchild.SetActive(false);
                        Debug.Log(obj + "Ended");
                    }
                }
            }



        }
        
    }

    /*private void UpdateWordResultPoses(Camera arCamera, IEnumerable<WordResult> ResultData)
    {
        VuforiaARController behaviour = VuforiaARController.Instance.RegisterVuforiaInitializedCallback( );
        VuforiaARController
        if (behaviour == null)
        {
            Debug.LogError("Vuforia Behaviour could not be found");
        }

        Rect viewportRectangle = behaviour.GetVideoBackgroundRectInViewPort();
        bool videoBackGroundMirrored = Behaviour.vi
                        OrientedBoundingBox cameraFrameObb = new OrientedBoundingBox()

                             Vuforia.VuforiaRuntimeUtilities.CameraFrameToScreenSpaceCoordinates(viewportRectangle);
    }
    */

}
