using UnityEngine;
using System.Collections;

public class enemyMovement : MonoBehaviour {
	public float speed = 8f;

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
}
