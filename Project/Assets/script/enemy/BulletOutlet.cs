using UnityEngine;
using System.Collections;

public class BulletOutlet : MonoBehaviour {
	public GameObject bullet;
	public float delay;
	float timer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > delay) {
			Instantiate (bullet,transform.position,transform.rotation);
			timer = 0;
		}
		else{
			timer+=Time.deltaTime;
		}
	}
}
