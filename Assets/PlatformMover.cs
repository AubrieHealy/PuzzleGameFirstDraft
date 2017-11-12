using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour {

	// Use this for initialization
	void Update () {
		//LeanTween.moveLocal(gameObject, new Vector3(30f, 0f, 10f), 5f);
		if (!GameState.shouldPauseAllPlatforms)
		{
			transform.Translate(Vector3.back * Time.deltaTime * 1f);
		}
	}
}
