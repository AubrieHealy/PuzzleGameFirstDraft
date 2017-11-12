using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
	void Start () {
		Time.timeScale = 1f;
		UnityEngine.SceneManagement.SceneManager.LoadScene("MasterScene");
	}
}
