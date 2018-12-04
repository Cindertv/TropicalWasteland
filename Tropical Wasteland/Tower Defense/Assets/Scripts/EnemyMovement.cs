using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]

public class EnemyMovement : MonoBehaviour {

    /// <summary>
    /// Tells the enemy unit which waypoints to follow and at what speed and direction takes a point of life away when it reaches the end of the path
    /// </summary>
	private Transform target;
	private int wavepointIndex = 0;

	private Enemy enemy;

	void Start()
	{
		enemy = GetComponent<Enemy>();

		target = Waypoints.wayPoints[0];
	}

	void Update()
	{
		Vector3 direction = target.position - transform.position;
		transform.Translate(direction.normalized * enemy.movementSpeed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			FindNewWaypoint();
		}

		enemy.movementSpeed = enemy.startMovementSpeed;
	}

	void FindNewWaypoint()
	{
		if (wavepointIndex >= Waypoints.wayPoints.Length - 1)
		{
			PathEndWaypoint();
			return;
		}

		wavepointIndex++;
		target = Waypoints.wayPoints[wavepointIndex];
	}

	void PathEndWaypoint()
	{
		PlayerStats.Lives--;
		WaveSpawner.EnemiesAlive--;
		Destroy(gameObject);
	}

}
