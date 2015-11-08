﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class StarBaseClass : MonoBehaviour {

    [Header("Managers")]
    public InputManager imScript;
    public LevelManager lm_Script;

	[Header("Movement")]
    public Rigidbody starRB;
    public float speed;

	[Header("Screen Wrapping")]
	public bool wrapWidth;
	public bool wrapHeight;
	private Camera currentCamera;
	private Renderer starRenderer;
	private Transform starTransform;
	private Vector2 viewportPosition;
	private bool isWrappingHeight;
	private bool isWrappingWidth;
	private Vector3 newStarPosition;

	// Use this for initialization
	void Start () {
	
	}

    public void InitialValues()
    {
        imScript = GameObject.Find("InputManager").GetComponent<InputManager>();
        lm_Script = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		starRB.velocity = Vector3.zero;

		//Screenwrapping
		InitialiseScreenWrapping();
    }

    public void FreezePosition()
    {
        starRB.isKinematic = true;
    }

    public void UnFreezePosition()
    {
        starRB.isKinematic = false;
    }

	void InitialiseScreenWrapping()
	{
		starRenderer = GetComponent<Renderer>();
		starTransform = transform;
		currentCamera = Camera.main;
		viewportPosition = Vector2.zero;
		isWrappingHeight = false;
		isWrappingWidth = false;
		newStarPosition = starTransform.position;
	}

    public void Move()
    {
        if(lm_Script.currentLevelState == LevelManager.levelState.InProgress)
        {
            Vector3 _movementDirection = imScript.movementDirection;

            if(_movementDirection != Vector3.zero)
            {
                _movementDirection.y = transform.position.y;

                Vector3 _heading = (_movementDirection - transform.position);
                _heading *= (speed * Time.deltaTime);

                starRB.AddForce(_heading, ForceMode.Force);

                ScreenWrap();
            }
           
        }
       
    }

	private void ScreenWrap()
	{
		bool isVisible = isBeingRendered();

		if(isVisible)
		{
			isWrappingWidth = false;
			isWrappingHeight = false;
		}

		newStarPosition = starTransform.position;
		viewportPosition = currentCamera.WorldToViewportPoint(newStarPosition);

		if(wrapWidth)
		{
			if(!isWrappingWidth)
			{
				if(viewportPosition.x > 1)
				{
					newStarPosition.x = currentCamera.ViewportToWorldPoint(Vector3.zero).x;
					isWrappingWidth = true;
					PositionUpdate();
				}
				else if(viewportPosition.x < 0)
				{
					newStarPosition.x = currentCamera.ViewportToWorldPoint(Vector3.one).x;
					isWrappingWidth = true;
					PositionUpdate();
				}
			}
		}

		if(wrapHeight)
		{
			if(!isWrappingHeight)
			{
				if(viewportPosition.y > 1)
				{
					newStarPosition.z = currentCamera.ViewportToWorldPoint(Vector3.zero).z;
					isWrappingHeight = true;
					PositionUpdate();
				}
				else if(viewportPosition.y < 0)
				{
					newStarPosition.z = currentCamera.ViewportToWorldPoint(Vector3.one).z;
					isWrappingHeight = true;
					PositionUpdate();
				}
			}
		}

	}

	void PositionUpdate()
	{
		transform.position = newStarPosition;
	}

	private bool isBeingRendered()
	{
		if(starRenderer.isVisible)
			return true;
		return false;
	}
}
