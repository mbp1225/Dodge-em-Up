using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
	[System.Serializable]
	struct HardPoint
	{
		public Vector3 pos;
		public GameObject ammo;
	}


	[SerializeField] Transform player;
	[SerializeField] GameObject hit;
	[SerializeField] HardPoint[] hardpoints;
	[SerializeField] private float fireRate;
	[SerializeField] float speed;
	[SerializeField] float followCutoff;

	[SerializeField] bool canShoot;
	[SerializeField] bool canFollow;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		if (canShoot) StartCoroutine(Fire());
	}
	
	void Update ()
	{
		if (canFollow) Follow();
	}

	IEnumerator Fire()
	{
		foreach (HardPoint hardpoint in hardpoints)
		{
			Instantiate(hardpoint.ammo, transform.position + hardpoint.pos, Quaternion.identity);
		}

		//Instantiate(bullet, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(fireRate);
		StartCoroutine(Fire());
	}

	void Follow()
	{
		if (transform.position.y > -followCutoff && player)
		{
			//transform.LookAt(player, Vector3.forward);
			//transform.Rotate(new Vector3(270f, 0f, 0f));

			transform.DORotate(new Vector3(0,0,(Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg) + 90), 1f);
		}

		transform.position += -transform.up * speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		//print("Collided");
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerController>().TakeDamage();
			Instantiate(hit, transform.position, Quaternion.identity);
		}
		//if (other.gameObject.tag == "Limits") Destroy(gameObject);
		Destroy(gameObject);
	}
}
