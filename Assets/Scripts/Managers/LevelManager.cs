using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    [Header("Manager")]
    [SerializeField] private UIManager uiScript;
    [SerializeField] private ConstellationBehaviour cbScript;

    public enum levelState
    {
        Initialise,
        InProgress,
        Finished,
    }
    public levelState currentLevelState = levelState.Initialise;

    [Header("Level Specifics")]
    public string levelName;    
    public GameObject[] stars;
    public GameObject[] starHolders;
    private int activeStarHolders = 0;
    public float totalTime;
    [SerializeField] private int numOfSequenceStars;
    public int[] scoreTiers = new int[3];


    // Use this for initialization
    void Start () {

        InitialValues();
	
	}

    void InitialValues()
    {
        uiScript = GameObject.Find("UIManager").GetComponent<UIManager>();
        uiScript.InitiliaseLevelUI(this.GetComponent<LevelManager>());

        cbScript = transform.GetChild(0).GetComponent<ConstellationBehaviour>();

        foreach(GameObject star in stars)
        {
            star.GetComponent<StarBaseClass>().FreezePosition();
            if(star.tag == "SequenceStar")
            {
                numOfSequenceStars += 1;
            }
        }

        starHolders = GameObject.FindGameObjectsWithTag("StarHolder");

    }

    public void StartLevel()
    {
        currentLevelState = levelState.InProgress;
        foreach(GameObject star in stars)
        {
            star.GetComponent<StarBaseClass>().UnFreezePosition();
        }
    }
	
	// Update is called once per frame
	void Update () {

       
	}

    public void StarHolderActive()
    {
        activeStarHolders += 1;

        if(activeStarHolders == starHolders.Length)
        {
            currentLevelState = levelState.Finished;
            cbScript.ChooseLineToDraw();           
        }
    }

    public void LevelComplete()
    {
        
        foreach(GameObject star in stars)
        {
           if(star.tag != "SequenceStar")
            {
                star.SetActive(false);
            }
        }

        
    }
}
