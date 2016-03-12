using UnityEngine;
using System.Collections;

public class carController : MonoBehaviour {
	public static int playerLife, coinCollected, missileCollected;
	public float carSpeed, carHandling;
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
		carHandling = 0.75f;
	}
	
	// Update is called once per frame
	void Update () {
		CarArrowControls ();
		Debug.Log(carHandling);
		// AccelerometerControls ();
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "eCar") {
			audioMangr.carCrash.Play ();
			playerLife -= 1;
			carHandling -= 0.05f;

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
				position.x -= carHandling;
				tempRotation = carRotationLeft;
			} else if (Input.GetButton ("Horizontal") && Input.GetAxisRaw("Horizontal") > 0.3f) {
				position.x += carHandling;
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
