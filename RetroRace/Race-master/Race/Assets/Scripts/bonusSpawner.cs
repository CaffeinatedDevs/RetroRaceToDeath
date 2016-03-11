using UnityEngine;
using System.Collections;

public class bonusSpawner : MonoBehaviour {
	public GameObject coin, powerUpMonster, powerUpMissile;
	public static float bonusCol, bonusToSpawn, spFlag;
	public static float delayTimer = 3f;
	float timer;

	// Use this for initialization
	void Start () {
		timer = delayTimer;
		bonusCol = Random.Range (1, 4);
		bonusToSpawn = -1;
		spFlag = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.Instance.gameStarted) {
			if (!GameManager.Instance.gameOver) {
				if (!GameManager.Instance.paused) {
					timer -= Time.deltaTime;
					
					bonusCol = Random.Range(1, 4);

					if (Mathf.Round(bonusCol) == 1 && Mathf.Round(enemySpawner.carCol) != 1) {
						bonusCol = -9.5f;
					}
					else if (Mathf.Round(bonusCol) == 2 && Mathf.Round(enemySpawner.carCol) != 2) {
						bonusCol = 0.3f;
					}
					else if (Mathf.Round(bonusCol) == 3 && Mathf.Round(enemySpawner.carCol) != 3) {
						bonusCol = 10.1f;
					}

					if (timer <= 0) {
						Vector3 bonusPos = new Vector3(bonusCol, transform.position.y, transform.position.z);

						if (spFlag == 10) { 
							bonusToSpawn = Random.Range(1, 11);
						}

						if (Mathf.Round(bonusToSpawn) % 2 == 0 && spFlag == 10) {
							Instantiate(powerUpMissile, bonusPos, transform.rotation);
							bonusToSpawn = -1;
							spFlag = 1;
						} else if (Mathf.Round(bonusToSpawn) % 2 == 1 && spFlag == 10) {
							Instantiate(powerUpMonster, bonusPos, transform.rotation);
							bonusToSpawn = -1;
							spFlag = 1;
						} else if (Mathf.Round(bonusToSpawn) == -1 && spFlag != 10) {
							Instantiate(coin, bonusPos, transform.rotation);
							spFlag++;
						}

						timer = delayTimer;
					}
				}
			}
		}
	}
}
