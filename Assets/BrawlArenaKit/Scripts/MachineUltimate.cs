//Ultimate to fire multiple long ranged bullets
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineUltimate : UltimateBase {
	public GameObject BulletPrefab;
	int mylayer;
	// Use this for initialization
	void Start () {
		if (Player)
			mylayer = 8;
		else if (AI.team == FindObjectOfType<PlayerController> ().team)
			mylayer = 8;
		else
			mylayer = 12;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public override void Fire()
	{

		float timer = 0;
		//Instantiate bullets after intervals in coroutine
		for (int i = 0; i < 20; i++) {
			StartCoroutine (ActivateBullet (timer,mylayer));
			timer += 0.1f;
		}
	}


	IEnumerator ActivateBullet(float timer,int layer)
	{
		yield return new WaitForSeconds (timer);
		GameObject go = Instantiate (BulletPrefab, transform.position+(transform.right*Random.Range(-0.2f,0.2f)), Quaternion.Euler(new Vector3(0,transform.rotation.eulerAngles.y,0)));
		go.layer = layer;
		if (transform.root.GetComponent<PlayerController> ())
			go.GetComponent<BulletScript> ().Player = transform.root.GetComponent<PlayerController> ();
		else
			go.GetComponent<BulletScript> ().AI = transform.root.GetComponent<AIController> ();
		go.SetActive (true);
		go.GetComponent<Rigidbody> ().velocity = go.transform.forward * 35;
		Destroy (go, 0.45f);
	}

}
