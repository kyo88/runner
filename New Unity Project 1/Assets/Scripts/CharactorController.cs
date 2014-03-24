using UnityEngine;
using System.Collections;

public enum CharacterState{
	Start,
	Run,
	Roll,
	Jump,
	TurnLeft,
	TurnRight,
	Die
};

public class CharactorController : MonoBehaviour {

	private Animator animator;

	private enum enMenuScreen {Main, Options, Extras};

	bool start = false;
	float posX;
	Vector3 pos;
	Vector3 posDie;
	public CharacterState state;
	public CharacterState nextState;
	public Controller controller;

	CapsuleCollider capsuleCollider ;

	private static CharactorController _instance = null;
	
	
	public static CharactorController instance {
		get {
			GameObject obj = GameObject.Find ("CharactorController");
			if (obj == null) {
				obj = Instantiate (Resources.Load ("Prefabs/Static/Charactor")) as GameObject;
				obj.name = "Charactor";
			}
			_instance = obj.GetComponent<CharactorController> ();
			
			return _instance;
		}
	}

	// Use this for initialization
	void Start () {

		Debug.Log ("NAME" + GameConfig.instance.name);
		state = CharacterState.Start;
		Debug.Log ("STATE" + state);
		capsuleCollider = gameObject.GetComponent<CapsuleCollider> ();
		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!controller.STATE.Equals (GameState.End)) {

						if (Input.GetKeyDown (KeyCode.L) && state.Equals (CharacterState.Start)) {
								//gameObject.renderer.material.color = Color.red;
								//Debug.Log ("I'm about to hit the ground! L");
								animator.SetFloat ("Start", 1);
								start = true;
								state = CharacterState.Run;
								controller.STATE = GameState.Start;
						}
						if (Input.GetKeyDown (KeyCode.R)) {
								if (state.Equals (CharacterState.Run)) {
										pos = transform.position;
										animator.SetFloat ("Roll", 1);
										state = CharacterState.Roll;
										nextState = CharacterState.Run;
								} else {
										nextState = CharacterState.Roll;
								}
						}
						if (Input.GetKeyDown (KeyCode.J)) {
								if (state.Equals (CharacterState.Run)) {
										pos = transform.position;
										animator.SetFloat ("Jump", 1);
										state = CharacterState.Jump;
										nextState = CharacterState.Run;
								} else {
										nextState = CharacterState.Jump;
								}
			
						}
						if (Input.GetKeyDown (KeyCode.RightArrow) && state.Equals (CharacterState.Run)) {
								if (start && transform.position.x < 3.0f) {
										transform.position = new Vector3 (transform.position.x + 2.0f, transform.position.y, transform.position.z);
										posX = transform.position.x;
								}
						}
						if (Input.GetKeyDown (KeyCode.LeftArrow) && state.Equals (CharacterState.Run)) {
								if (start && transform.position.x > -3.0f) {
										transform.position = new Vector3 (transform.position.x - 2.0f, transform.position.y, transform.position.z);
										posX = transform.position.x;
								}
						}
						float speed = (controller.LEVEL - 1) * 0.2f;
						transform.position = new Vector3 (posX, transform.position.y, transform.position.z + speed);
				} else {
				transform.position = posDie;
		}
	}
	void RunRight(){
		//Debug.Log ("RRRRRRRRRRRRRRRRRRRRRRB");
		animator.SetFloat("Right",9);
		animator.SetFloat("Left",9);
		animator.SetFloat("Jump",9);

	}

	void OnTriggerEnter(Collider other)
	{
		if(!GameConfig.instance.DEMO){
		//Destroy(other.gameObject);
			if (other.gameObject.name == "Zombie") {
				animator.SetFloat("Zombie",1);
				//Destroy(other.gameObject);
				ZombieController zom = other.gameObject.GetComponent<ZombieController>();
				if(zom){
					zom.StopMoving();
				}
				controller.STATE = GameState.End;
				posDie = this.gameObject.transform.position;
			}else if(other.gameObject.name == "coin"){
				controller.IncreaseCoin(1);
				Destroy(other.gameObject);
			}else if(other.gameObject.name == "wall"){
				animator.SetFloat("Zombie",1);
				controller.STATE = GameState.End;
				posDie = new Vector3 (this.gameObject.transform.position.x,this.gameObject.transform.position.y, this.gameObject.transform.position.z - 2.0f);
			}
		}  
	}

	public void RunBack(){
		if (state.Equals (CharacterState.Roll)) {
			animator.SetFloat ("Roll", -1.0f);
			transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
			CapsuleCollider capsuleCollider = gameObject.GetComponent<CapsuleCollider> ();
			capsuleCollider.height = 2.0f;
			capsuleCollider.center = new Vector3 (0.0f, 1.2f, 0.0f);

		}else if(state.Equals (CharacterState.Jump)){
			animator.SetFloat ("Jump", -1.0f);
			transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
			CapsuleCollider capsuleCollider = gameObject.GetComponent<CapsuleCollider> ();
			capsuleCollider.height = 2.0f;
			capsuleCollider.center = new Vector3 (0.0f, 1.2f, 0.0f);
		}
		if(!nextState.Equals(CharacterState.Run)){
			if(nextState.Equals(CharacterState.Roll)){
				pos = transform.position;
				animator.SetFloat("Roll",1);
				state = CharacterState.Roll;
			}else if(nextState.Equals(CharacterState.Jump)){
				pos = transform.position;
				animator.SetFloat("Jump",1);
				state = CharacterState.Jump;
			}
			nextState = CharacterState.Run;
		}else{
			animator.SetFloat ("Roll", -1.0f);
			animator.SetFloat ("Jump", -1.0f);
		}
		state = CharacterState.Run;
	}
	
	public void RollStart(){
		CapsuleCollider capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
		capsuleCollider.height = 1.3f;
		capsuleCollider.center = new Vector3(0.0f  , 0.6f, 0.0f);
	}

	public void SetCapsuleColliderJump(){
		capsuleCollider.center = new Vector3 (0.0f, 2.6f , 0.0f);
	}
}
