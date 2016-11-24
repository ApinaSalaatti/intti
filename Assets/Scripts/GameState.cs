using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameState : MonoBehaviour {
    private static GameState instance;
    public static GameState Instance { get { return instance; } }

    [SerializeField]
    private GameObject player;
    public GameObject Player { get { return player; } }
    

	// Use this for initialization
	void Awake () {
        instance = this;
	}
	
}
