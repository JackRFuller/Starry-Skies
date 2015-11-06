using UnityEngine;
using System.Collections;

public class NeutronStarBehaviour : StarBaseClass {

	private float criticalMass;
	[SerializeField] private float criticalMassLimit;
	[SerializeField] private float[] addedMasses = new float[5];
	[SerializeField] private float secondsBeforeExplosion;

	// Use this for initialization
	void Start () {

		InitialValues();
	
	}
	
	// Update is called once per frame
	void Update () {

		Move();
	
	}

	IEnumerator StartExplosion()
	{
		yield return new WaitForSeconds(secondsBeforeExplosion);
		Explode();
	}

	void Explode()
	{

	}

	void OnCollisionEnter(Collision other)
	{
		switch(other.collider.tag)
		{
		case("Protostar"):
			criticalMass += addedMasses[0];
			break;
		case("SequenceStar"):
			criticalMass += addedMasses[1];
			break;
		case("RedGiant"):
			criticalMass += addedMasses[2];
			break;
		}

		if(criticalMass >= criticalMassLimit)
		{
			StartCoroutine(StartExplosion());
		}
	}
}
