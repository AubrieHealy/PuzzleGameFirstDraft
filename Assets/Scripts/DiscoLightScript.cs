using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLightScript : MonoBehaviour {
	private Light[] lights;
	private Color[] colors;
	private WaitForSeconds waitForSeconds = new WaitForSeconds(.1f);

	// Use this for initialization
	void Start () {
		colors = new Color[]
		{
			Color.red,
			Color.green,
			Color.yellow,
			Color.blue,
			Color.cyan,
			Color.magenta,
			Color.white
		};

		lights = gameObject.GetComponentsInChildren<Light>();
		StartCoroutine(startEffect());
	}

	IEnumerator startEffect()
	{
		while (true)
		{
			yield return waitForSeconds;
			for (int i = 0; i < lights.Length; i++)
			{
				lights[i].color = colors[Random.Range(0, colors.Length)];
			}
		}
	}

}
