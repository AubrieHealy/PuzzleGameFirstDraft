using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Managers;

public class Mirror : MonoBehaviour, Interactable
{
    public bool HasBeenHitThisRound = false;

    [SerializeField]
    EyeballAnimations eyeAni;

    public Animator CogsworthAnimator = null;
    public static int CogsworthTurnLeft = Animator.StringToHash("TurnLeft");
    public static int CogsworthTurnRight = Animator.StringToHash("TurnRight");

    public void Interact()
    {
    	if (!HasBeenHitThisRound) {
    		HasBeenHitThisRound = true;
			if (eyeAni != null)
			{
				eyeAni.AwakenReaction();
			}
    	}
    }

    public INTERACTION_TYPE GetInteractionType()
    {
        return INTERACTION_TYPE.MIRROR;
    }

    // --
    // Passing clicks through to outline and clickable scripts
    //--
	[SerializeField]
	Outline mirrorOutline;
	[SerializeField]
	ClickToRotate clickScript;

    void OnMouseOver () {
    	mirrorOutline.OnMouseOver();
        clickScript.OnMouseOver();
    }
	void OnMouseExit () {
		mirrorOutline.OnMouseExit();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            TurnLeft();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            TurnRight();
        }
    }

    public void TurnLeft()
    {
		eyeAni.KillEyes();
        if(CogsworthAnimator != null)
        {
            AudioManager.Singleton.PlaySFXShortDurSound(AudioManager.SFX_SHORT_DUR_ID.COGSWORTH_MOVING);
            CogsworthAnimator.SetTrigger(CogsworthTurnLeft);
        }
    }

    public void TurnRight()
    {
	    eyeAni.KillEyes();
	    if(CogsworthAnimator != null)
        {
            AudioManager.Singleton.PlaySFXShortDurSound(AudioManager.SFX_SHORT_DUR_ID.COGSWORTH_MOVING);
            CogsworthAnimator.SetTrigger(CogsworthTurnRight);
        }
    }
}
