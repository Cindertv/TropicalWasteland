using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {
    /// <summary>
    /// Spawns enemy units each wave with a timer before the first wave starts and a timer between each wave
    /// holds an array of the waypoints
    /// Has a speed variable of how fast the enemies should spawn in the specific wave
    /// Has predefind enenmy prefabs that you allocate in the inspector so show which enemie should spawn on which wave
    /// Sets the value of how many enemy there should spawn each wave
    /// Sets the value of how many waves there should be in a level 
    /// </summary>

    public static int amountOfEnemiesAlive = 0;

	public Wave[] waves;

	public Transform spawnPoint;

	public float timeBetweenWaves = 10f;
	private float waveCountDown = 5f;

	public GameManager gameManager;

	private int waveIndex = 0;


	void Update ()
	{
		if (amountOfEnemiesAlive > 0)
		{
			return;
		}

		if (waveIndex == waves.Length)
		{
			gameManager.WinLevel();
			this.enabled = false;
		}

		if (waveCountDown <= 0f)
		{
			StartCoroutine(SpawnWave());
			waveCountDown = timeBetweenWaves;
			return;
		}

		waveCountDown -= Time.deltaTime;

		waveCountDown = Mathf.Clamp(waveCountDown, 0f, Mathf.Infinity);
	}

	IEnumerator SpawnWave ()
	{
		PlayerStats.Rounds++;

		Wave wave = waves[waveIndex];

		amountOfEnemiesAlive = wave.amountOfEnemiesToSpawn;

		for (int i = 0; i < wave.amountOfEnemiesToSpawn; i++)
		{
			SpawnEnemy(wave.enemyPrefab);
			yield return new WaitForSeconds(1f);
		}

		waveIndex++;
        
	}

	void SpawnEnemy (GameObject enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}

}
