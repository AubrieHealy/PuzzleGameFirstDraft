using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Managers;

public class UndergroupdMirrorTransformScript : MonoBehaviour {
	[SerializeField]
	private GameObject[] mirrorsUnderground;
	[SerializeField]
	private GameObject[] magicCircles;
	[SerializeField]
	private Vector3[] endPositionMirrors;

	private ParticleSystem[] magicCirclesParticleSystemsFirst;
	private ParticleSystem[] magicCirclesParticleSystemsSecond;

	private bool hasUnlocked = false;
	// Use this for initialization
	void Start () {
		GameState.SecretAction += CheckForUnlocking;

		magicCirclesParticleSystemsFirst = magicCircles[0].GetComponentsInChildren<ParticleSystem>();
		magicCirclesParticleSystemsSecond = magicCircles[magicCircles.Length - 1].GetComponentsInChildren<ParticleSystem>();
	}

	void CheckForUnlocking(GameState.SecretType secretType)
	{
		if (!hasUnlocked)
		{
			if (secretType == GameState.SecretType.Level1Switch)
			{
				//Let's animate the dudes to go up
				hasUnlocked = true;
				for (int i = 0; i < magicCirclesParticleSystemsFirst.Length; i++)
				{
					magicCirclesParticleSystemsFirst[i].Play();
				}

				for (int i = 0; i < magicCirclesParticleSystemsSecond.Length; i++)
				{
					magicCirclesParticleSystemsSecond[i].Play();
				}


				StartCoroutine(playSound());
			}	
		}
	}

	public void OnMouseDown ()
	{
		//don't allow multi presses
		//AudioManager.Singleton.PlaySoundClip(AudioManager.AUDIO_SOURCE_ID.SFX_SOURCE, switchHit);
		AudioManager.Singleton.PlaySFXShortDurSound(AudioManager.SFX_SHORT_DUR_ID.LEVER_SWITCH);
		CheckForUnlocking(GameState.SecretType.Level1Switch);
	}

	IEnumerator playSound()
	{
		yield return new WaitForSeconds(.5f);
		MainCameraController.ShakeCameraEpic();
		AudioManager.Singleton.PlaySFXLongtDurSound(AudioManager.SFK_LONGISH_DUR_ID.MAGIC_PORTAL_SOUND);
		for (int i = 0; i < mirrorsUnderground.Length; i++)
		{
			LeanTween.moveLocal(mirrorsUnderground[i], endPositionMirrors[i], 1f).setEaseInCirc().setOnComplete(() => StartCoroutine(EndEffect()));
		}
	}

	IEnumerator EndEffect()
	{
		yield return new WaitForSeconds(3f);
		for (int i = 0; i < magicCirclesParticleSystemsFirst.Length; i++)
		{
			magicCirclesParticleSystemsFirst[i].Stop();
		}

		for (int i = 0; i < magicCirclesParticleSystemsSecond.Length; i++)
		{
			magicCirclesParticleSystemsSecond[i].Stop();
		}
		yield return new WaitForSeconds(.5f);

		for (int i = 0; i < magicCircles.Length; i++)
		{
			magicCircles[i].SetActive(false);
		}
	}
}
