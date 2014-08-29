using UnityEngine;
using System.Collections;

public class SimpleBullet : MonoBehaviour {
	public float speed;
	public float lifetime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifetime -= Time.deltaTime;
		transform.Translate (Vector3.forward * speed);
		if (lifetime <= 0) {
			Destroy(this.gameObject);
		}
	}
}
