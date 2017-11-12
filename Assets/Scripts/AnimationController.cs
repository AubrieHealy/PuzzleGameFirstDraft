using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Managers
{
    public class AnimationController : MonoBehaviour
    {

        public static AnimationController Singleton = null;

        public enum COGSWORTH_ANIMATIONS
        {
            COGSWORTH_MOVE_LEFT,
            MAX_POSES
        }

        Dictionary<ACTOR, Animator> animDict = new Dictionary<ACTOR, Animator>();

        // Use this for initialization
        void Start()
        {
            if (Singleton == null)
            {
                Singleton = this;
                Singleton.InitAnimationController();
            }
        }

        void InitAnimationController()
        {
            //animDict[ACTOR.COGSWORTH] = CuppaHappyAnimator;

        }

        public void SetTrigger(ACTOR toAnimate, int stringHash)
        {
            animDict[toAnimate].SetTrigger(stringHash);
        }
    }
}