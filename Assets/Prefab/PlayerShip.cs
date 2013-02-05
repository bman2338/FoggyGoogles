using UnityEngine;
using System.Collections;
using SecretSauce;
public class PlayerShip : MonoBehaviour {
	
	Mesh shipMesh {get;set;}
	// Use this for initialization
	//Pipe width = roughly 3.5

	private Vector3 lastPull;
	private Vector3 lastForward;
	private float lastAmnt;
	//for the second ray trace to find the spine of the track
	private Vector3 NO_HIT = new Vector3(999999,999999,999999);
	private float SEARCH_INC = .1f;
	private Vector3 center = Vector3.zero;
	
	private Vector3 ip;
	//transform.TransformDirection(normalFromMesh)
	void Start () {
		
		
		
		
	}

	void Update () {
	

		
		Debug.Log ("Euler's test y:" + transform.localRotation);
	

		if (Input.GetKey("left")) {
			transform.Rotate(new Vector3(0f,0,-1.0f));
		
			
		}
		
		if (Input.GetKey("right")) {
			transform.Rotate(new Vector3(0f,0,1.0f));
			
		}
		
	
		float maxVelocity = 4.3f;
	
		var v = rigidbody.velocity;
		if(v.magnitude > maxVelocity){
			rigidbody.velocity = Vector3.ClampMagnitude(v,maxVelocity);
			
		}
		//strafe
		if(Input.GetKeyDown ("a")){
			//transform.RotateAround(new Vector3());	
			//ChangeLane(transform, 5, 1);
			var rot = transform.rotation;
			rot.y = rot.y - 10.0f;
			transform.rotation = rot;
			
		}
		if(Input.GetKeyDown ("d")){
			//transform.RotateAround(new Vector3());	
			//ChangeLane(transform, -5, 1);
			
			var rot = transform.rotation;
			rot.y = 200.0f;
			transform.rotation = rot;
		}
	}
	
	private bool changing  = false;
	private float turn =  2.0f;
	
	void ChangeLane(Transform ship, float angle, float time){
		float t;
		float bank;
		if(changing) return;
		changing = true;
		for(t = 0; t < 1;){
			t+= 2*Time.deltaTime/time;
			turn = Mathf.Lerp (turn,angle,t);
			
			bank = 0.5f * turn;
			ship.localEulerAngles = new Vector3(bank,0,turn);
			return;
		}
		for(t = 0;t < 1;){
			t += 2* Time.deltaTime/time;
			turn = Mathf.Lerp (turn, 0, t);
			bank = 0.5f * turn;
			ship.localEulerAngles = new Vector3(turn,0,bank);
			return;
		}
		changing = false;
	}
	
	
	void FixedUpdate ()
	{
		RaycastHit hitinfo;
		Vector3 hitPoint = NO_HIT;
		float hoverHeight = .001f;
		if(Physics.Raycast(transform.position,-transform.forward,out hitinfo)){
			var amnt = Mathf.Max (1.0f,hitinfo.distance);
			var idealPosition = transform.position + 
				((hoverHeight-hitinfo.distance) * transform.forward);
			var pull = (idealPosition - transform.position);
			
			transform.rigidbody.AddForce(-hitinfo.normal * (9800 * amnt * amnt * amnt));
			//transform.forward = hitinfo.normal;
			Debug.Log ("Forward:" + transform.forward);
			ip = idealPosition;
			hitPoint = hitinfo.point;
			lastPull = pull;
			lastForward = hitinfo.normal;
			lastAmnt = amnt;
		}
		else{
			transform.rigidbody.AddForce(-lastForward * (9800 * lastAmnt * lastAmnt * lastAmnt));
		}
		
	    if (Input.GetKey("up")) {
			rigidbody.AddForce(transform.up* 6400);
		}
		if (Input.GetKey("down")) {
			rigidbody.AddForce(-transform.up * 6400);
		}
		
	}
	void LateUpdate(){
		//transform.position = ip;
	}
	void OnCollisionEnter(Collision other){

	}
}
