using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
	private static MainCameraController camControlScript;

	[SerializeField]
	OrthoCamPos[] cameraOrientations;
	[SerializeField]
	float CamAnimationSpeed = 1f;

	void Awake () {
		camControlScript = this;
	}

	public static void SetLevelPosition(int level, bool shouldTween = true)
	{
		//adjustment for intro scene
		level++;

		if (shouldTween)
		{
			LeanTween.move(camControlScript.gameObject, camControlScript.cameraOrientations[level].transform.localPosition, camControlScript.CamAnimationSpeed).setEaseInOutSine();
			LeanTween.rotate(camControlScript.gameObject, camControlScript.cameraOrientations[level].transform.localRotation.eulerAngles, camControlScript.CamAnimationSpeed).setEaseInOutSine();
			//ortho size
			if (Camera.main.orthographicSize != camControlScript.cameraOrientations[level].OrthoSize) {
				LeanTween.value(camControlScript.gameObject,
					(float newVal) => {
						Camera.main.orthographicSize = newVal;
					}, Camera.main.orthographicSize, camControlScript.cameraOrientations[level].OrthoSize, camControlScript.CamAnimationSpeed).setEaseInOutSine();
			}
		}
		else
		{
			camControlScript.gameObject.transform.position = camControlScript.cameraOrientations[level].transform.localPosition;
			camControlScript.gameObject.transform.rotation = camControlScript.cameraOrientations[level].transform.localRotation;
			Camera.main.orthographicSize = camControlScript.cameraOrientations[level].OrthoSize;
		}
	}

	public static void IntroAnimation()
	{
		float aniSpeed = 3f;
		LeanTween.move(camControlScript.gameObject, camControlScript.cameraOrientations[1].transform.localPosition, aniSpeed).setEaseInOutSine();
		LeanTween.rotate(camControlScript.gameObject, camControlScript.cameraOrientations[1].transform.localRotation.eulerAngles, aniSpeed).setEaseInOutSine();
			//ortho size
			if (Camera.main.orthographicSize != camControlScript.cameraOrientations[1].OrthoSize) {
				LeanTween.value(camControlScript.gameObject,
					(float newVal) => {
						Camera.main.orthographicSize = newVal;
				}, Camera.main.orthographicSize, camControlScript.cameraOrientations[1].OrthoSize, aniSpeed).setEaseInOutSine();
			}
	}

	//Shakes the camera after player completes a level
	public static void ShakeCameraWin()
	{
		EZCameraShake.CameraShaker.Instance.ShakeOnce(1f, 7f, .1f, .6f, new Vector3(.25f, 0.25f, .25f), new Vector3(1f, 1f, 1f));
	}

	public static void ShakeCameraEpic()
	{
		EZCameraShake.CameraShaker.Instance.ShakeOnce(3f, 7f, .5f, .5f, new Vector3(.25f, 0.25f, .25f), new Vector3(1f, 1f, 1f));

	}

	public static void ShakeCameraMoreEpic()
	{
		EZCameraShake.CameraShaker.Instance.ShakeOnce(20f, 7f, .5f, 2f, new Vector3(.25f, 0.25f, .25f), new Vector3(1f, 1f, 1f));

	}
}
