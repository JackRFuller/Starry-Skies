using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class StarBaseClass : MonoBehaviour {

    [Header("Managers")]
    public InputManager imScript;

    public Rigidbody starRB;
    public float speed;

	public Text DebugText;
    

	// Use this for initialization
	void Start () {
	
	}

    public void InitialValues()
    {
        imScript = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    public void Move()
    {
		Vector3 _movementDirection = imScript.movementDirection * speed;
		DebugText.text = _movementDirection.ToString();
        starRB.AddForce(_movementDirection, ForceMode.Force);
    }
}
