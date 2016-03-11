using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour {
	public GameObject car;
	public static float carCol;
	public static float delayTimer = 0.7f;
	float timer;

	// Use this for initialization
	void Start () {
		timer = delayTimer;
		carCol = Random.Range (1, 4);
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.gameStarted) {
            if (!GameManager.Instance.gameOver) {
                if (!GameManager.Instance.paused) {
                    timer -= Time.deltaTime;

                    carCol = Random.Range(1, 4);
					if (Mathf.Round(carCol) == 1 && Mathf.Round(bonusSpawner.bonusCol) != 1) {
                        carCol = -9.5f;
                    }
					else if (Mathf.Round(carCol) == 2 && Mathf.Round(bonusSpawner.bonusCol) != 2) {
                        carCol = 0.3f;
                    }
					else if (Mathf.Round(carCol) == 3 && Mathf.Round(bonusSpawner.bonusCol) != 3) {
                        carCol = 10.1f;
                    }


                    if (timer <= 0) {
                        Vector3 carPos = new Vector3(carCol, transform.position.y, transform.position.z);
                        Instantiate(car, carPos, transform.rotation);
                        timer = delayTimer;
                    }
                }
            }
        }
	}
}
