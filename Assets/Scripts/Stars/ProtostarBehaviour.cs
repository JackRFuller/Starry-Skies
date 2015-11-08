using UnityEngine;
using System.Collections;

public class ProtostarBehaviour : StarBaseClass {

	[SerializeField] private GameObject SequenceStar;
	private bool hasTransformed;

	// Use this for initialization
	void Start () {

		InitialValues();
	
	}
	
	// Update is called once per frame
	void Update () {

		Move ();
	
	}

	void TransformIntoSequence()
	{
		GameObject newStar = Instantiate(SequenceStar,transform.position,transform.rotation) as GameObject;
        
		SequenceStarBehaviour ssScript = newStar.GetComponent<SequenceStarBehaviour>();
		ssScript.spawnedFromProtoStar = true;

		newStar.GetComponent<Rigidbody>().velocity = starRB.velocity;       
		gameObject.SetActive(false);
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "RedGiant")
		{
			if(!hasTransformed)
			{
				TransformIntoSequence();
				hasTransformed = true;
			}
		}
	}


}
