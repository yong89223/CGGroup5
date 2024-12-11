using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMode : MonoBehaviour {
	public enum GunTypes // your custom enumeration
	{
		Shotgun,//spread type gun 
		Single, //single shot gun
		Machine,//multiple shot gun
		Throwable//grenade type gun
	};
	public GunTypes GunType;
	public GameObject BulletPrefab;
	public int BulletCount;//bullets to be fired for multiple and shotgun types
	public int ShotgunSpreadAngle;//angle between first and last bullet
	public int MachineFireRate;//fire rate for machine type gun
	public float BulletVelocity;
	public float BulletLife;
	Vector3 initialPosition;
	// Use this for initialization
	void Start () {

	}
	public void Fire(Quaternion Direction,int layer,Vector3 ThrowableLocation=default(Vector3))
	{
		switch (GunType) {
		case GunTypes.Shotgun:
			float angle=-ShotgunSpreadAngle/2;
			for (int i = 0; i < BulletCount; i++) {
				Vector3 bulletAngle=new Vector3(0,Direction.eulerAngles.y+angle,0);//spread all bullets across angle
				angle += ShotgunSpreadAngle / BulletCount;
				GameObject go = Instantiate (BulletPrefab, transform.position, Quaternion.Euler (bulletAngle));
				go.layer = layer;

				//assign AI/Player script for updating ultimate charge
				if (transform.root.GetComponent<PlayerController> ())
					go.GetComponent<BulletScript> ().Player = transform.root.GetComponent<PlayerController> ();
				else
					go.GetComponent<BulletScript> ().AI = transform.root.GetComponent<AIController> ();


				go.SetActive (true);
				go.GetComponent<Rigidbody> ().velocity = go.transform.forward * BulletVelocity;
				Destroy (go, BulletLife);
			}
			break;
		case GunTypes.Machine:
			float timer = 0;
			Vector3 BulletAngle=new Vector3(0,Direction.eulerAngles.y,0);

			//fire machine bullets over time using coroutine
			for (int i = 0; i < BulletCount; i++) {
				if (gameObject.activeInHierarchy)
					StartCoroutine (ActivateBullet (timer,layer,Quaternion.Euler(BulletAngle)));
				timer += 1f / MachineFireRate;
			}
			break;
		case GunTypes.Throwable:
			GameObject projectile = Instantiate (BulletPrefab, transform.position, transform.rotation);
			Vector3 horizontalForce = ThrowableLocation - transform.position;
			projectile.layer = layer;

			//assign AI/Player script for updating ultimate charge
			if (transform.root.GetComponent<PlayerController> ())
				projectile.GetComponent<BulletScript> ().Player = transform.root.GetComponent<PlayerController> ();
			else
				projectile.GetComponent<BulletScript> ().AI = transform.root.GetComponent<AIController> ();
			projectile.SetActive (true);
			projectile.GetComponent<Rigidbody> ().AddForce (horizontalForce.x * 25, 500, horizontalForce.z * 25);//calculation for throwing grenade at designated position
			break;
		}

	}

	//for firing multiple machine bullets over time
	IEnumerator ActivateBullet(float timer,int layer,Quaternion Direction)
	{
		yield return new WaitForSeconds (timer);
		GameObject go = Instantiate (BulletPrefab, transform.position, Direction);
		go.layer = layer;

		//assign AI/Player script for updating ultimate charge
		if (transform.root.GetComponent<PlayerController> ())
			go.GetComponent<BulletScript> ().Player = transform.root.GetComponent<PlayerController> ();
		else
			go.GetComponent<BulletScript> ().AI = transform.root.GetComponent<AIController> ();
		go.SetActive (true);
		go.GetComponent<Rigidbody> ().velocity = go.transform.forward * BulletVelocity;
		Destroy (go, BulletLife);
	}
}
