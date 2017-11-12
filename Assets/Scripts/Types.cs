using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum INTERACTION_TYPE
{
	BOMBA,
    MIRROR,
    DOOR,
    AMPLIFIER,
    TORCH,
    LENSE,
    CREATURE,
    BAT_BOMB_TRIGGER,
    NONE
}

public enum ACTOR
{
    COGSWORTH,
    DOOR,
    NUM_ACTORS
}

public struct LightCollisionInfo
{
    public INTERACTION_TYPE InteractionType;
    public Vector3 CollisionPoint;
    public Vector3 ReflectionVector;
    public Vector3 CollisionForward;
    
    public LightCollisionInfo(INTERACTION_TYPE interactionType,
        Vector3 collisionPoint, Vector3 reflectionVector, Vector3 collisionForward)
    {
        InteractionType = interactionType;
        CollisionPoint = collisionPoint;
        ReflectionVector = reflectionVector;
        CollisionForward = collisionForward;
    }
}