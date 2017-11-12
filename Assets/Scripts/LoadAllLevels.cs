using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class LoadAllLevels : MonoBehaviour
{
    public string ScenesPath;
    public int LevelCount;
    private bool loaded = false;
    
    private void Start()
    {
        Load();
    }

    public void Load()
    {
        if(!loaded)
        {
            string scenePath;
            string sceneName;
            for(int i = 0; i < LevelCount; i++)
            {
                sceneName = "Level" + i.ToString();
                scenePath = Path.Combine(ScenesPath, sceneName);
                if(Application.isEditor && !Application.isPlaying)
                {
#if UNITY_EDITOR
                    scenePath += ".unity";
                    EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
#endif
                }
                else
                {
                    bool load = true;
                    for(int j = 0; j < SceneManager.sceneCount; j++)
                    {
                        if(SceneManager.GetSceneAt(j).name == sceneName)
                        {
                            load = false;
                            break;
                        }
                    }

                    if(load)
                    {
                        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                    }

                }


            }
        }
        loaded = true;
    }
}
