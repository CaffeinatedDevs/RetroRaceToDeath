﻿using UnityEngine;
using System.Collections;

public class coinMovement : MonoBehaviour {
	public static float speed = 50f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.Instance.gameOver) {
			if (!GameManager.Instance.paused) {
				transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
			}
		}
	}

	void OnCollisionEnter(Collision col){		
		if (col.gameObject.tag == "eCar") {
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
	}
}
