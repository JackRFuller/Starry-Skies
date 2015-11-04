using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [Header("Timer")]
    [SerializeField] private Image timerImage;
    [SerializeField] private Text timerText;
    public float totalTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Timer(string _time)
    {
        timerText.text = _time;
        float _timeTakenOff = totalTime / 10000 * Time.deltaTime;
        timerImage.fillAmount  -= _timeTakenOff;
    }
}
