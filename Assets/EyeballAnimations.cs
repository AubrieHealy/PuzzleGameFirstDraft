using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//fix light emit
//fix flench

public class EyeballAnimations : MonoBehaviour
{
	[SerializeField]
	GameObject LeftEye, RightEye, cogsHolder;
	[SerializeField]
	Transform cogWobble;
	//initial states
	Vector3 leftEyeOrginRot, rightEyeOriginRot;
	public bool disableEyes = false;

	void Awake ()
	{
		leftEyeOrginRot = LeftEye.transform.localEulerAngles;
		rightEyeOriginRot = RightEye.transform.localEulerAngles;
	}

	public void OpenEyes ()
	{
		LeftEye.SetActive(true);
		RightEye.SetActive(true);
	}

	public void CloseEyes ()
	{
		LeftEye.SetActive(false);
		RightEye.SetActive(false);
	}

	public IEnumerator BlinkEyes (float delay = 0f)
	{
		yield return new WaitForSeconds(delay);

		if (!disableEyes) {
			LeanTween.rotate(LeftEye, Vector3.back, 0.5f).setEaseInSine();
			LeanTween.rotate(RightEye, Vector3.back, 0.5f).setEaseInSine();

			yield return new WaitForSeconds(0.5f);

			CloseEyes();
			//reset eyes
			LeftEye.transform.localEulerAngles = leftEyeOrginRot;
			RightEye.transform.localEulerAngles = rightEyeOriginRot;

			yield return new WaitForSeconds(0.2f);
			if (!disableEyes) {
				OpenEyes();
				yield return new WaitForSeconds(0.5f);
				CloseEyes();
			}
		}
	}

	public void KillEyes ()
	{
		if (!disableEyes) {
			CloseEyes();
			disableEyes = true;
		}
	}

	public void AwakenReaction ()
	{
		if (LeftEye.activeInHierarchy || RightEye.activeInHierarchy) {
			//cannot wake again!
			return;
		}
		MainCameraController.ShakeCameraEpic();

		//Jolt
		LeanTween.rotateLocal(cogsHolder, cogWobble.localEulerAngles, 0.2f).setEaseOutBack().setLoopPingPong(1);
		//Animate
		OpenEyes();
		//rotate for pain
		LeanTween.rotate(LeftEye, Vector3.down, 0.2f).setEase(LeanTweenType.easeInSine).setLoopPingPong(1);
		LeanTween.rotate(RightEye, Vector3.down, 0.2f).setEase(LeanTweenType.easeInSine).setLoopPingPong(1).setOnComplete(() =>
		{
			StartCoroutine(BlinkEyes(0.5f));
		});
	}
}
