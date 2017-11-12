using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Managers;

public class SecretDoorSkeletonDance : MonoBehaviour {
	[SerializeField]
	private GameState.SecretType secretTypeForThisObject;
	[SerializeField]
	private GameObject gateToOpenAfterDancing;
	[SerializeField]
	private AudioClip gateOpeningSoundEffect;
	[SerializeField]
	private AudioClip stonewallOpening;
	[SerializeField]
	private AudioClip discoMusic;

	// Use this for initialization
	void Start () {
	}

	public void DoSecretEvent()
	{
		//AudioManager.Singleton.PlaySoundClip(AudioManager.AUDIO_SOURCE_ID.SFX_SOURCE, stonewallOpening);
		//AudioManager.Singleton.PlaySoundClip(AudioManager.AUDIO_SOURCE_ID.SFX_SOURCE, discoMusic);
        AudioManager.Singleton.PlaySFXShortDurSound(AudioManager.SFX_SHORT_DUR_ID.STONE_DOOR_MOVE);
        AudioManager.Singleton.SetAudioSourceVol(AudioManager.AUDIO_SOURCE_ID.MUSIC_SOURCE, .2f);
        AudioManager.Singleton.PlayMusicSound(AudioManager.MUSIC_ID.RAVE_MUSIC);
		MainCameraController.ShakeCameraEpic();

        LeanTween.moveLocal(this.gameObject, new Vector3(32.77367f, 9.25f, -28.12169f), 1f).setOnComplete(() => StartCoroutine(beginTransitionToNextLevel()));
	}

	IEnumerator beginTransitionToNextLevel()
	{
		GameState.shouldSpawnPlatforms = true;
		yield return new WaitForSeconds(4f);
		AudioManager.Singleton.PlaySoundClip(AudioManager.AUDIO_SOURCE_ID.SFX_SOURCE, gateOpeningSoundEffect);
		LeanTween.moveLocal(gateToOpenAfterDancing, new Vector3(gateToOpenAfterDancing.transform.localPosition.x, -10f, gateToOpenAfterDancing.transform.localPosition.z), 1f);
	}
}
