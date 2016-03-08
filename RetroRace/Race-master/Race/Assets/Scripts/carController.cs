﻿using UnityEngine;
using System.Collections;

public class carController : MonoBehaviour {
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
		position = transform.position;
		originalRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Horizontal") && Input.GetAxisRaw("Horizontal") < 0.3f) {
			position.x -= 0.6f;
			tempRotation = carRotationLeft;
		} else if (Input.GetButton ("Horizontal") && Input.GetAxisRaw("Horizontal") > 0.3f) {
			position.x += 0.6f;
			tempRotation = carRotationRight;
		} else {
			tempRotation = carRotationOriginal;
		}

		transform.rotation = Quaternion.Slerp(transform.rotation, tempRotation, Time.time * 0.25f);
		position.x = Mathf.Clamp (position.x, -9.5f,10.1f);
		transform.position = position;
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "eCar") {
			audioMangr.carSound.Stop ();
			audioMangr.raceBackground.Stop();
			audioMangr.carCrash.Play ();
			audioMangr.newHighScore.Play();
            GameManager.Instance.gameOver = true;
            Scoring.Instance.stopScore = true;
            GameManager.Instance.AddScoreToDb();
            Destroy(gameObject);
		}
	}
}