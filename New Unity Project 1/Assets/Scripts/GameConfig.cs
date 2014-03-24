using UnityEngine;
using System.Collections;

public class GameConfig : MonoBehaviour {

	
	private static GameConfig _instance = null;

	public string CHARACTER_NAME = "Character";




	// Static Paths
	public string GAME_CONTROLLER_PATH = "Prefabs/Static/GameController";
	public string ENEMY_CONTROLLER_PATH = "Prefabs/Static/EnemyController";
	public string COIN_CONTROLLER_PATH = "Prefabs/Static/CoinController";
	public string MAP_CONTROLLER_PATH = "Prefabs/Static/MapController";


	// Game Obj Names
	public string COIN_NAME = "Coin";
	public string ZOMBIE1 = "Zomebie1";
	public string ZOMBIE2 = "Zomebie2";
	public string ZOMBIE3 = "Zomebie3";
	public string WAY_STONE = "Stone";
	public string WAY_MOSS = "Moss";
	public string WAY_GROUND = "Ground";

	//Game Config 
	public bool DEMO = true;
	public int LEVEL_CHANGE = 200;


	public static GameConfig instance {
		get {
			GameObject gameConfig = GameObject.Find ("GameConfig");
			if (gameConfig == null) {
				gameConfig = Instantiate (Resources.Load ("Prefabs/Static/GameConfig")) as GameObject;
				gameConfig.name = "GameConfig";
			}
			_instance = gameConfig.GetComponent<GameConfig> ();
			
			return _instance;
		}
	}	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
