using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

	private int NUMBER_ZOMBIES =3;

	private string[] zombiesPath;

	private GameObject character;

	private GameController clt;

	public GameObject gameControllerObj;
	public Controller controller = null;


	public List<GameObject> enemys ;

	private static EnemyController _instance = null;
	
	
	public static EnemyController instance {
		get {
			GameObject obj = GameObject.Find ("EnemyController");
			if (obj == null) {
				obj = Instantiate (Resources.Load (GameConfig.instance.ENEMY_CONTROLLER_PATH)) as GameObject;
				obj.name = "EnemyController";
			}
			_instance = obj.GetComponent<EnemyController> ();
			
			return _instance;
		}
	}

	// Use this for initialization
	void Start () {
		zombiesPath = new string[NUMBER_ZOMBIES];
		zombiesPath[0]="Prefabs/Zombies/Zombie1";
		zombiesPath[1]="Prefabs/Zombies/Zombie2";
		zombiesPath[2]="Prefabs/Zombies/Zombie3";
		gameControllerObj = GameObject.Find ("GameController");
		clt = gameControllerObj.GetComponent <GameController>();
		character = clt.character;
		enemys = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {


		for (int i = enemys.Count - 1; i >= 0; i--) {
			GameObject enemy = enemys[i];
			//Debug.Log ("CHECK");
			if(enemy){
				float distance = enemy.transform.position.z - character.transform.position.z;
				//Debug.Log ("CHECK" + distance);
				if (distance < -35.0f) {
					enemys.Remove(enemy);
					Destroy(enemy);
				}
			}else{
				enemys.Remove(enemy);
			}
		}

	}

	public void AddEnemy(GameObject obj){

		int appear = (int)Random.Range(0.0F, (float) (controller.LEVEL_MAX + 1 - controller.LEVEL));

		if( appear == 0 || (controller.LEVEL == controller.LEVEL_MAX)){
			//random Enemy
			int e_id = (int)Random.Range(0.0F, 2.9F);

			int z_locate = (int)Random.Range( -1.9F, 1.9F);

			GameObject zombie = Instantiate(Resources.Load(zombiesPath[e_id])) as GameObject;
			if (zombie) {
				ZombieController zomCol = zombie.GetComponent<ZombieController>();
				if(zomCol){
					zomCol.STATE = EnemyState.Run;
					zombie.transform.position = new Vector3 (z_locate * -2.0f, 2.0f, obj.transform.position.z);
					zombie.transform.parent = this.transform;
					zombie.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
					zombie.name = "Zombie";
					enemys.Add(zombie);
				}

			}
		}
	}

}
