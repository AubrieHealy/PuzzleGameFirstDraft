using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Managers;

public class LightShaftOrigin : MonoBehaviour {
    
    public Vector3 IncomingLightVector = new Vector3();
    public bool IsOriginalLightSource = false;
    LightCollisionInfo CollisionInfo = new LightCollisionInfo();

    public GameObject GeroBeamPrefab;
    private GameObject GeroBeamObj;

    // Use this for initialization
    void Start () {
        CollisionInfo = new LightCollisionInfo();
        IncomingLightVector = transform.forward;

        GeroBeamObj = Instantiate(GeroBeamPrefab, gameObject.transform, false) as GameObject;
        if(IsOriginalLightSource)
        {
            UpdateBeamLocation(IncomingLightVector);
        }
        else
        {
            AudioManager.Singleton.PlaySFXSound(AudioManager.SFX_ID.MIRROR_HIT);
        }
    }

    public LightCollisionInfo ProjectLight()
    {
        CollisionInfo.InteractionType = INTERACTION_TYPE.NONE;

        RaycastHit hit;
        if(IsOriginalLightSource)
        {
            IncomingLightVector = transform.forward;
        }

        Ray ray = new Ray(gameObject.transform.position, IncomingLightVector);
        bool result = Physics.Raycast(ray, out hit, 100f);

        Debug.DrawRay(gameObject.transform.position, IncomingLightVector);
        if (result)
        {
            Interactable interactableObj = hit.transform.GetComponent<Interactable>();

            if (interactableObj != null)
            {
                interactableObj.Interact();
                if(interactableObj.GetInteractionType() == INTERACTION_TYPE.MIRROR)
                {
                    // new
                    Vector3 reflectionVec = Vector3.Reflect((hit.point - gameObject.transform.position).normalized, hit.normal);


                    //Debug.DrawLine(hit.point, reflectionVec);
                    Debug.DrawLine(hit.point, hit.point + reflectionVec * 10.0f, Color.blue);
                    //gameObject.transform.localRotation = Quaternion.Euler(reflectionVec);

                    CollisionInfo.InteractionType = INTERACTION_TYPE.MIRROR;
                    CollisionInfo.ReflectionVector = reflectionVec;
                    CollisionInfo.CollisionPoint = hit.point;
                    CollisionInfo.CollisionForward = hit.transform.TransformDirection(hit.transform.forward);
                }
            }
        }
        return CollisionInfo;
    }

    public void UpdateBeamLocation(Vector3 beamDirection)
    {
        GeroBeamObj.transform.rotation = Quaternion.LookRotation(beamDirection);
    }
}
