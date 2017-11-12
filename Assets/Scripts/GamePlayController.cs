using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
	private const int LEVEL_COUNT = 99;

	//ref
	private LevelController[] levelControllers;
	private LevelController currentLevel;

	//singleton
	private static GamePlayController instance;
	void Awake () {
		instance = this;
		DontDestroyOnLoad (gameObject);
	}

	//level reference setup (happens on load)
	public static void InitLevel (LevelController levelRef, int levelNumber) {
		if (instance.levelControllers == null) {
			instance.levelControllers = new LevelController[LEVEL_COUNT];
		}
		if (levelNumber >= LEVEL_COUNT) {
			Debug.LogError("Make LEVEL_COUNT bigger, you exceeded the level count!");
		}
		if (instance.levelControllers[levelNumber] != null) {
			Debug.LogError("You have MORE THAN ONE of these LEVELS: Level "+levelNumber.ToString());
		}
		instance.levelControllers[levelNumber] = levelRef;
	}

	public static void SetLevelConfig (int level)
	{
		//let last level know to stop processing things
		if (instance.currentLevel != null) {
			instance.currentLevel.UnloadLevel();
		}
		//setup next level
		instance.currentLevel = instance.levelControllers[level];
		instance.currentLevel.SetupLevel();
	}

    public static void ResetCurrentLevel()
    {
        Time.timeScale = 1f;
        if(instance.currentLevel != null)
        {
            instance.currentLevel.ResetLevel();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reset");
            ResetCurrentLevel();
        }
    }
}
