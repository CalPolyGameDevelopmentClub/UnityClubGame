using UnityEngine;
using System.Collections;

public class MoveToPlatform : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Vector3 screenpoint = Input.mousePosition;
			Debug.DrawRay(transform.position, Camera.main.ScreenToWorldPoint(screenpoint));
			CastRayToPlatform(Camera.main.ScreenPointToRay(screenpoint));
		}
	}

	void CastRayToPlatform(Ray ray){
		RaycastHit info;
		if (Physics.Raycast (ray, out info)) {
			Debug.DrawRay(info.transform.position,info.normal);
		}

	}

}
