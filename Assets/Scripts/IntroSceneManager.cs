using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("Co_SplashSequence");
	}
	
    IEnumerator Co_SplashSequence()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(1);
    }
}
