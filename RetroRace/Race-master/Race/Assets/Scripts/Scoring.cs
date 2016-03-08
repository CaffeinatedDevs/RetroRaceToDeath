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
        }
    }
}
