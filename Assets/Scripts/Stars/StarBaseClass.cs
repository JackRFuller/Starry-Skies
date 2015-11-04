using UnityEngine;
using System.Collections;

[System.Serializable]
public class StarBaseClass : MonoBehaviour {

    [Header("Managers")]
    public InputManager imScript;

    public Rigidbody starRB;
    public float speed;
    

	// Use this for initialization
	void Start () {
	
	}

    public void InitialValues()
    {
        imScript = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    public void Move()
    {
        Vector3 _movementDirection = new Vector3(imScript.horizontalAxis * speed, 0, imScript.verticalAxis * speed);
        starRB.AddForce(_movementDirection, ForceMode.Force);
    }
}
