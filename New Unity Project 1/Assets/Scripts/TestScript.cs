using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log(transform.position.x);
		
		if(transform.position.y <= 5f)
		{
			Debug.Log ("I'm about to hit the ground!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
		{
			gameObject.renderer.material.color = Color.red;
		}
		if(Input.GetKeyDown(KeyCode.G))
		{
			gameObject.renderer.material.color = Color.green;
		}
		if(Input.GetKeyDown(KeyCode.B))
		{
			gameObject.renderer.material.color = Color.blue;
		}	
		//transform.Translate(Vector3.forward * Time.deltaTime * 10);
		transform.position = new Vector3(transform.position.x +1  , transform.position.y, transform.position.z);
		Debug.Log ("I'm about to hit the ground!"+ Time.deltaTime);
		//transform.Translate(Vector3.up * Time.deltaTime, Space.World);
	}
	void OnCollisionEnter(Collision collision) {
		Debug.Log ("I'm about to hit the ground!");
	}
}
