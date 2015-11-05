using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

	public enum controlType
	{
		Keyboard,
		Mobile,
	}

	public controlType currentControlType;

	[SerializeField] private Text Direction;

    [Header("Input Values")]
	public Vector3 movementDirection;

	private bool hasInt;
	private Vector3 startDirection;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		switch(currentControlType)
		{
		case(controlType.Keyboard):
			KeyboardInput();
			break;
		case(controlType.Mobile):
			MobileInput();
			break;
		}
    }

    void MobileInput()
    {
		if(!hasInt)
		{
			startDirection.x = Input.acceleration.y;
			startDirection.y = Input.acceleration.x;
			if(startDirection.sqrMagnitude > 1)
			{
				startDirection.Normalize();
			}
			hasInt = true;
		}

		//Remap Device Orientation Axis to Game coordinates
		movementDirection.x = -Input.acceleration.y - startDirection.x;
		movementDirection.z = -Input.acceleration.x - startDirection.y;

		//Clamp Acceleration
		if(movementDirection > 1)
		{
			movementDirection.Normalize();
		}

		Direction.text = movementDirection.ToString();
    }

    void KeyboardInput()
    {
		movementDirection = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));   
		Direction.text = movementDirection.ToString();
    }
}
