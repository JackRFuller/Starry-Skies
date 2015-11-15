using UnityEngine;
using System.Collections;

public class DevelopmentManager : MonoBehaviour {

    public string levelToLoad;

	void Awake()
    {
        Application.LoadLevelAdditive(levelToLoad);
    }
}
