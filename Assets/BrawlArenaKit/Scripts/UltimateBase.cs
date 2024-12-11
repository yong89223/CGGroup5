//Base Class used for Ultimate weapons :: Fire() called by AI and Player Scripts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateBase : MonoBehaviour {
	public Animator PlayerAnim;
	public PlayerController Player;
	public AIController AI;
	// Use this for initialization
	public virtual void Fire () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
