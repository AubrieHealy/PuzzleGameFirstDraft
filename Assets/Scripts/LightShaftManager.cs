using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShaftManager : MonoBehaviour
{
    List<LightShaftOrigin> LightShaftOriginList = new List<LightShaftOrigin>();
    public LightShaftOrigin OriginalLightSource;
    public GameObject LightShaftOriginPrefab;

    Vector3 SpawnOffsetVec = new Vector3();
    LightCollisionInfo collision = new LightCollisionInfo();

    public bool isPropegating = false;

    void Start()
    {
        LightShaftOriginList.Add(OriginalLightSource);
    }

    void FixedUpdate()
    {
        if (isPropegating)
        {
            PropogateLightT();
        }
    }

    void PropogateLightT()
    {
        int currLight = 0;

        while (currLight < LightShaftOriginList.Count)
        {
            collision = LightShaftOriginList[currLight].ProjectLight();

            // we have hit a mirror, and there isn't a previously existing LightShaftOrigin object at the destination
            if (collision.InteractionType == INTERACTION_TYPE.MIRROR && currLight == (LightShaftOriginList.Count - 1))
            {
                GameObject obj = Instantiate(LightShaftOriginPrefab, collision.CollisionPoint, Quaternion.identity) as GameObject;

                //obj.transform.localRotation = Quaternion.Euler(collision.ReflectionVector);
                LightShaftOrigin lightShaftOrigin = obj.GetComponent<LightShaftOrigin>();
                lightShaftOrigin.IsOriginalLightSource = false;
                if (lightShaftOrigin != null)
                {
                    LightShaftOriginList.Add(lightShaftOrigin);
                    lightShaftOrigin.IncomingLightVector = collision.ReflectionVector;
                    lightShaftOrigin.name = "LightShaftOrigin #" + (currLight + 1);
                }
            }
            else if (collision.InteractionType == INTERACTION_TYPE.NONE)
            {
                for (int i = (LightShaftOriginList.Count - 1); i > (currLight); i--)
                {

                    LightShaftOrigin shaft = LightShaftOriginList[i];
                    LightShaftOriginList.RemoveAt(i);
                    Destroy(shaft.gameObject);
                }
            }
            else if( collision.InteractionType == INTERACTION_TYPE.MIRROR)
            {
                LightShaftOriginList[currLight + 1].gameObject.transform.position = collision.CollisionPoint;
                LightShaftOriginList[currLight+1].IncomingLightVector = collision.ReflectionVector;
                LightShaftOriginList[currLight + 1].UpdateBeamLocation(collision.ReflectionVector);
            }
            currLight++;
        }
    }

    public LightShaftOrigin[] GetLightPath()
    {
        return LightShaftOriginList.ToArray();
    }
}
