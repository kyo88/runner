using UnityEngine;
using System.Collections;

public enum EnemyState{
	Begin,
	Run,
	Stop,
	Die
}

public class ZombieController : MonoBehaviour {

	private enum zombieType {One, Two, Three};

	public EnemyState STATE;
	// Use this for initialization
	void Start () {
		STATE = EnemyState.Run;
	}
	
	// Update is called once per frame
	void Update () {
		if(STATE.Equals(EnemyState.Run)){
			transform.position = new Vector3(transform.position.x  , transform.position.y, transform.position.z - 0.1f);
		}else if (STATE.Equals(EnemyState.Stop)){
			transform.position = new Vector3(transform.position.x  , transform.position.y, transform.position.z + 2.0f);
			STATE = EnemyState.Die;
		}
	}

	public void StopMoving(){
		Animation ani = gameObject.GetComponent<Animation> ();
		if(ani){
			//ani.Stop();
			STATE = EnemyState.Stop;
		}
	}
}
