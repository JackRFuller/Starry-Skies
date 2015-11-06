using UnityEngine;
using System.Collections;


public class SequenceStarBehaviour : StarBaseClass {

	private bool inPlace;
	public bool spawnedFromProtoStar;

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

	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "RedGiant")
		{
			if(!spawnedFromProtoStar && !inPlace)
			{
				gameObject.SetActive(false);
			}
		}
	}

	void OnCollisionExit(Collision other)
	{
		if(other.collider.tag == "RedGiant")
		{
			if(spawnedFromProtoStar)
			{
				spawnedFromProtoStar = false;
			}
		}
	}
}
