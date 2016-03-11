using UnityEngine;
using System.Collections;

public class extraDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "eCar" || col.gameObject.tag == "coinObj" || col.gameObject.tag == "powerMonster" || col.gameObject.tag == "powerMissile") {
			if (!Scoring.Instance.stopScore) {
				Scoring.Instance.score++;
			}
			Destroy (col.gameObject);
		}
	}
}
