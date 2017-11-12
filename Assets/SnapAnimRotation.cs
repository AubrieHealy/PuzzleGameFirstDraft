using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapAnimRotation : StateMachineBehaviour {
    public float Delta = 0f;

    //private Vector3 lastOffset = Vector3.zero;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    Transform root = animator.transform.FindChildByRecursive("rootJoint");
    //    //lastOffset = Vector3.Max(root.localPosition, lastOffset);
    //    lastOffset = root.localPosition;
    //    lastOffset.y = 0f;
    //    //root.parent.localPosition = -lastOffset;
    //    Debug.Log(lastOffset);
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Transform root = animator.transform.FindChildByRecursive("rootJoint");
        //Vector3 lateralOffset = lastOffset;
        //lateralOffset.y = 0f;
        //root.parent.localPosition = lateralOffset;

        //LeanTween.moveLocal(root.parent.gameObject, Vector3.zero, .1f);

        //LeanTween.rotateY(animator.gameObject, animator.transform.rotation.eulerAngles.y + Delta, .1f);
        Transform grandpa = animator.transform.parent.parent;
        grandpa.rotation *= Quaternion.AngleAxis(Delta, Vector3.up);

        animator.transform.GetComponentInChildren<Outline>().ResetForcedGlow();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
