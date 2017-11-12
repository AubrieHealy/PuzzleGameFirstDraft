using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LightShaftManager))]
[RequireComponent(typeof(LineRenderer))]
public class LightPathRenderer : MonoBehaviour {
    private LightShaftManager manager;
    private LineRenderer line;

    private LightShaftOrigin[] lightPath;

    private void Awake()
    {
        manager = GetComponent<LightShaftManager>();
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lightPath = manager.GetLightPath();

        if(lightPath != null)
        {
            line.positionCount = lightPath.Length;
            for(int i = 0; i < lightPath.Length; i++)
            {
                line.SetPosition(i, lightPath[i].transform.position);
            }
        }
        else
        {
            line.positionCount = 0;
        }
    }
}
