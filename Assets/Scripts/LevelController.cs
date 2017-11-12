using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Managers;

public class LevelController : MonoBehaviour
{
	[SerializeField]
	int levelNumber;
	[SerializeField]
	LightShaftManager lightManager;

	//All for intro animation
	[SerializeField]
	GameObject firstLight;
	[SerializeField]
	GameObject cogsWorth;
	[SerializeField]
	Transform cogsWorthWobble;
	[SerializeField]
	GameObject zzzParticles;
	[SerializeField]
	GameObject eyeballs;
	[SerializeField]
	Transform lefteye;
	[SerializeField]
	Transform righteye;

    public GameObject myCanvas;
    public Image TitleImage;

	//refs - attached during startup
	public List<Outline> objectOutlines = new List<Outline>();
	public List<Door> doors = new List<Door>();

    private IResetObject[] resettableChildren;

	void Start ()
	{
		GamePlayController.InitLevel(this, levelNumber);
        resettableChildren = GetComponentsInChildren<IResetObject>();

    }

    IEnumerator FadeCanvasOut()
    {
        yield return new WaitForSeconds(2.5f);
        myCanvas.SetActive(false);
    }

	public void SetupLevel ()
	{
		if (firstLight != null) {
			firstLight.SetActive(true);
			Invoke("StartLightManager", 0.1f);
		}
		else {
			lightManager.isPropegating = true;
		}

		switch(levelNumber) {
            case 0:
                TitleImage.CrossFadeAlpha(0.0f, 2.0f, false);
                StartCoroutine("FadeCanvasOut");
                AudioManager.Singleton.SetAudioSourceVol(AudioManager.AUDIO_SOURCE_ID.MUSIC_SOURCE, 0.3f);
                AudioManager.Singleton.PlaySFXLongtDurSound(AudioManager.SFK_LONGISH_DUR_ID.GAME_START);
                AudioManager.Singleton.PlayMusicSound(AudioManager.MUSIC_ID.MAIN_LOOP);
                AudioManager.Singleton.PlayAlwaysOnSFXSound(AudioManager.SFX_ALWAYS_ON_ID.LASER_LOOP);
				//kill zzz's
				if (zzzParticles != null) {
					Destroy(zzzParticles);
				}
                break;
            case 1:
                AudioManager.Singleton.PlaySFXLongtDurSound(AudioManager.SFK_LONGISH_DUR_ID.LEVEL_START);
                break;
            case 2:
                AudioManager.Singleton.PlaySFXLongtDurSound(AudioManager.SFK_LONGISH_DUR_ID.LEVEL_START);
                //AudioManager.Singleton.PlayMusicSound(AudioManager.MUSIC_ID.RAVE_MUSIC); - put this on skelly wall opening
                break;
            case 3:
                AudioManager.Singleton.PlaySFXLongtDurSound(AudioManager.SFK_LONGISH_DUR_ID.LEVEL_START);
                break;
            case 4:
                AudioManager.Singleton.PlaySFXLongtDurSound(AudioManager.SFK_LONGISH_DUR_ID.LEVEL_START);
                break;
            case 5:
                AudioManager.Singleton.PlaySFXLongtDurSound(AudioManager.SFK_LONGISH_DUR_ID.LEVEL_START);
                break;
            case 6:
                AudioManager.Singleton.PlaySFXLongtDurSound(AudioManager.SFK_LONGISH_DUR_ID.LEVEL_START);
                break;
            default:
            break;
		}

		//Enable MouseOver for Outline effects in this Level
		foreach (Outline oScript in objectOutlines) {
			oScript.mouseOver = true;
		}
		//Enable Doors
		foreach (Door d in doors) {
			d.ResetDoorState();
		}
	}
	void StartLightManager () {
		lightManager.isPropegating = true;
	}
	void DisableEyes () {
		LeanTween.rotate(righteye.gameObject, Vector3.back, 0.5f).setEaseInSine();
		LeanTween.rotate(lefteye.gameObject, Vector3.back, 0.5f).setEaseInSine().setOnComplete(() =>
		{
			eyeballs.SetActive(false);
		});
	}

	public void UnloadLevel ()
	{
		lightManager.isPropegating = false;
		//remove highlighters
		foreach (Outline oScript in objectOutlines) {
			oScript.mouseOver = false;
		}
	}

    public void ResetLevel()
    {
        for(int i = 0; i < resettableChildren.Length; i++)
        {
            resettableChildren[i].ResetState();
        }
    }
}
