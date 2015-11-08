using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    [Header("Managers")]
    [SerializeField] private UIManager uiScript;

    [Header("Input Values")]
	public Vector3 movementDirection;
    private Vector3 lastPosition;
	

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

        if (uiScript.hasStarted)
        {
            if (Input.GetMouseButton(0))
            {
                PlayerInput();
            }
        }
    }
    
    void PlayerInput()
    {
//	    Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//	    movementDirection = target;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			if(hit.collider.tag == "Terrain")
			{
				movementDirection = hit.point;
			}
		}
    }
}
