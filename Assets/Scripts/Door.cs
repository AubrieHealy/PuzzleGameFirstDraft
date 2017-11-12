using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Managers;

public class Door : MonoBehaviour, Interactable
{
	[SerializeField]
	int LevelToProgressTo;
	[SerializeField]
	float DelayDuration = 2f;

	bool HitInitiated = false;
	bool CheckForDuration = false;
	bool DoorActive = false;

    private Animator anim;

	void Awake()
	{
		//attach to parent LevelController
		Transform t = transform;
		while (t.parent != null)
		{
			LevelController lManager = t.parent.GetComponent<LevelController>();
			if (lManager != null) {
				lManager.doors.Add(this);
				break;
			}
			t = t.parent.transform;
		}

        anim = GetComponent<Animator>();
	}

    public void Interact()
    {
    	if (!DoorActive) {
    		return;
    	}

		if (!HitInitiated) {
			HitInitiated = true;
			Invoke("CheckForAdvance", DelayDuration);
        }
		else if (CheckForDuration) {
			DoorActive = false;
			AudioManager.Singleton.PlaySFXSound(AudioManager.SFX_ID.GATE_RAISE);
            if(anim != null)
            {
                anim.SetBool("Open", true);
                AudioManager.Singleton.PlaySFXShortDurSound(AudioManager.SFX_SHORT_DUR_ID.GATE_RAISE);
            }

			Debug.Log("Progressing Level to " + LevelToProgressTo);
			if (LevelToProgressTo == 4)
			{
				GameState.shouldSpawnPlatforms = false;
				GameState.shouldPauseAllPlatforms = true;
			}
            StartCoroutine(delayTransition());
		}
    }

    IEnumerator delayTransition()
    {
        yield return new WaitForSeconds(1.2f);
		if (LevelToProgressTo == 5)
		{
			//Game over you WIN
			AudioManager.Singleton.PlaySFXLongtDurSound(AudioManager.SFK_LONGISH_DUR_ID.CLAPPING_CROWD);
			Debug.Log("YOU WIN");
			yield break;
		}

        GameState.instance.AdvanceLevel(LevelToProgressTo);
    }

    void CheckForAdvance () {
    	CheckForDuration = true;
    }

    public void ResetDoorState ()
    {
    	CheckForDuration = false;
    	HitInitiated = false;
    	DoorActive = true;
    }

    public INTERACTION_TYPE GetInteractionType()
    {
        return INTERACTION_TYPE.DOOR;
    }
}
