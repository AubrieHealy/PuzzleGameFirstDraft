using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Managers;

public class GameState : MonoBehaviour
{
    public static GameState instance;
	public static bool shouldPauseAllPlatforms = false;
    LTRect fadeOut = new LTRect(0,0,Screen.width,Screen.height,0f);
	LTRect fadeInText = new LTRect(0,0,Screen.width,Screen.height,0f);
	LTRect clickToRestart = new LTRect(0,0,Screen.width,Screen.height,0f);

    Texture2D fadeTex;
    GUIStyle gameOverStyle;

	[SerializeField]
	public Texture2D GameOverTex;

    [SerializeField]
    UnityStandardAssets.ImageEffects.NoiseAndGrain noiseFilter;
          
    public float CameraMoveTime = 1f;
    public int Levels;
	public static bool shouldSpawnPlatforms = false;

    private int level = -1; //title scene
    public static int CurrentLevel {
    	get {
    		return instance.level;
    	}
    }

	#region Secret Event States
	public static event Action<SecretType> SecretAction;

	public enum SecretType
	{
		SkeletonDanceUnlock,
		CrisApartmentUnlock,
		Level1Switch
	}
	#endregion

    void Awake()
    {
        if(instance == null) {
            instance = this;
        }
    }

    void Start() {
    	//config
		Texture2D blackTex = new Texture2D(1,1);
        blackTex.SetPixel(0,0, Color.black);
        blackTex.Apply();
        fadeTex = blackTex;
		gameOverStyle = new GUIStyle();
		gameOverStyle.font = (Font)Resources.Load("Fonts/KOMIKAX_", typeof(Font));
		gameOverStyle.alignment = TextAnchor.MiddleCenter;
		gameOverStyle.normal.textColor = Color.white;
		gameOverStyle.fontSize = 20;

		//setup
        MainCameraController.SetLevelPosition(level, false);
        AudioManager.Singleton.SetAudioSourceVol(AudioManager.AUDIO_SOURCE_ID.MUSIC_SOURCE, 1.0f);
        AudioManager.Singleton.PlayMusicSound(AudioManager.MUSIC_ID.INTRO_LOOP);
    }

    void Update() {
		//Start Screen
		if (level == -1 && Input.GetMouseButtonDown(0)) {
			level = 0;
			MainCameraController.IntroAnimation();
			GamePlayController.SetLevelConfig(level);
		}
    }


    void OnGUI()
    {
    	if (fadeOut.alpha > 0f) {
    		GUI.DrawTexture(fadeOut.rect, fadeTex);
			//GUI.Label(fadeInText.rect, "You killed Cogsworth...", gameOverStyle);
			GUI.DrawTexture(fadeInText.rect, GameOverTex);

			if ((fadeInText.alpha == 1f || clickToRestart.alpha > 0f) && GUI.Button(fadeInText.rect, GUIContent.none, GUIStyle.none)) {
				RestartGame();
			}

			if (clickToRestart.alpha > 0f)
			{
				GUI.Label(clickToRestart.rect, "- click to restart - ", gameOverStyle);
			}
    	}

#if DOPE_CHEATS
        if(GUI.Button(new Rect(10, 70, 100, 30), "Prev Level"))
        {
            decreaseLevel();
        }
        if (GUI.Button(new Rect(10, 100, 100, 30), "Next Level"))
		{
			AdvanceLevel(level + 1);
		}
		if (GUI.Button(new Rect(10, 130, 100, 30), "CameraShake"))
		{
			MainCameraController.ShakeCameraWin();
		}
#endif
	}
    private void decreaseLevel()
    {
        if(level > 0)
        {
            level -= 1;
            MainCameraController.SetLevelPosition(level);
            GamePlayController.SetLevelConfig(level);
        }
    }

    public void AdvanceLevel(int newLevel)
    {
        // Move the camera to the next point
		if(newLevel < Levels)
        {
            level = newLevel;
            MainCameraController.SetLevelPosition(level);
            GamePlayController.SetLevelConfig(level);
        }
    }

	//When we unlock a secret
	public static void CallSecretEvent(SecretType secretType)
	{
		if (SecretAction != null)
		{
			SecretAction(secretType);
		}
	}

	public static void GameOver()
	{

	}
	public static void StartGameOverAnimations (float aniSpeed)
	{
		//static filter
		LeanTween.value(0f, 6f, aniSpeed).setEaseOutQuad().setOnUpdate((float newValue) => instance.noiseFilter.intensityMultiplier = newValue).setIgnoreTimeScale(true);
		//fade
		LeanTween.alpha(instance.fadeOut, 0.6f, aniSpeed*2f).setEaseOutQuad().setDelay(aniSpeed/4f).setIgnoreTimeScale(true);
		LeanTween.alpha(instance.fadeInText, 1f, 2f).setEaseOutQuad().setDelay(aniSpeed/4f).setIgnoreTimeScale(true);
		//fade out
		LeanTween.alpha(instance.fadeInText, 0f, 5f).setEaseInSine().setDelay(2f).setIgnoreTimeScale(true).setOnComplete(() => {
			LeanTween.alpha(instance.clickToRestart, 1f, 1f).setEaseInSine().setIgnoreTimeScale(true);

		});
		//restart text

	}

	void RestartGame()
	{
		Debug.Log("Game Restart");
		shouldSpawnPlatforms = false;
		shouldPauseAllPlatforms = false;
		UnityEngine.SceneManagement.SceneManager.LoadScene("RestartScene");
	}
}