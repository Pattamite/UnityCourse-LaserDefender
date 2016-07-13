using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour 
{
	public GameObject projectile;
	public float health = 200f;
	public float projectileSpeed = 5f;
	public float shotPerSeconds = 0.5f;
	public int scoreValue_Hit = 25;
	public int scoreValue_Kill = 50;
	public AudioClip enemyFire;
	public AudioClip enemyDown;
	
	private ScoreKeeper scoreKeeper;
	
	void Start()
	{
		scoreKeeper = GameObject.Find ("Score Value").GetComponent<ScoreKeeper>();
	}
	
	void Update()
	{
		float probability = Time.deltaTime * shotPerSeconds;
		if(Random.value < probability)
		{
			Fire();
		}
	}
	
	void Fire()
	{
		GameObject missile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		missile.rigidbody2D.velocity = new Vector2(0,projectileSpeed*(-1));
		AudioSource.PlayClipAtPoint(enemyFire, transform.position);
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		Projectile missile =  collider.gameObject.GetComponent<Projectile>();
		if(missile)
		{
			//Debug.Log ("Hit by a projectile");
			health -= missile.GetDamage();
			missile.Hit();
			scoreKeeper.Score(scoreValue_Hit);
			if(health <=0) Die();
		}
	}
	
	void Die()
	{
		scoreKeeper.Score(scoreValue_Kill);
		AudioSource.PlayClipAtPoint(enemyDown, transform.position);
		Destroy(gameObject);
	}
}
