using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsLookAt : MonoBehaviour {
	GameObject Cam;
	// Use this for initialization
	void Start () {
//		Cam = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.eulerAngles = new Vector3(-37,180,0);
	}
}
