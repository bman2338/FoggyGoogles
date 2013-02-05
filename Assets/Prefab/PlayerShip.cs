using UnityEngine;
using System.Collections;
using SecretSauce;
public class PlayerShip : MonoBehaviour {
	
	Mesh shipMesh {get;set;}
	//Pipe width = roughly 3.5

	private Vector3 lastPull;
	private Vector3 lastForward;
	private float lastAmnt;
	private Vector3 NO_HIT = new Vector3(999999,999999,999999);
	private float SEARCH_INC = .1f;
	private Vector3 center = Vector3.zero;
	
	private Vector3 ip;
	
	void Start () {
	}

	void Update () {
	
	

		if (Input.GetKey("left")) {
			transform.Rotate(new Vector3(0f,0,-1.0f));
			
		}
		
		if (Input.GetKey("right")) {
			transform.Rotate(new Vector3(0f,0,1.0f));
		}
		
		//strafe
		//SEE: http://answers.unity3d.com/questions/173817/how-to-rotate-a-car-smoothly.html
		if(Input.GetKeyDown ("a")){
			//ChangeLane(transform, 5, 1);
		}
		if(Input.GetKeyDown ("d")){
			//ChangeLane(transform, -5, 1);
		
		}
	}
	

	
	
	void FixedUpdate ()
	{
		RaycastHit hitinfo;
		if(Physics.Raycast(transform.position,-transform.forward,out hitinfo)){
			
			/**
			 * float hoverHeight = .001f;
			 * SEE: http://answers.unity3d.com/questions/173817/how-to-rotate-a-car-smoothly.html
			var idealPosition = transform.position + 
				((hoverHeight-hitinfo.distance) * transform.forward);
			var pull = (idealPosition - transform.position);
			ip = idealPosition;
			*/
			//lastPull = pull;
			
			var amnt = hitinfo.distance * 5;
			amnt = amnt * amnt;
			lastForward = hitinfo.normal;
			lastAmnt = amnt;
			
			
			//push up from the track to hover...
			//transform.rigidbody.AddForce( hitinfo.normal * (60 / Mathf.Max(0.15f,hitinfo.distance)));
			
			//push down for gravity
			transform.rigidbody.AddForce(-hitinfo.normal * ( 16000));
			
		}
		else{
			transform.rigidbody.AddForce(-lastForward * ( 16000));
			//transform.rigidbody.AddForce( lastForward * (60 / Mathf.Max(0.15f,hitinfo.distance)));
		}
		
	    if (Input.GetKey("up")) {
			rigidbody.AddForce(transform.up* 12400);
		}
		if (Input.GetKey("down")) {
			rigidbody.AddForce(-transform.up * 12400);
		}
		
		
		float maxVelocity = 5.3f;
	
		var v = rigidbody.velocity;
		if(v.magnitude > maxVelocity){
			rigidbody.velocity = Vector3.ClampMagnitude(v,maxVelocity);
		}
		
	}
	void LateUpdate(){
		
	}
	
	
	void OnCollisionEnter(Collision other){
		return;
		Debug.LogError("collide!");
		if(other.gameObject.tag == "pipe"){
			Debug.LogError("!");
			return;	
		}
	}
}
