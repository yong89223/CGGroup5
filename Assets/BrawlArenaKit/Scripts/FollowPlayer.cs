using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	// The target we are following
	[SerializeField]
	public Transform target;
	// The distance in the x-z plane to the target
	[SerializeField]
	private float distance = 10.0f;
	// the height we want the camera to be above the target
	[SerializeField]
	private float height = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (!target)
			return;
		var wantedHeight = target.position.y + height;
		transform.position = target.position+(Vector3.back*distance);
		transform.position = new Vector3(transform.position.x ,wantedHeight , transform.position.z);
		transform.LookAt(target);
	}
}
