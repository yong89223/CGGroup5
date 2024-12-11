//Script for Django AI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DjangoAi : AIController {


	
	// Update is called once per frame
	public override void Update () {
		if (FireRateDelay > 0)
			FireRateDelay -= Time.deltaTime;
		float dist = 9999;//set distance to maximum

		//Find closest visible alive enemy
		closestenemy = null;
		foreach (GameObject go in GM.GetEnemyTeam(team)) {
			if (dist > Vector3.Distance (transform.position, go.transform.position) && go.activeInHierarchy) {
				if (go == MainPlayer.gameObject) {
					if (MainPlayer.isInvisible || MainPlayer.isForcedInvisible)
						continue;
				} else if (go.GetComponent<AIController> ().isInvisible || go.GetComponent<AIController> ().isForcedInvisible)
					continue;
				dist = Vector3.Distance (transform.position, go.transform.position);
				closestenemy = go.transform;
			}
		}
		//Done finding closest enemy



		//refill ammo at given rate if ammo < 3
		if (CurrentAmmo < 3) {
			CurrentAmmo += Time.deltaTime / AmmoFillTime;
			AmmoImage.fillAmount = CurrentAmmo / 3;
		}
		else
			CurrentAmmo = 3;


		//if ultimate is ready and enemy is in range fire ultimate
		if (closestenemy && CurrentUltimateCharge >=1&& dist<=4) {

			CurrentUltimateCharge = 0;
			UltimateLauncher.Fire ();
		}
	

		//if health is low run to a safe point until health goes back up
		if (GetComponent<HealthController> ().Health < GetComponent<HealthController> ().MaxHealth / 3) {
			if(currentSafe==null)
				currentSafe = SafePoints[ Random.Range (0, SafePoints.Length)];

			if (closestenemy!=null && Vector3.Distance (closestenemy.position, currentSafe.position) < 5) {
				currentSafe = SafePoints [Random.Range (0, SafePoints.Length)];
			}

			Navi.SetDestination (currentSafe.position);
			GetComponent<Animator> ().SetFloat ("Speed", 1);
		}


		//if health is ok run to nearest enemy
		else {
			if (closestenemy && dist > 3) {
					Navi.SetDestination (closestenemy.transform.position);
					GetComponent<Animator> ().SetFloat ("Speed", 1);
			} else {
				Navi.SetDestination (transform.position);
				GetComponent<Animator> ().SetFloat ("Speed", 0);
			}
		}


		//if enemy is in fire range and gun is not empty :: fire at current fire rate
		if (dist < MaxDistance + 2 && FireRateDelay <= 0 && CurrentAmmo>=1) {
			Gun.transform.LookAt (closestenemy);
			CurrentAmmo -= 1;
			if (team != MainPlayer.team) {
				Gun.Fire (Gun.transform.rotation, 12);
			} else {
				Gun.Fire (Gun.transform.rotation, 8);
			}
			FireRateDelay = MyFireRate;

		}
	}
}
