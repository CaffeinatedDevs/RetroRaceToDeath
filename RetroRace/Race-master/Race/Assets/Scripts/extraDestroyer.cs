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
		if (col.gameObject.tag == "eCar") {
            if (!Scoring.Instance.stopScore) {
                Scoring.Instance.score++;
            }
			Destroy(col.gameObject);
            
		}
	}
}
