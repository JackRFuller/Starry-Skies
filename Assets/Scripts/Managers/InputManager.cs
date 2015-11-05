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
    private float xAtStart = 0;
    private float yAtStart = 0;
    private float zAtStart = 0;

    private Vector3 lastPosition;
	

	// Use this for initialization
	void Start () {

        xAtStart = Input.acceleration.x;
        yAtStart = Input.acceleration.y;
        zAtStart = Input.acceleration.z;
	
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
        Vector3 _direction = Vector3.zero;

        //Remap the device acceleration axis to game coordinates
        //  1) XY plane of the device is mapped onto XZ plane
        //  2) rotated 90 degrees around Y axis

        _direction.x = (Input.acceleration.x - xAtStart);
        _direction.z = (Input.acceleration.z - zAtStart);

        //Clamp Acceleration to the unit sphere
        if (_direction.sqrMagnitude > 1)
            _direction.Normalize();

        movementDirection = _direction;

        Direction.text = movementDirection.ToString();
    }

    void KeyboardInput()
    {

        if (Input.GetMouseButton(0))
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            movementDirection = target;
        }
		//movementDirection = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));   
		//Direction.text = movementDirection.ToString();
    }
}
