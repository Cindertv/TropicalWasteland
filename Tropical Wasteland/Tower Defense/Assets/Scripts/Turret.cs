using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	private Transform enemyTarget;
	private Enemy targetEnemy;

	public float fireRange = 15f;

	[Header("Use Normal Attack")]
	public GameObject bulletPrefab;
	public float rateOfFire = 1f;
	private float countDownFire = 0f;

	[Header("Use FrostRay")]
	public bool useFrostRay = false;

	public int damageOverTime = 30;
	public float slowAmount = .5f;

	public LineRenderer lineRenderer;
	public ParticleSystem impactEffect;
	public Light impactLight;

	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";

	public Transform partToRotate;
	public float turnSpeed = 10f;

	public Transform firePoint;

	
	void Start () {
		InvokeRepeating("FindClosestTarget", 0f, 0.5f);
	}
	
	void FindClosestTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
			if (targetDistance < shortestDistance)
			{
				shortestDistance = targetDistance;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= fireRange)
		{
			enemyTarget = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<Enemy>();
		} else
		{
			enemyTarget = null;
		}

	}

	
	void Update () {
		if (enemyTarget == null)
		{
			if (useFrostRay)
			{
				if (lineRenderer.enabled)
				{
					lineRenderer.enabled = false;
					impactEffect.Stop();
					impactLight.enabled = false;
				}
			}

			return;
		}

		LockOnTarget();

		if (useFrostRay)
		{
			Laser();
		} else
		{
			if (countDownFire <= 0f)
			{
				Shoot();
				countDownFire = 1f / rateOfFire;
			}

			countDownFire -= Time.deltaTime;
		}

	}

	void LockOnTarget ()
	{
		Vector3 direction = enemyTarget.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	void Laser ()
	{
		targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
		targetEnemy.Slow(slowAmount);

		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
			impactEffect.Play();
			impactLight.enabled = true;
		}

		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, enemyTarget.position);

		Vector3 dir = firePoint.position - enemyTarget.position;

		impactEffect.transform.position = enemyTarget.position + dir.normalized;

		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}

	void Shoot ()
	{
		GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null)
			bullet.Seek(enemyTarget);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, fireRange);
	}
}
