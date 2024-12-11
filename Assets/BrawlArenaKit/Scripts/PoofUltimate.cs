using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PoofUltimate : UltimateBase {


	public GameObject poof;
	// Use this for initialization
	public override void Fire () {
		GameObject poofEff = Instantiate (poof,poof.transform.position,poof.transform.rotation);
		poofEff.SetActive (true);
		Destroy (poofEff, 2);
		PlayerAnim.speed = 2;
		if (Player)
			Player.ForceInvisible ();
		else {
			AI.Navi.speed = AI.BaseSpeed*5;
			AI.ForceInvisible ();
		}
		Invoke ("StopMe",5);
	}
	


	void StopMe()
	{
		PlayerAnim.speed = 1;
		if (Player)
			Player.ForceVisible ();
		else {
			AI.Navi.speed = AI.BaseSpeed*2.5f;
			AI.ForceVisible ();
		}
	}
}
