using UnityEngine;
using System.Collections;


public class SequenceStarBehaviour : StarBaseClass {

	private bool inPlace;

	// Use this for initialization
	void Start () {

        InitialValues();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(!inPlace)
		{
			Move();
		}
        
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "StarHolder")
		{
			transform.position = other.transform.position;
			starRB.isKinematic = true;
		}
	}
}
