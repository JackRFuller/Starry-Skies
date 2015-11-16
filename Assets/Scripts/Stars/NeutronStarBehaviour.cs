using UnityEngine;
using System.Collections;

public class NeutronStarBehaviour : StarBaseClass {

	private float criticalMass;
	[SerializeField] private float criticalMassLimit;
	[SerializeField] private float[] addedMasses = new float[5];
	[SerializeField] private float[] addedSize = new float[5];
	[SerializeField] private float secondsBeforeExplosion;
	[SerializeField] private ParticleSystem explosionSystem;
	private Animator neutronStarAnimator;

	// Use this for initialization
	void Start () {

		InitialValues();

		neutronStarAnimator = this.GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {

		Move();
	
	}

	IEnumerator StartExplosion()
	{
		starRB.velocity = Vector3.zero;
		starRB.isKinematic = true;

		neutronStarAnimator.enabled = true;
		yield return new WaitForSeconds(secondsBeforeExplosion);
		StartCoroutine(Explode());
	}

	IEnumerator Explode()
	{	
		GetComponent<MeshRenderer>().enabled = false;
		explosionSystem.enableEmission = true;
		explosionSystem.Play();

		yield return new WaitForSeconds(2F);
		explosionSystem.enableEmission = false;
		explosionSystem.Stop();
		gameObject.SetActive(false);
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag != "Terrain")
		{
			int _addedSize = 0;

			switch(other.collider.tag)
			{
			case("Protostar"):
				criticalMass += addedMasses[0];
				_addedSize = 0;
				break;
			case("SequenceStar"):
				criticalMass += addedMasses[1];
				_addedSize = 1;
				break;
			case("RedGiant"):
				criticalMass += addedMasses[2];
				_addedSize = 2;
				break;
			case("NeutronStar"):
				criticalMass += addedMasses[3];
				_addedSize = 3;
				break;
			case("SuperGiantStar"):
				criticalMass += addedMasses[4];
				_addedSize = 4;
				break;
			}

			other.gameObject.SetActive(false);

			transform.localScale = new Vector3(transform.localScale.x + addedSize[_addedSize],
			                                   transform.localScale.y + addedSize[_addedSize],
			                                   transform.localScale.z + addedSize[_addedSize]);
			
			if(criticalMass >= criticalMassLimit)
			{
				StartCoroutine(StartExplosion());
			}
		}
	}
}
