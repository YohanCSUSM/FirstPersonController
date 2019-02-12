using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public Transform head;
	public MenuManager menuManager;

	private Vector3 direction;
	private Rigidbody rbody;

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public float health;
	public Image healthBar;
	public Text healthNumber;
	public int maxMonsters;

	private float speed= 5.0f;
	private float rotationSpeed = 1.5f;
	private float minY = -60f;
	private float maxY = 60f;
	private float rotationY = 10f;
	private float rotationX = 0f;
	public int monsterKilled = 0;

	void Start () 
	{
		rbody = GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;
		healthNumber.text = health.ToString();
	}
	
	void Update () 
	{
		if (monsterKilled >= maxMonsters)
		{
			menuManager.End(true);
		}

		direction = Vector3.zero;
		direction.x = Input.GetAxis("Horizontal");
		direction.z = Input.GetAxis("Vertical");
		direction = direction.normalized;
		if(direction.x != 0)
			rbody.MovePosition(rbody.position + transform.right * direction.x * speed * Time.deltaTime);
		if(direction.z != 0)
			rbody.MovePosition(rbody.position + transform.forward * direction.z * speed * Time.deltaTime);
		
		rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
		transform.localEulerAngles = new Vector3(0, rotationX, 0);
		
		rotationY += Input.GetAxis("Mouse Y") * rotationSpeed;
		rotationY = Mathf.Clamp(rotationY, minY, maxY);
		head.localEulerAngles = new Vector3(-rotationY, 0, 0);

		if(Input.GetButtonDown("Fire1"))
		{
			Fire();
		}
	}

	void Fire() {
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 30;
		Destroy(bullet, 2.0f);
	}

	public void getHit()
	{
		health -= 10;
		if (healthNumber.text != "")
			healthNumber.text = health.ToString();
		healthBar.fillAmount -= 0.1f;
		if (health <= 0)
		{
			menuManager.End(false);
			healthNumber.text = "";
		}
	}
}
