using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    [Header("Input Values")]
    public float horizontalAxis;
    public float verticalAxis;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR
        KeyboardInput();
#endif

#if UNITY_ANDROID
        //MobileInput();
#endif

    }

    void MobileInput()
    {
        horizontalAxis = Input.acceleration.x;
        verticalAxis = Input.acceleration.y;       
    }

    void KeyboardInput()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        Debug.Log("Success");
    }
}
