using UnityEngine;
using System.Collections;

public class BackgroudRunner : MonoBehaviour {
    public float speed = 100.0f;
    //Gawing private sa finalu
    //private float speed = 100.0f;
    
        
    // Use this for initialization
    void Start() {
        GameManager.Instance.gameStarted = false;
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.Instance.gameStarted) {
            if (!GameManager.Instance.gameOver) {
                if (!GameManager.Instance.paused) {
                    if (transform.position.z <= -60.0f) {
                        MapGen.Instance.tracks.Push(gameObject);
                        MapGen.Instance.SpawnMap();
                    }
                    transform.Translate(0, 0, -1 * Time.deltaTime * speed);
                }
            }
        }
    }
}
