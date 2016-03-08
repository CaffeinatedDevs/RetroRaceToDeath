using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

    public void click() {
        GameManager.Instance.gameStarted = true;
        Debug.Log("clicked");
    }
}
