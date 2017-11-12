using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnerScript : MonoBehaviour {
	[SerializeField]
	private GameObject smartPlatform;
	[SerializeField]
	private GameObject[] itemsToSpawnOnTop;

	bool lastItemWasBomb = false;
	bool shouldSpawnOnlyMirrorsNow = false;
	float[] potentialangles;
	int countItems = 0;
	// Use this for initialization
	void Start () {
		potentialangles = new float[]{ 
			-45, 135
		};

		StartCoroutine(StartSpawning());
	}

	IEnumerator StartSpawning() 
	{
		GameObject spawnedPlatform, spawnedItem;
		int randomNumber;
		Vector3 positionToSpawn = Vector3.zero;
		Quaternion qAngles = Quaternion.identity;

		while (true)
		{
			yield return new WaitForSeconds(5f);
			if (!GameState.shouldSpawnPlatforms)
			{
				continue;
			}

			countItems++;
			spawnedPlatform = Instantiate(smartPlatform, transform.position, Quaternion.identity) as GameObject;;
			randomNumber = Random.Range(0, itemsToSpawnOnTop.Length);
			qAngles = Quaternion.Euler(0f, potentialangles[Random.Range(0, potentialangles.Length)], 0f);


			if (itemsToSpawnOnTop[randomNumber].tag == "Bomb" && lastItemWasBomb)
			{
				//No 2x bombs
				randomNumber = 0; //important, mirror thingy must be #1. WE ARE NUMBER ONE
				lastItemWasBomb = true;
			}
			else
			{
				lastItemWasBomb = false;
			}

			if (shouldSpawnOnlyMirrorsNow)
			{
				randomNumber = 0;
			}

			switch (itemsToSpawnOnTop[randomNumber].tag)
			{
				case "Bomb":
					positionToSpawn = new Vector3(transform.position.x + 1f, 2.4f, transform.position.z);
					break;
				case "Mirror":
						
					positionToSpawn = new Vector3(transform.position.x + 1.08f, 0f, transform.position.z);
					break;
				case "Skeleton":
					positionToSpawn = new Vector3(transform.position.x + 1.17f, 1f, transform.position.z - 1f);
					break;
			}
			spawnedItem = Instantiate(itemsToSpawnOnTop[randomNumber], positionToSpawn, qAngles) as GameObject;
			spawnedItem.transform.parent = spawnedPlatform.transform;

			if (countItems > 2)
			{
				shouldSpawnOnlyMirrorsNow = true;
			}
		}

	}
}
