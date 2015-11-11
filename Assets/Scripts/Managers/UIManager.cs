﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [Header("Managers")]
    public LevelManager lmScript;
	private ConstellationBehaviour cbScript;


    [Header("Initial Level Screen")]
    [SerializeField] private Image overlayImage;
    [SerializeField] private Text levelTitleText;
    [SerializeField] private Animation levelStartUI;
    [SerializeField] private GameObject playButton;
    public bool hasStarted;

    [Header("Timer Lerping Variables")]
    [SerializeField] private float timeTakenDuringLerping;
    private bool isLerping;
    private float startPosition;
    private float timerStartPosition;
    private float endPosition;
    private float timeStartedLerping;
	private Image timerImage;
	public int numOfTimers;
	public int timerID;

    [Header("Stars")]
    [SerializeField] private Image[] stars;

	// Use this for initialization
	void Start () {

        
	
	}

    public void InitiliaseLevelUI(LevelManager _lmScript)
    {
        lmScript = _lmScript;
		cbScript = _lmScript.transform.GetChild(0).GetComponent<ConstellationBehaviour>();

        int _starCount = 0;
        foreach(Image star in stars)
        {            
            Text _pointsNeeded = star.transform.parent.GetChild(1).GetComponent<Text>();
            _pointsNeeded.text = (lmScript.scoreTiers[_starCount] * 100).ToString();
            _starCount++;
        }

        overlayImage.enabled = true;
        levelTitleText.text = _lmScript.levelName;
        levelStartUI.Play("MainButtonIn");

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (isLerping)
        {
            float _timeSinceStart = Time.time - timeStartedLerping;
            float percentageComplete = _timeSinceStart / timeTakenDuringLerping;

            timerImage.fillAmount = Mathf.Lerp(startPosition, endPosition, percentageComplete);
            float _timerText = Mathf.Lerp(timerStartPosition, endPosition, percentageComplete);
            //timerText.text = _timerText.ToString("F0");

            if(percentageComplete >= 1.0F)
            {
                isLerping = false;
                timerStartPosition = Mathf.RoundToInt(timerStartPosition);

				if(timerID < numOfTimers)
				{
					cbScript.SendTimerToDepelete(timerID);
				}
				else
				{
					Debug.Log("Game Finished");
				}                
            }
        }
    }

    public void StartLevel()
    {
        levelStartUI.Play("MainButtonOut");
        overlayImage.enabled = false;

        lmScript.StartLevel();
        hasStarted = true;
		numOfTimers = lmScript.starHolders.Length;
    }

	public void ShowResults()
	{
		levelStartUI.Play("ResultsIn");
	}
   
	public void SetTimerToDepelete(Image _depeletingTimer, int timer)
	{
		timerID = timer;

		timerImage = _depeletingTimer;
		isLerping = true;
		timeStartedLerping = Time.time;
		
		startPosition = timerImage.fillAmount;
		timerStartPosition = lmScript.totalTime;
		endPosition = 0;

		timerID++;
	}

    IEnumerator DetermineStarScore()
    {
        if(timerStartPosition >= lmScript.scoreTiers[0])
        {
            stars[0].enabled = true;
        }
        yield return new WaitForSeconds(0.5F);
        if (timerStartPosition >= lmScript.scoreTiers[1])
        {
            stars[1].enabled = true;
        }
        yield return new WaitForSeconds(1F);
        if (timerStartPosition >= lmScript.scoreTiers[2])
        {
            stars[2].enabled = true;
        }
    }

   
}
