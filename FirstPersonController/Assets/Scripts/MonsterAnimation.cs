using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour {

	public LayerMask playerLayer;
	public GameObject player;
	public GameObject explosion;

	float speed;
	Animator animator;
	bool walk;
	bool attack;
	PlayerController playerController;
	float attackSpeed;

	void Start () {
		animator = GetComponent<Animator>();
		speed = 1.8f;
		animator.SetFloat("Speed", 0.4f);
		walk = true;
		attack = false;
		transform.localEulerAngles = new Vector3(0, Random.Range(0f, 360f), 0);
		playerController = player.GetComponent<PlayerController>();
		attackSpeed = 0;
	}
	
	void Update () {

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f, playerLayer);
		if (hitColliders.Length > 0)
		{
			var lookpos = player.transform.position - transform.position;
			lookpos.y = 0;
			var rotation = Quaternion.LookRotation(lookpos);
			
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
		}

		if (walk) {
			transform.position += transform.forward * speed * Time.deltaTime;
		}

		 if (attack)
		 {
			 if (attackSpeed >= 1.14)
			 {
				 playerController.getHit();
				 attackSpeed = 0;
			 }
			attackSpeed += Time.deltaTime;
		 }
	}

	void OnCollisionStay(Collision collision)
    {
		if (!collision.gameObject.CompareTag("Ground") && !collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Bullet"))
		{
			transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y + Random.Range(0f, 360f), 0);
		}
    }

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			animator.SetBool("Hit", true);
			animator.SetFloat("Speed", 0);
			walk = false;
			attack = true;
		}
		else if (collision.gameObject.CompareTag("Bullet"))
		{
			Instantiate(explosion, transform.position, transform.rotation);
			playerController.monsterKilled += 1;
			Destroy(this);
			Destroy(gameObject);
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			animator.SetBool("Hit", false);
			animator.SetFloat("Speed", 0.4f);
			walk = true;
			attack = true;
		}
	}
}
