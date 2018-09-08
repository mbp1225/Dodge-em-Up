using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxObject : MonoBehaviour
{
	[SerializeField] private int layer;
	[SerializeField] private int destroyTreshold;

	void Start()
	{
		transform.position += Vector3.forward * layer;
	}
	
	void Update ()
	{
		transform.position += Vector3.down * (10f/layer) * Time.deltaTime;
		
		if (transform.position.y < -destroyTreshold) Destroy(gameObject); 
	}
}
