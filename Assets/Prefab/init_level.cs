using UnityEngine;
using System.Collections;
using SecretSauce;
public class init_level : MonoBehaviour {

	// Use this for initialization
	private Camera cam;
	public PlayerShip ship;
	public GameObject pipe;
	//TODO:replace with various zoom levels
	void Start () {
		pipe.tag = "pipe";
		Physics.IgnoreCollision( ship.collider,pipe.collider);
		
		//flip normals of a pipe
		//Switch every second a third value of the triangle array
		//negate the normals
	}
	
	// Update is called once per frame
	void Update () {
		cam = Camera.mainCamera;
		cam.transform.parent = ship.transform;
			
	}
	
}
