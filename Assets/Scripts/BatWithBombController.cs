using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatWithBombController : MonoBehaviour, Interactable {

    public Transform StartPosition;
    public Transform EndPosition;

    public float TimeToReachTarget;
    float CurrentTime = 0;
    public GameObject BatObject;
    BoxCollider boxCollider;

    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        BatObject.SetActive(false);
    }
	
    public void SpawnBat()
    {
        BatObject.SetActive(true);
        CurrentTime = 0;
        BatObject.transform.position = StartPosition.position;
        BatObject.transform.LookAt(EndPosition);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnBat();
        }
        if(BatObject.activeInHierarchy)
        {
            CurrentTime += Time.deltaTime / TimeToReachTarget;
            BatObject.transform.position = Vector3.Lerp(StartPosition.position, EndPosition.position,
            CurrentTime);
        }
    }

    public void Interact()
    {
        Debug.Log("Hit Interact");
        boxCollider.enabled = false;
        SpawnBat();
    }

    public INTERACTION_TYPE GetInteractionType()
    {
        return INTERACTION_TYPE.BAT_BOMB_TRIGGER;
    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
    }
}
