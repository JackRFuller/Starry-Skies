using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [Header("Managers")]
    public LevelManager lmScript;

    [Header("Initial Level Screen")]
    [SerializeField] private Image overlayImage;
    [SerializeField] private Text levelTitleText;
    [SerializeField] private Animation levelStartUI;
    [SerializeField] private GameObject playButton;
    public bool hasStarted;

    [Header("Timer")]
    [SerializeField] private Image timerImage;
    [SerializeField] private Animation timerAnimation;
    [SerializeField] private Text timerText;
    public float totalTime;

    [Header("Timer Lerping Variables")]
    [SerializeField] private float timeTakenDuringLerping;
    private bool isLerping;
    private float startPosition;
    private float timerStartPosition;
    private float endPosition;
    private float timeStartedLerping;

    [Header("Stars")]
    [SerializeField] private Image[] stars;

	// Use this for initialization
	void Start () {

        
	
	}

    public void InitiliaseLevelUI(LevelManager _lmScript)
    {
        lmScript = _lmScript;

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
            timerText.text = _timerText.ToString("F0");

            if(percentageComplete >= 1.0F)
            {
                isLerping = false;
                timerStartPosition = Mathf.RoundToInt(timerStartPosition);
                timerText.text = (timerStartPosition * 100).ToString("F0");

                StartCoroutine(DetermineStarScore());
            }
        }
    }

    public void StartLevel()
    {
        levelStartUI.Play("MainButtonOut");
        overlayImage.enabled = false;

        lmScript.StartLevel();
        hasStarted = true;
    }

    public IEnumerator ResultsScreenIn()
    {
        levelStartUI.Play("ResultsIn");
        yield return new WaitForSeconds(levelStartUI.GetClip("ResultsIn").length);
        timerAnimation.Play();
        yield return new WaitForSeconds(timerAnimation.GetClip("TimerToCentre").length);
        DepeleteTimer();
    }

    public void Timer(string _time)
    {
        timerText.text = _time;        
        timerImage.fillAmount  -= 0.01F * Time.deltaTime;
    }

    void DepeleteTimer()
    {
        isLerping = true;
        timeStartedLerping = Time.time;

        startPosition = timerImage.fillAmount;
        timerStartPosition = lmScript.totalTime;
        endPosition = 0;
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
