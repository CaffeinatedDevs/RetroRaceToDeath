using UnityEngine;
using System.Collections;

public class spawnedObjectMovement : MonoBehaviour {
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
		if (col.gameObject.tag == "coinObj" || col.gameObject.tag == "powerMonster" || col.gameObject.tag == "powerMissile" || col.gameObject.tag == "eCar") {
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
	}
}
