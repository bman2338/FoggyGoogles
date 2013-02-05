using UnityEngine;
using System.Collections;

public class PlayerCam : MonoBehaviour {

	// Use this for initialization
	public Vector3 offset;
	public PlayerShip ship;
	
	private float cameraVel;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.parent = ship.transform;
		
	}
	void LateUpdate(){

	}
}
