using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject character;
	public GameObject mapController;
	public GameObject starController;
	
	private static GameController _instance = null;

	public Controller controller = null;

	
	public static GameController instance {
		get {
			GameObject obj = GameObject.Find ("GameController");
			if (obj == null) {
				obj = Instantiate (Resources.Load ("Prefabs/Static/GameController")) as GameObject;
				obj.name = "GameController";
			}
			_instance = obj.GetComponent<GameController> ();
			
			return _instance;
		}
	}
	// Use this for initialization
	void Start () {
		character = GameObject.Find (GameConfig.instance.CHARACTER_NAME);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
