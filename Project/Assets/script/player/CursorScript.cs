using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour {
	public Texture2D cursor;
	public float scale;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI(){
		float xMin = (Screen.width / 2) - cursor.width * scale / 2;
		float yMin = (Screen.height / 2) - cursor.height*scale / 2;
		GUI.DrawTexture (new Rect (xMin, yMin, cursor.width*scale, cursor.height*scale), cursor);
	}
}
