using UnityEngine;
using System.Collections;

public class ResetManager : MonoBehaviour {	

    public void ResetLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
