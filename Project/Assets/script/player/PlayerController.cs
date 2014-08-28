using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	private Vector3 camEuler = Vector3.zero;
	private Vector3 lastMouse = Vector3.zero;

	private Quaternion playerTarg;
	private Quaternion camTarg;
	
	private float camRotAgl; 
	private bool grappling = false;
	// Use this for initialization
	void Start () {
		lastMouse = Input.mousePosition;
		playerTarg = transform.rotation;
		camTarg = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {

		HandleSnapToPlatform();

		Vector3 mouseDelta = getMouseDelta();
		camEuler.y = -mouseDelta.x;
		camRotAgl += mouseDelta.y;
		Quaternion plyrRot = Quaternion.Euler (new Vector3(0,camEuler.y,0));
		playerTarg = playerTarg * plyrRot;
		
		camRotAgl = Mathf.Clamp (camRotAgl, -90, 60);
		Quaternion camRot = Quaternion.Euler(camRotAgl,0,0);
		Transform cam = transform.GetChild(0).transform;
		
		camTarg = Quaternion.identity * transform.rotation * camRot;

		if (!grappling) {
			transform.rotation = Quaternion.Slerp (transform.rotation,
				                                   playerTarg, 0.3f);
			cam.rotation = Quaternion.Slerp (cam.rotation, 
		                                camTarg,
		                                 0.3f);
		}
		
	}
	
	private Vector3 getMouseDelta(){
		Vector3 delt = lastMouse - Input.mousePosition;
		lastMouse = Input.mousePosition;
		return delt;
	}
	
	void UpdatePlayerTarg(Quaternion targ){
		playerTarg = targ;
	}

	RaycastHit? CastRayToPlatform(Ray ray){
		RaycastHit info;
		if (Physics.Raycast (ray, out info)) {
			Debug.DrawRay(info.transform.position,info.normal);
			return info;
		}
		return null;
		
	}

	private void HandleSnapToPlatform(){
		if (Input.GetMouseButtonDown(0)) {
			Vector3 screenpoint = Input.mousePosition;
			Debug.DrawRay(transform.position, Camera.main.ScreenToWorldPoint(screenpoint));

			RaycastHit? possiblePlatform = 
				CastRayToPlatform(Camera.main.ScreenPointToRay(screenpoint));
			if(possiblePlatform.HasValue){
				Vector3 forward = Vector3.Cross (transform.right,possiblePlatform.Value.normal);
				if(Vector3.Dot(transform.up,possiblePlatform.Value.normal) <  -0.95)
					forward = -forward;
				playerTarg = 
					Quaternion.LookRotation(forward, possiblePlatform.Value.normal);
				StartCoroutine("lerpToPlatform",
				               possiblePlatform.Value.point + possiblePlatform.Value.normal*transform.localScale.y);
			}

		}
	}

	IEnumerator lerpToPlatform(Vector3 platformPos)
	{
		grappling = true;
		Vector3 init = transform.position;
		Quaternion rotInit = transform.rotation;
		float dist = Vector3.Distance (transform.position, platformPos);
		float accr = 0.05f;
		for(float timer = 0; (timer+accr)/dist < 1 ; timer += accr){
			accr += 0.05f;
			transform.position = Vector3.Lerp(init,platformPos,timer/dist);
			transform.rotation = Quaternion.Slerp(rotInit,playerTarg,timer/dist);
			yield return null;
		}
		grappling = false;
		transform.position = platformPos;
	}

}