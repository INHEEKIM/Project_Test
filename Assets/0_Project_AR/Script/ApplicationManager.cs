using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour {


    public void LoadLevel(int Scene_Level)
    {
        Application.LoadLevel(Scene_Level);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
