using UnityEngine;
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
    [SerializeField] private Image starCounter;   
    private int starScoreID = 0;

	[Header("Score")]
	[SerializeField] private float score;
	[SerializeField] private Text scoreText;
	private float startScore;
	private float endScore;

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
            DepeleteStarHolderTimers();
        }
    }

    void DepeleteStarHolderTimers()
    {
        float _timeSinceStart = Time.time - timeStartedLerping;
        float percentageComplete = _timeSinceStart / timeTakenDuringLerping;

        timerImage.fillAmount = Mathf.Lerp(startPosition, endPosition, percentageComplete);
        

        startScore = timerImage.fillAmount;

        if (startScore <= endScore)
        {
            startScore = timerImage.fillAmount;
            endScore = timerImage.fillAmount -= 0.01F;
           
            score += 50;

            score = Mathf.Round(score);
            scoreText.text = score.ToString();

            BuildToStar();

            StartCoroutine(DetermineStarScore());
        }

        if (percentageComplete >= 1.0F)
        {
            isLerping = false;
            timerStartPosition = Mathf.RoundToInt(timerStartPosition);

            if (timerID < numOfTimers)
            {
                cbScript.SendTimerToDepelete(timerID);
            }
            else
            {
                Debug.Log("Level Fully Finished");
            }
        }
    }

    void BuildToStar()
    {
        if(starScoreID < lmScript.scoreTiers.Length)
        {
            starCounter.fillAmount += starFillRate();

            if (starCounter.fillAmount == 1 && starScoreID != 2)
            {
                starCounter.fillAmount = 0;
                starScoreID++;
            }
        }
    }

    float starFillRate()
    {
        float _diff = 0;

        if(starScoreID == 0)
        {
            _diff = lmScript.scoreTiers[starScoreID];           
        }
        else
        {
            _diff = (lmScript.scoreTiers[starScoreID] - lmScript.scoreTiers[starScoreID - 1]);
        }

        _diff *= 100;        
        _diff = (_diff / 50);
        _diff = (1 / _diff);
        Debug.Log(_diff);

        return _diff;
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
		scoreText.text = score.ToString();
		levelStartUI.Play("ResultsIn");
		StartCoroutine(StartScoreCalculator());

	}

	IEnumerator StartScoreCalculator()
	{
        starCounter.fillAmount = 0;
        yield return new WaitForSeconds(levelStartUI.GetClip("ResultsIn").length);
		cbScript.SendTimerToDepelete(0);
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

		startScore = timerImage.fillAmount;
		endScore = timerImage.fillAmount - 0.01F;

		timerID++;       
	}

	public void GameOver(string _Reason)
	{
		overlayImage.enabled = true;

		if(_Reason == "Time")
		{
			levelTitleText.text = "Out of Time";
		}

		if(_Reason == "Destory")
		{
			levelTitleText.text = "Star Destroyed";
		}

		levelStartUI.Play("GameOver");
	}
    
    IEnumerator DetermineStarScore()
    {
        if(score >= lmScript.scoreTiers[0] * 100)
        {
            stars[0].enabled = true;
        }        
        if (score >= lmScript.scoreTiers[1] * 100)
        {
            stars[1].enabled = true;
        }        
        if (score >= lmScript.scoreTiers[2] * 100)
        {
            stars[2].enabled = true;
        }

		yield return null;
    }

   
}
