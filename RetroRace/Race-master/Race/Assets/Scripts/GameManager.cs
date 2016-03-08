using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
public class GameManager : MonoBehaviour {
    public bool paused;

    public bool gameStarted;

    public bool gameOver;
    public Button startB;
    public GameObject pauseButton;
    public GameObject pauseMessage;
    public GameObject title;
    public GameObject startButton;
    public GameObject Timer1;
    public GameObject Timer2;
    public GameObject Timer3;
    public GameObject Blocker;
    public GameObject score;
    public GameObject fscoreBG;
    public GameObject go;
    public Text fscore;
    public GameObject lboard;
    public Text lboard1;
    public Text lboard2;
    public Text lboard3;
    public Text lboard4;
    public Text lboard5;
    public bool lboardFlag;

    public static int[] leaderboard = new int[10];

    private static GameManager instance;

	public AudioManager audioMangr;

    public static GameManager Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<GameManager>();
            }
            return instance;
        }
        set {instance = value;}
    }

    IEnumerator UnPauseTimer() {
        paused = !paused;
        pauseMessage.SetActive(false);
        startButton.SetActive(false);
        Timer3.SetActive(true);
        yield return StartCoroutine(WaitForRealSeconds(1.0f));
        Timer3.SetActive(false);
        Timer2.SetActive(true);
        yield return StartCoroutine(WaitForRealSeconds(1.0f));
        Timer2.SetActive(false);
        Timer1.SetActive(true);
        yield return StartCoroutine(WaitForRealSeconds(1.0f));
        Timer1.SetActive(false);
        yield return StartCoroutine(WaitForRealSeconds(1.0f));
        paused = !paused;

		audioMangr.menuBackground.Stop();
		audioMangr.raceBackground.Play();
		audioMangr.carSound.Play();
    }

    IEnumerator WaitForRealSeconds(float waitTime) {
        float endTime = Time.realtimeSinceStartup + waitTime;

        while (Time.realtimeSinceStartup < endTime) {
            yield return null;
        }
    }
	
    public void AddScoreToDb(){
        leaderboard[9] = Scoring.Instance.score;
        Array.Sort(leaderboard);
        Array.Reverse(leaderboard);
        var result = string.Join(",", Array.ConvertAll(leaderboard, x => x.ToString()));
        File.WriteAllText(@"C:\Users\Anzano\Documents\GitHub\Race\Race\Assets\Marci\scoresheet.csv", result);
    }
	
    static void readCsv() {
        String[] values = File.ReadAllText(@"C:\Users\Anzano\Documents\GitHub\Race\Race\Assets\Marci\scoresheet.csv").Split(',');
        for (int i = 0; i < 5; i++) {
            leaderboard[i] = Convert.ToInt32(values[i]);
            i++;
        }
        Array.Sort(leaderboard);
        Array.Reverse(leaderboard);
    }

    // Update is called once per frame


    // Use this for initialization
    void Start() {
        readCsv();
        score.SetActive(true);
        title.SetActive(true);
        Blocker.SetActive(true);
        paused = false;
        gameStarted = false;
        gameOver = false;
        pauseMessage.SetActive(false);
        startButton.SetActive(true);
        Timer1.SetActive(false);
        Timer2.SetActive(false);
        Timer3.SetActive(false);
        fscoreBG.SetActive(false);
        go.SetActive(false);
        fscore.text = "";
        lboard.SetActive(false);
        lboard1.text = "";
        lboard2.text = "";
        lboard3.text = "";
        lboard4.text = "";
        lboard5.text = "";
        lboardFlag = false;
    }
    void Update () {
        if (gameOver) {
            go.SetActive(true);
            fscoreBG.SetActive(true);
            startButton.SetActive(true);
            Scoring.Instance.scoreCtr.text = "";
            fscore.text = Scoring.Instance.score.ToString();
            if (Input.GetKeyDown(KeyCode.Return)|| Input.GetKeyDown(KeyCode.Escape)) {
                Application.LoadLevel(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Return)&& paused)) {
            paused = !paused;
            if (paused) {
                pauseMessage.SetActive(true);
                startButton.SetActive(true);
                gameStarted = false;
				audioMangr.raceBackground.Stop();
				audioMangr.carSound.Stop();
				audioMangr.menuBackground.Play();
            }
            else {
                StartCoroutine(UnPauseTimer());
                gameStarted = true;
            }
        }
        if (!gameStarted) {
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
                gameStarted = true;
                startButton.SetActive(false);
				title.SetActive(false);
				audioMangr.menuBackground.Stop();
				audioMangr.raceBackground.Play();
				audioMangr.carSound.Play();
            }
            if (Input.GetKeyDown(KeyCode.Tab)) {
                lboard.SetActive(true);
                lboard1.text = leaderboard[0].ToString();
                lboard2.text = leaderboard[1].ToString();
                lboard3.text = leaderboard[2].ToString();
                lboard4.text = leaderboard[3].ToString();
                lboard5.text = leaderboard[5].ToString();
                lboardFlag = true;
            }
            if (lboardFlag) {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) {
                    Application.LoadLevel(0);
                    }
                lboard1.text = leaderboard[0].ToString();
                lboard2.text = leaderboard[1].ToString();
                lboard3.text = leaderboard[2].ToString();
                lboard4.text = leaderboard[3].ToString();
                lboard5.text = leaderboard[5].ToString();
                title.SetActive(false);
            }
            else {
                lboard1.text = "";
                lboard2.text = "";
                lboard3.text = "";
                lboard4.text = "";
                lboard5.text = "";
            }
            //Scoring.Instance.scoreCtr.text = "";
            //Scoring.Instance.score = 0;
        }
        else {
            Scoring.Instance.scoreCtr.text = Scoring.Instance.score.ToString();
        }
    }
}
