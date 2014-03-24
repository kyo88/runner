using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapController : MonoBehaviour {

	private int NUMBER_OBJ=10;
	private int NUMBER_OBJ_IN_MODE=5;
	private int NUMBER_MAPS_TYPE=3;
	private int START_MAP = 0;
	private int END_MAP = 9;
	private int currentMapGen=0;
	private int currentMapType=0;
	private bool MAP_ON=false;
	private string[] mapsPath;
	private string[] wallsHardPath;
	private string wallPath;
	private bool wallFlag;

	
	public GameObject[] maps;

	private GameObject character;
	public GameObject gameControllerObj;
	public Controller controller = null;
	public GameObject enemyControllerObj;
	public EnemyController enemyController;
	public CointController coinController=null;

	GameController clt;

	private static MapController _instance = null;

	public List<GameObject> walls ;
	
	
	public static MapController instance {
		get {
			GameObject obj = GameObject.Find ("MapController");
			if (obj == null) {
				obj = Instantiate (Resources.Load (GameConfig.instance.MAP_CONTROLLER_PATH)) as GameObject;
				obj.name = "MapController";
			}
			_instance = obj.GetComponent<MapController> ();
			
			return _instance;
		}
	}

	// Use this for initialization
	void Start () {

		//init
		wallFlag = false;

		maps = new GameObject[NUMBER_OBJ];
		CreateMapOnStart();

		//Find GameController
		gameControllerObj = GameObject.Find ("GameController");
		clt = gameControllerObj.GetComponent <GameController>();
		//Debug.Log ("AAAAAAAAAAAAAAAAAAAAAA: sss" + clt.character.transform.position.x);

		//Find EnemyController
		enemyControllerObj = GameObject.Find ("EnemyController");
		enemyController = enemyControllerObj.GetComponent <EnemyController>();


		mapsPath = new string[NUMBER_MAPS_TYPE];
		mapsPath [0] = "Prefabs/Maps/way";
		mapsPath [1] = "Prefabs/Maps/way_duongreu";
		mapsPath [2] = "Prefabs/Maps/way_ground";
		wallPath = "Prefabs/Maps/wall_easy";
		wallsHardPath = new string[3];
		wallsHardPath [0] = "Prefabs/Maps/wall_hard_left";
		wallsHardPath [1] = "Prefabs/Maps/wall_hard_center";
		wallsHardPath [2] = "Prefabs/Maps/wall_hard_right";

		walls = new List<GameObject> ();

	}
	
	// Update is called once per frame
	void Update () {
		UpdateMaps ();
	}

	// Update Maps
	void UpdateMaps(){
		float distant = CaculateDistance ();
		//Debug.Log ("BBBBBBBBBBBBBBBB s" + distant + " - " + (NUMBER_OBJ - 5) * 28  );
		if (distant < (NUMBER_OBJ - 2) * 28) {
			int end = (int)maps [END_MAP].transform.position.z / 28;
			Destroy(maps[START_MAP]);
			maps[START_MAP] = null;
			for(int i =1 ;i < NUMBER_OBJ ; i++){
				maps[i-1] = maps[i];
			}

			//random map
			if(currentMapGen==NUMBER_OBJ_IN_MODE){
				currentMapGen = 0;
				currentMapType=(int)Random.Range(0.0F, 2.9F);
				Debug.Log ("MAP TYPE: " + currentMapType );
			}else{
				currentMapGen++;
			}

			maps[END_MAP] = Instantiate(Resources.Load(mapsPath[currentMapType])) as GameObject;
			maps[END_MAP].transform.position = new Vector3(0.0f  , 0.0f, (end + 1)*28f);
			maps[END_MAP].transform.parent = this.transform;
			AddWall(maps[END_MAP]);
			enemyController.AddEnemy(maps[END_MAP]);

			if(coinController){
				coinController.AddCoint(maps[END_MAP]);
			}else{
				coinController=GameObject.Find("CointController").GetComponent<CointController>();
				coinController.AddCoint(maps[END_MAP]);
				Debug.Log ("MapController CointController NULL!!!!");
			}

			//if(START_MAP)
		}
		// Check wall distance
		for (int i = walls.Count - 1; i >= 0; i--) {
			GameObject wall = walls[i];
			//Debug.Log ("CHECK");
			if(wall){
				float distance = wall.transform.position.z - clt.character.transform.position.z;
				//Debug.Log ("CHECK" + distance);
				if (distance < -35.0f) {
					walls.Remove(wall);
					Destroy(wall);
				}
			}else{
				walls.Remove(wall);
			}
		}
	}

	float CaculateDistance(){
		return maps [END_MAP].transform.position.z - clt.character.transform.position.z ;
	}

	//Create Map on Start
	void CreateMapOnStart(){
		for (int i=0; i<10; i++) {
			maps[i]=Instantiate(Resources.Load("Prefabs/Maps/way")) as GameObject;
			maps[i].transform.position = new Vector3(0.0f  , 0.0f, i*28f);
			maps[i].transform.parent = this.transform;
			//Debug.Log ("TE TE TE" + i);
		}
		MAP_ON = true;
	}

	//Get Map On
	public bool GetMapOn(){
		return MAP_ON;
	}

	private void AddWall(GameObject way){

		if(controller.LEVEL >= 3 && !wallFlag){
			if( controller.LEVEL == controller.LEVEL_MAX){
				int flag = (int)Random.Range(0.0F,4.0f);
				if(flag == 0){
					AddWallHard(way);
				}else{
					AddWallNormal(way);
				}
			}else{
				AddWallNormal(way);
			}
		}else{
				wallFlag = false;
		}

	}

	private void AddWallNormal(GameObject way){
		Debug.Log ("----- NORMAL -------");
		int flag = (int)Random.Range(0.0F, (float) (controller.LEVEL_MAX*2 + 1 - controller.LEVEL));
		if( flag == 0 ){
			GameObject wall = Instantiate(Resources.Load(wallPath)) as GameObject;
			wall.transform.position = new Vector3(0.0f  , 2.0f, way.transform.position.z);
			//wall.transform.localScale = new Vector3(-1.2f  , 1.0f, 0.4f);
			wall.transform.parent = this.transform;
			wall.name = "wall";
			walls.Add(wall);
			wallFlag = true;
		}

	}

	private void AddWallHard(GameObject way){
		Debug.Log ("----- HARD -------");
		int flag = (int)Random.Range(0.0F, 2.9F);
		GameObject wall_hard = Instantiate(Resources.Load(wallsHardPath[flag])) as GameObject;
		wall_hard.transform.position = new Vector3(0.0f  , 9.0f, way.transform.position.z);
		wall_hard.transform.parent = this.transform;
		walls.Add(wall_hard);
		wallFlag = true;
	}
}
