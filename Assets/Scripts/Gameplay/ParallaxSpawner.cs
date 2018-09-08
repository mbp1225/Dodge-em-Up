using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSpawner : MonoBehaviour
{
	[SerializeField] private GameObject[] objects;
	[SerializeField] private float spawnRate;

	void Start()
	{
		StartCoroutine(Spawner());
	}

	IEnumerator Spawner()
	{
		while (true)
		{
			Instantiate(objects[Random.Range(0, objects.Length)], transform.position + (transform.right * Random.Range(-4f, 4f)), Quaternion.identity);
			yield return new WaitForSeconds(spawnRate);
		}
	}
	
}
