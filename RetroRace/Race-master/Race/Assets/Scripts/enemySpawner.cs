using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour {
	public GameObject car;
	public float carCol;
	private float delayTimer = 0.7f;
	float timer;

	// Use this for initialization
	void Start () {
		timer = delayTimer;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.gameStarted) {
            if (!GameManager.Instance.gameOver) {
                if (!GameManager.Instance.paused) {
                    timer -= Time.deltaTime;

                    carCol = Random.Range(0, 4);
                    if (Mathf.Round(carCol) == 1) {
                        carCol = -9.5f;
                    }
                    else if (Mathf.Round(carCol) == 2) {
                        carCol = 0.3f;
                    }
                    else if (Mathf.Round(carCol) == 3) {
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
