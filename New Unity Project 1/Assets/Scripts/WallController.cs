using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other)
	{
		//Destroy(other.gameObject);
		if (other.gameObject.name == "Zombie") {
			Destroy(other.gameObject);
		}else if(other.gameObject.name == "coin"){
			Destroy(other.gameObject);
		}
	}
}
