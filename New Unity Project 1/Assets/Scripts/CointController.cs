using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CointController : MonoBehaviour {

	string coinPath="Prefabs/Other/coin";

	private List<GameObject> coins ;
	private GameObject character;
	
	private GameController clt;
	
	public GameObject gameControllerObj;
	public Controller controller = null;

	private static CointController _instance = null;
	
	
	public static CointController instance {
		get {
			GameObject obj = GameObject.Find ("CointController");
			if (obj == null) {
				obj = Instantiate (Resources.Load (GameConfig.instance.COIN_CONTROLLER_PATH)) as GameObject;
				obj.name = "CointController";
			}
			_instance = obj.GetComponent<CointController> ();
			
			return _instance;
		}
	}
	
	// Use this for initialization
	void Start () {
		coins = new List<GameObject> ();
		gameControllerObj = GameObject.Find ("GameController");
		clt = gameControllerObj.GetComponent <GameController>();
		character = clt.character;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = coins.Count - 1; i >= 0; i--) {
			GameObject coin = coins[i];
			//Debug.Log ("CHECK");
			if(coin){
				float distance = coin.transform.position.z - character.transform.position.z;
				//Debug.Log ("CHECK" + distance);
				if (distance < -15.0f) {
					coins.Remove(coin);
					Destroy(coin);
				}
			}else{
				coins.Remove(coin);
			}
		}	
	}

	public void AddCoint(GameObject obj){
		int z_locate = (int)Random.Range( -1.9F, 1.9F);
		float start = obj.transform.position.z;
		for (int i = 0; i < controller.LEVEL; i ++) {
						GameObject coin = Instantiate (Resources.Load (coinPath)) as GameObject;
						coin.transform.position = new Vector3 (z_locate * -2.0f, 2.5f, start + i*4.0f);
						coin.transform.parent = this.transform;
						coin.name="coin";
						//coin.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
						coins.Add (coin);
				}
	}
}
