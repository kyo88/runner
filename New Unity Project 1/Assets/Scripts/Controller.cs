using UnityEngine;
using System.Collections;


public enum GameState{
	Begin,
	Start,
	Pause,
	End
};

public class Controller : MonoBehaviour {

	public int score = 0;
	private int score_start = 0;
	public int coin_score = 0;

	public int LEVEL = 0;
	public int LEVEL_MAX = 5;

	public GameState STATE;

	public GameObject character = null;
	
	// Use this for initialization
	void Start () {

		STATE = GameState.Begin;
		score_start = (int)character.transform.position.z;
		LEVEL = 1;
		//Add GameController
		GameConfig.instance.transform.parent = this.transform;
		GameController.instance.transform.parent = this.transform;
		GameController.instance.controller = this;
		EnemyController.instance.transform.parent = this.transform;
		EnemyController.instance.controller = this;
		CointController.instance.transform.parent = this.transform;
		CointController.instance.controller = this;
		MapController.instance.transform.parent = this.transform;
		MapController.instance.controller = this;

	}
	
	// Update is called once per frame
	void Update () {
		score = (int)character.transform.position.z - score_start;
		if (LEVEL < LEVEL_MAX) {
			if( score >= GameConfig.instance.LEVEL_CHANGE*LEVEL ){
				LEVEL ++;
			}
		}
	}

	//Increase Coin Score
	public void IncreaseCoin(int coin){
		coin_score += coin*LEVEL;
	}
}
