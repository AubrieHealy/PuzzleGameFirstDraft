using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour, IResetObject
{
	[SerializeField]
	Vector3 MovingAxis = Vector3.left;
	[SerializeField]
	float MovementDistance = 5;
	[SerializeField]
	float Speed = 3;

    private Vector3 initialPosition;
    private int uid;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
        startTween();
	}

    private void startTween()
    {
        uid = LeanTween.move(gameObject, transform.position + (MovingAxis * MovementDistance), Speed).setEase(LeanTweenType.easeInOutSine).setLoopPingPong(-1).uniqueId;
    }

    public void ResetState()
    {
        LeanTween.cancel(uid);
        transform.position = initialPosition;
        startTween();
    }
}
