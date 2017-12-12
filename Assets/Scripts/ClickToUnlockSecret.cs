using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions; 
using Assets.Managers;

public class ClickToUnlockSecret : MonoBehaviour {
	[SerializeField]
	private bool shouldRemoveObject;
	[SerializeField]
	private GameState.SecretType secretType;
	[SerializeField]
	private AudioClip switchHit;
	[SerializeField]
	private SecretDoorSkeletonDance secretDoorScript;
	//refs
	Outline outlineScript;
	private bool hasPressed;

	void Awake ()
	{
		outlineScript = gameObject.GetComponent<Outline>();
        Assert.IsNotNull(outlineScript, "The the outline script is null!");
    }

	public void OnMouseDown ()
	{
		//don't allow multi presses
		if (!hasPressed) {
            //AudioManager.Singleton.PlaySoundClip(AudioManager.AUDIO_SOURCE_ID.SFX_SOURCE, switchHit);
            AudioManager.Singleton.PlaySFXShortDurSound(AudioManager.SFX_SHORT_DUR_ID.LEVER_SWITCH);

			if (secretType == GameState.SecretType.SkeletonDanceUnlock)
			{
				secretDoorScript.DoSecretEvent();
			}
            GameState.CallSecretEvent(secretType);

			if (shouldRemoveObject)
			{
				Destroy(gameObject);
			}
		}
	}
}
