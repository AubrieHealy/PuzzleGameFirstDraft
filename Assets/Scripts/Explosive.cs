using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour, Interactable
{
    [SerializeField]
	float explosionForce = 10f;
	[SerializeField]
	float explosionRadius = 10f;
	[SerializeField]
    float explosionUpwardMod = 5f;

	Rigidbody rBody;

    public void Interact()
    {
    	if (rBody == null) {
			rBody = gameObject.AddComponent<Rigidbody>();
    	}
    	else {
			Debug.Log("EXPLODE!");
			rBody.AddExplosionForce(explosionForce, transform.position + Vector3.forward*0.5f, explosionRadius, explosionUpwardMod);
    	}
    }

    public INTERACTION_TYPE GetInteractionType()
    {
        return INTERACTION_TYPE.CREATURE;
    }
}
