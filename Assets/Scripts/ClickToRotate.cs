using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToRotate : MonoBehaviour
{
	[SerializeField]
	Transform transformToRotate;
	[SerializeField]
	float rotationAmount = 90;
	[SerializeField]
	float rotationSpeed = 1f;

    [SerializeField]
    private Mirror mirror;

	//refs
	Outline outlineScript;

	//animation tracker
	int? aniID;

	void Awake ()
	{
		outlineScript = gameObject.GetComponent<Outline>();
	}

    public void OnMouseOver()
    {
        if(aniID == null && GameState.CurrentLevel >= 0)
        {
            float dir = 0f;
            if(Input.GetMouseButtonDown(0))
            {
                dir = -1f;
            }
            else if(Input.GetMouseButtonDown(1))
            {
                dir = 1f;
            }

            if(Mathf.Abs(dir) > float.Epsilon)
            {
                if(outlineScript != null) outlineScript.ForceGlow();

                if(dir > 0)
                {
                    mirror.TurnRight();
                }
                else
                {
                    mirror.TurnLeft();
                }
                //aniID = LeanTween.rotateY(transformToRotate.gameObject, transformToRotate.gameObject.transform.localEulerAngles.y + rotationAmount * dir, rotationSpeed).setOnComplete(
                //    () =>
                //    {
                //        if(outlineScript != null) outlineScript.ResetForcedGlow();
                //        aniID = null;
                //    }
                //).uniqueId;
            }
        }
    }
}
