using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseManager : MonoBehaviour {

    [SerializeField] private InputManager imScript;

    public enum pauseState
    {
        paused,
        playing,
    }

    public pauseState currentPauseState;

    private GameObject[] sequenceStars;
    private Vector3[] sequenceStarVelocity;

    // Use this for initialization
    void Start () {
	
	}
    public void PauseState()
    {
        switch (currentPauseState)
        {
            case (pauseState.playing):
                Pause();
                break;
            case (pauseState.paused):
                Play();
                break;
        }
    }

    void Pause()
    {
        currentPauseState = pauseState.paused;

        imScript.SwitchTouchPoint();

        PauseSequenceStars();

        
    }

    void Play()
    {
        currentPauseState = pauseState.playing;

        imScript.SwitchTouchPoint();

        UnPauseSequenceStars();

        
    }

    void PauseSequenceStars()
    {        
        sequenceStars = GameObject.FindGameObjectsWithTag("SequenceStar");
        sequenceStarVelocity = new Vector3[sequenceStars.Length]; 

       for(int i = 0; i < sequenceStarVelocity.Length; i++)
        {
            Rigidbody _starRB = sequenceStars[i].GetComponent<Rigidbody>();
            sequenceStarVelocity[i] = _starRB.velocity;
            _starRB.isKinematic = true;
        }
    }

    void UnPauseSequenceStars()
    {
        for (int i = 0; i < sequenceStarVelocity.Length; i++)
        {
            Rigidbody _starRB = sequenceStars[i].GetComponent<Rigidbody>();
            _starRB.isKinematic = false;
            _starRB.velocity = sequenceStarVelocity[i];            
        }
    }
}
