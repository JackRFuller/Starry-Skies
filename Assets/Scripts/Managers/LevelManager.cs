using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    [Header("Manager")]
    [SerializeField] private UIManager uiScript;

    [Header("Timer")]
    [SerializeField] private float totalTime;

    [Header("Gameplay")]
    [SerializeField] private bool isPlaying;

	// Use this for initialization
	void Start () {

        InitialValues();
	
	}

    void InitialValues()
    {
        uiScript = GameObject.Find("UIManager").GetComponent<UIManager>();
        uiScript.totalTime = totalTime;
    }
	
	// Update is called once per frame
	void Update () {

        if (isPlaying)
        {
            RunTimer();
        }
	
	}

    void RunTimer()
    {
        if(totalTime > 0)
        {
            totalTime -= Time.deltaTime;
            string _formattedTime = totalTime.ToString("F0");
            uiScript.Timer(_formattedTime);
        }
       
    }
}
