using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scoring : MonoBehaviour {

    public int score;
    public Text scoreCtr;
    public bool stopScore;

    private static Scoring instance;

    public static Scoring Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<Scoring>();
            }
            return instance;
        }
        set { instance = value; }
    }

    // Use this for initialization
    void Start () {
        stopScore = false;
        scoreCtr.text = "";
        score = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.gameStarted) {
            scoreCtr.text = score.ToString();

			if (score >= 20 && score <= 29) {
				spawnedObjectMovement.speed = 60.0f;
				enemySpawner.delayTimer = 0.5f;
				bonusSpawner.delayTimer = 2.0f;
			} else if (score >= 30 && score <= 39) {
				spawnedObjectMovement.speed = 70.0f;
				enemySpawner.delayTimer = 0.3f;
				bonusSpawner.delayTimer = 1.0f;
			} else if (score >= 40 && score <= 49) {
				spawnedObjectMovement.speed = 80.0f;
				enemySpawner.delayTimer = 0.1f;
				bonusSpawner.delayTimer = 0.5f;
			}  else {
				spawnedObjectMovement.speed = 50.0f;
				enemySpawner.delayTimer = 0.7f;
				bonusSpawner.delayTimer = 3.0f;
			}
        }
    }
}
