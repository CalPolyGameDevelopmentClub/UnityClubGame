using UnityEngine;
using System.Collections;

public class PitchYawCamera : MonoBehaviour {
	
	private Vector3 camEuler = Vector3.zero;
	private Vector3 lastMouse = Vector3.zero;

	private Quaternion playerTarg;
	private Quaternion camTarg;

	private float camRotAgl;
	// Use this for initialization
	void Start () {
		lastMouse = Input.mousePosition;
		playerTarg = transform.rotation;
		camTarg = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mouseDelta = getMouseDelta();
		camEuler.y = -mouseDelta.x;
		camRotAgl += mouseDelta.y;
		Debug.Log(camRotAgl);
		Quaternion plyrRot = Quaternion.Euler (new Vector3(0,camEuler.y,0));
		playerTarg = playerTarg * plyrRot;

		camRotAgl = Mathf.Clamp (camRotAgl, -60, 60);
		Quaternion camRot = Quaternion.Euler(camRotAgl,0,0);
		Transform cam = transform.GetChild(0).transform;

		camTarg = Quaternion.identity * transform.rotation * camRot;
		transform.rotation = Quaternion.Slerp (transform.rotation,
			playerTarg, 0.3f);
		cam.rotation = Quaternion.Slerp (cam.rotation, 
		                                camTarg,
		                                0.3f);

	}
	
	private Vector3 getMouseDelta(){
		Vector3 delt = lastMouse - Input.mousePosition;
		lastMouse = Input.mousePosition;
		return delt;
	}

	void UpdatePlayerTarg(Quaternion targ){
		playerTarg = targ;
	}



}
