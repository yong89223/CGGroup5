//Ultimate to heal all friendly player in radius over time
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealUltimate : UltimateBase {
	public GameObject HealParticles;
	GameManager gm;
	int team;
	public float timer=6;
	bool isactive=false;
	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameManager> ();
	}
	public override void Fire()//Activate ultimate
	{
		if (AI)
			team = AI.team;
		else
			team = Player.team;
		isactive = true;
		transform.root.GetComponent<Animator> ().speed = 1.5f;
		HealParticles.SetActive (true);
		StartCoroutine (deactivate ());
	}
	IEnumerator deactivate()//deactivate after a certain time
	{
		yield return new WaitForSeconds (timer);
		isactive = false;
		transform.root.GetComponent<Animator> ().speed = 0.85f;

		HealParticles.SetActive (false);
	}

	void OnDisable()
	{
		isactive = false;
		transform.root.GetComponent<Animator> ().speed = 0.85f;

		HealParticles.SetActive (false);
	}
	// Update is called once per frame
	void Update () {
		if (isactive) {
			//if Ultimate is activate heal all players from team which are in radius
			if (team == 0) {
				foreach (GameObject go in gm.Team1Player) {
					if (Vector3.Distance (go.transform.position, transform.position )< 3.7f) {
						HealthController hc = go.GetComponent<HealthController> ();
						hc.addHealth (Time.deltaTime * 100);
					}
				}
			}
			if (team == 1) {
				foreach (GameObject go in gm.Team2Player) {
					if (Vector3.Distance (go.transform.position, transform.position) < 3.7f) {
						HealthController hc = go.GetComponent<HealthController> ();
						hc.addHealth (Time.deltaTime * 100);
					}
				}
			}
		}
	}
}
