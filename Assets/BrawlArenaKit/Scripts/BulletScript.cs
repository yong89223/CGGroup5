using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	public PlayerController Player;
	public AIController AI;

	public GameObject impactParticle;//played on collision
	public GameObject projectileParticle;//bullet particle
	public GameObject muzzleParticle;
	public Vector3 impactNormal;
	public float UltimateChargePerHit;//charge to add in AI/Player script
	float timer;
	public float damage=35;
	public bool IsThrowable;//for grenade type objects
	public List<HealthController> enemiesinradius;//for grenade type objects upon collision
	private bool hasCollided = false;

	// Use this for initialization
	void Start () {

		// instantiate bullet and muzzel flash
		projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
		projectileParticle.transform.parent = transform;
		if (muzzleParticle){
			muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
			Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
		}

	}

	//used for grenade type bullets to deal damage to all enemies in impact radius
	void OnTriggerEnter(Collider hit)
	{
		if (hit.GetComponent<HealthController> ())
			enemiesinradius.Add (hit.GetComponent<HealthController> ());
	}

	//used for grenade type bullets to deal damage to all enemies in impact radius
	void OnTriggerExit(Collider hit)
	{
		if (hit.GetComponent<HealthController> ())
			enemiesinradius.Remove (hit.GetComponent<HealthController> ());
	}


	void OnCollisionEnter(Collision hit)
	{
		if (!hasCollided)
		{

			//for grenade type: upon impact deal damage to all enemies in radius
			if (IsThrowable) {
				foreach (HealthController hc in enemiesinradius) {
						hc.DeductHealth (damage);
					if (Player)
						Player.AddUltimate (UltimateChargePerHit);
					else if(AI)
						AI.AddUltimate (UltimateChargePerHit);
				}
			}

			//for bullet type check if collided object is AI or player (no collisions with same team players)
			else if (hit.gameObject.layer == 9 || hit.gameObject.layer == 10 || hit.gameObject.layer == 11) {
				if (Player)
					Player.AddUltimate (UltimateChargePerHit);
				else if(AI)
					AI.AddUltimate (UltimateChargePerHit);
					hit.gameObject.GetComponent<HealthController> ().DeductHealth (damage);

			}
			hasCollided = true;
			impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;

			if (hit.gameObject.tag == "Destructible") // Projectile will destroy objects tagged as Destructible
			{
				Destroy(hit.gameObject);
			}


			Destroy(projectileParticle, 3f);
			Destroy(impactParticle, 5f);
			Destroy(gameObject);

			ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
			for (int i = 1; i < trails.Length; i++)
			{
				ParticleSystem trail = trails[i];
				if (!trail.gameObject.name.Contains("Trail"))
					continue;
				trail.transform.SetParent(null);
				Destroy(trail.gameObject, 2);
			}
		}
	}
}
