using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGen : MonoBehaviour {

    public GameObject trackPrefab;

    public GameObject currentTrack;

    private static MapGen instance;

    public Stack<GameObject> tracks = new Stack<GameObject>();

    public static MapGen Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<MapGen>();
            }
            return instance;
        }
    }


    // Use this for initialization
    void Start () {
        CreateMap(5);
        for(int i=0; i < 5; i++) {
            SpawnMap();
        }        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateMap(int num) { //Prepare Maps
        for(int i = 0; i < num; i++) {
            tracks.Push(Instantiate(trackPrefab));
            tracks.Peek().SetActive(false);
        }
    }

    public void SpawnMap() {

        if (tracks.Count == 0) {
            CreateMap(5);
        }
        GameObject tmp = tracks.Pop();
//        Debug.Log(tracks.Count);
        tmp.SetActive(true);
        tmp.transform.position = currentTrack.transform.GetChild(0).transform.GetChild(0).position;
        currentTrack = tmp;
        //currentTrack = (GameObject)Instantiate(trackPrefab, currentTrack.transform.GetChild(0).transform.GetChild(0).position, Quaternion.identity);
    }
}
