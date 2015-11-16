using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    [Header("Managers")]
    [SerializeField] private UIManager uiScript;
    [SerializeField] private PauseManager pmScript;

    [Header("Input Values")]
	public Vector3 movementDirection;
    private Vector3 lastPosition;
	[SerializeField] private SpriteRenderer touchPoint;
	

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

        if (pmScript.currentPauseState == PauseManager.pauseState.playing)
        {
            if (uiScript.hasStarted)
            {

               
                if (Input.GetMouseButton(0))
                {
                    PlayerInput();
                    
                }
                if (Input.GetMouseButtonUp(0))
                {
                    touchPoint.enabled = false;
                    movementDirection = Vector3.zero;
                }
            }
        }
    }

    public void SwitchTouchPoint()
    {
        switch (pmScript.currentPauseState)
        {
            case (PauseManager.pauseState.playing):
                touchPoint.enabled = true;
                break;
            case (PauseManager.pauseState.paused):
                touchPoint.enabled = false;
                break;
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
				touchPoint.enabled = true;
				touchPoint.transform.position = hit.point;
			}
		}
    }
}
