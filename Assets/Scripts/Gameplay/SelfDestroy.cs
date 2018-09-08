using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
	[SerializeField] private float time;

	void Start ()
	{
		StartCoroutine(Die());
	}

	IEnumerator Die()
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
