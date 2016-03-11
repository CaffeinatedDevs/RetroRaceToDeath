using UnityEngine;
using System.Collections;

public class carController : MonoBehaviour {
	public int playerLife, coinCollected, missileCollected;
	public float carSpeed;
	public string carDirection;
	public Quaternion originalRotation, tempRotation;
	public Quaternion carRotationLeft = Quaternion.Euler(0.0f, 80.0f, 0.0f);
	public Quaternion carRotationRight = Quaternion.Euler(0.0f, 100.0f, 0.0f);
	public Quaternion carRotationOriginal = Quaternion.Euler(0.0f, 90.0f, 0.0f);

	Vector3 position;

	public AudioManager audioMangr;

	// Use this for initialization
	void Start () {
		playerLife = 5;
		position = transform.position;
		originalRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		CarArrowControls ();
		// AccelerometerControls ();
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "eCar") {
			audioMangr.carCrash.Play ();
			playerLife -= 1;
			Debug.Log(playerLife);

			if (playerLife >= 1) {
				Destroy(col.gameObject);
			} else {
				Debug.Log(coinCollected);
				Destroy(gameObject);
				audioMangr.carSound.Stop ();
				audioMangr.raceBackground.Stop();
				audioMangr.newHighScore.Play();
				GameManager.Instance.gameOver = true;
				Scoring.Instance.stopScore = true;
				GameManager.Instance.AddScoreToDb();
			}
		}

		if (col.gameObject.tag == "coinObj") {
			Destroy(col.gameObject);
			coinCollected += 1;

//			if (Scoring.Instance.score >= 20 && Scoring.Instance.score < 40) {
//				enemyMovement.speed = 70.0f;
//				coinMovement.speed = 70.0f;
//				enemySpawner.delayTimer = 0.5f;
//				coinSpawner.delayTimer = 2.0f;
//			} else if (Scoring.Instance.score >= 40 && Scoring.Instance.score < 60) {
//				enemyMovement.speed = 100.0f;
//				coinMovement.speed = 100.0f;
//				enemySpawner.delayTimer = 0.3f;
//				coinSpawner.delayTimer = 1.5f;
//			}
		}

		if (col.gameObject.tag == "powerMonster") {
			Destroy(col.gameObject);
			// monster scene
		}

		if (col.gameObject.tag == "powerMissile") {
			Destroy(col.gameObject);
			missileCollected++;
		}
	}

	void CarArrowControls(){
		if (GameManager.Instance.gameStarted == true && GameManager.Instance.gameOver == false) {
			if (Input.GetButton ("Horizontal") && Input.GetAxisRaw("Horizontal") < 0.3f) {
				position.x -= 0.75f;
				tempRotation = carRotationLeft;
			} else if (Input.GetButton ("Horizontal") && Input.GetAxisRaw("Horizontal") > 0.3f) {
				position.x += 0.75f;
				tempRotation = carRotationRight;
			} else {
				tempRotation = carRotationOriginal;
			}
			
			transform.rotation = Quaternion.Slerp(transform.rotation, tempRotation, Time.time * 0.25f);
			position.x = Mathf.Clamp (position.x, -10.0f, 10.6f);
			transform.position = position;
		}
	}

	void AccelerometerControls(){
		float x = Input.acceleration.x;

		if (x < 0.1f) {
			position.x -= 0.6f;
			tempRotation = carRotationLeft;
		} else if (x > 0.1f) {
			position.x += 0.6f;
			tempRotation = carRotationRight;
		} else {
			tempRotation = carRotationOriginal;
		}

		transform.rotation = Quaternion.Slerp(transform.rotation, tempRotation, Time.time * 0.25f);
		position.x = Mathf.Clamp (position.x, -9.5f, 10.1f);
		transform.position = position;
		Debug.Log (position);
	}
}
