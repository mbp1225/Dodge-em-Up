using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private GameObject hit;

	[SerializeField] Vector3 direction;
	
	void Update ()
	{
		transform.position += direction * speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		print("Collided");
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerController>().TakeDamage();
			Instantiate(hit, transform.position, Quaternion.identity);
		}
		//if (other.gameObject.tag == "Limits") Destroy(gameObject);
		Destroy(gameObject);
	}
}
