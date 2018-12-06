using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float startMovementSpeed = 10f;

	[HideInInspector]
	public float movementSpeed;

	public float startHealth = 100;
	private float health;

	public int killValue = 50;

	public GameObject deathEffect;

	public Image healthBar;

	private bool isDead = false;

	void Start ()
	{
		movementSpeed = startMovementSpeed;
		health = startHealth;
	}

	public void TakeDamage (float amount)
	{
		health -= amount;

		healthBar.fillAmount = health / startHealth;

		if (health <= 0 && !isDead)
		{
			EnemyDie();
		}
	}

	public void Slow (float pct)
	{
		movementSpeed = startMovementSpeed * (1f - pct);
	}

	void EnemyDie ()
	{
		isDead = true;

		PlayerStats.Money += killValue;

		GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);

		WaveSpawner.amountOfEnemiesAlive--;

		Destroy(gameObject);
	}

}
