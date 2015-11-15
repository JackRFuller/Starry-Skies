using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarHolder : MonoBehaviour {

    [Header("Manager")]
    [SerializeField] private LevelManager lmScript;
    private PauseManager pmScript;

    [SerializeField] private Animation starAnimations;    
    [SerializeField] private string starName;
    [SerializeField] private Text starNameText;
    

	[Header("Timer")]
	[SerializeField] private Image starTimer;
	[SerializeField] private float timer;
	private float deductionRate;
	private bool starInPlace;


	// Use this for initialization
	void Start () {

        InitialValues();
	
	}

    void InitialValues()
    {
        pmScript = GameObject.Find("PauseManager").GetComponent<PauseManager>();
    }


	// Update is called once per frame
	void Update () {

        if(pmScript.currentPauseState == PauseManager.pauseState.playing)
        {
            if (lmScript.currentLevelState == LevelManager.levelState.InProgress)
            {
                RunTimer();
            }
        }
	}

	void RunTimer()
	{
		if(!starInPlace)
		{
			if(timer > 0)
			{
				timer -= Time.smoothDeltaTime;
				starTimer.fillAmount -= 0.0167F * Time.deltaTime;
			}
			else
			{
                TriggerGameOver();
			}
		}
	}

    void TriggerGameOver()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SequenceStar")
        {
			starInPlace = true;
            GetComponent<Collider>().enabled = false;
            starAnimations.Play("InPlace");
            lmScript.StarHolderActive();
            if (starNameText)
            {
                StartCoroutine(ShowStarName());
                
            }
        }
    }

    IEnumerator ShowStarName()
    {
        yield return new WaitForSeconds(starAnimations.GetClip("InPlace").length);
        
    }
}
