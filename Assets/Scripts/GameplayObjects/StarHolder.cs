using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarHolder : MonoBehaviour {

    [SerializeField] private Animation starAnimations;
    [SerializeField] private Animator showText;
    [SerializeField] private string starName;
    [SerializeField] private Text starNameText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SequenceStar")
        {
            GetComponent<Collider>().enabled = false;
            starAnimations.Play("InPlace");
            if (starNameText)
            {
                StartCoroutine(ShowStarName());
                
            }
        }
    }

    IEnumerator ShowStarName()
    {
        yield return new WaitForSeconds(starAnimations.GetClip("InPlace").length);
        showText.enabled = true;
        
        
    }
}
