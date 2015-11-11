using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ConstellationBehaviour : MonoBehaviour {

    [Header("Managers")]
    private LevelManager LM_Script;
	private UIManager uiScript;

    [Header("LineRenderers")]
    [SerializeField] private Transform[] starHolders;
    private List<LineRenderer> starLines = new List<LineRenderer>();

    [Header("Drawing Line Variables")]
    [SerializeField] private Transform origin;
    [SerializeField] private Transform destination;
    [SerializeField] private float drawSpeed = 6;

    private LineRenderer currentLR;
    private float counter;
    private float distance;
    private bool drawing;
    public int lineID = 0;

	private int timerToDepelete = 0;
    

	// Use this for initialization
	void Start () {

        Setup();
	
	}

    void Setup()
    {
        LM_Script = transform.parent.GetComponent<LevelManager>();
		uiScript = GameObject.Find("UIManager").GetComponent<UIManager>();

        for(int i = 0; i < starHolders.Length; i++)
        {
            starLines.Add(starHolders[i].GetChild(2).GetComponent<LineRenderer>());
        }

       
    }
	
	// Update is called once per frame
	void Update () {

        if (drawing)
        {

            if (counter < distance)
            {
                counter += .1F / drawSpeed;
                float x = Mathf.Lerp(0, distance, counter);

                Vector3 _pointA = origin.position;
                Vector3 _pointB = destination.position;

                Vector3 _pointAlongline = x * Vector3.Normalize(_pointB - _pointA) + _pointA;

                if (!starLines[lineID].enabled)
                {
                    starLines[lineID].enabled = true;
                }

                currentLR.SetPosition(1, _pointAlongline);

                if(_pointAlongline == _pointB)
                {
                    drawing = false;
                    if(lineID < starLines.Count - 1)
                    {
                        counter = 0;
                        lineID++;
                        ChooseLineToDraw();
                    }
                    if(lineID == starHolders.Length - 1)
                    {
						SendTimerToDepelete(0);
                    }
                }
            }
        }
	}

	public void SendTimerToDepelete(int _timerToDepelete)
	{
		Image _starTimer = starHolders[_timerToDepelete].GetChild(1).transform.GetChild(1).GetComponent<Image>();

		uiScript.SetTimerToDepelete(_starTimer, _timerToDepelete);

	}

    public void ChooseLineToDraw()
    {
        if(lineID < starLines.Count - 1)
        {
            origin = starHolders[lineID];
            destination = starHolders[lineID + 1];
            distance = Vector3.Distance(origin.position, destination.position);

            currentLR = starLines[lineID];
            currentLR.SetPosition(0, origin.position);           

            drawing = true;
        }
    }
}
