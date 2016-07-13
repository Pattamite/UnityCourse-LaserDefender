using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{

	public float speed = 5.0f;
	public GameObject projectile;
	public float projectileSpeed = 5.0f;
	public float fireRate = 0.2f;
	public float health = 100f;
	public AudioClip playerFire;
	
	private float xmin;
	private float xmax;	
	private float playerHalfSize = 0.5f;
	// Use this for initialization
	void Start () 
	{
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xmin = leftmost.x + playerHalfSize;;
		xmax = rightmost.x - playerHalfSize;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			//transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		
		if(Input.GetKeyDown(KeyCode.Space)) InvokeRepeating("Fire", 0.000001f, fireRate);
		if(Input.GetKeyUp(KeyCode.Space)) CancelInvoke("Fire");
		
	}
	
	void Fire()
	{
		GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity  = new Vector3 (0, projectileSpeed);
		AudioSource.PlayClipAtPoint(playerFire , transform.position);
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		Projectile missile =  collider.gameObject.GetComponent<Projectile>();
		if(missile)
		{
			//Debug.Log ("Hit by a projectile");
			health -= missile.GetDamage();
			missile.Hit();
			if(health <=0)
			{
				Die();
			}
		}
	}
	
	void Die()
	{
		LevelManager levelmanager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		levelmanager.LoadLevel("Game Over");
	}
}
