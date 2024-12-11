// Base Script for all AI scripts

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class AIController : MonoBehaviour {

	[Header("Base Stats")]
	[Space(5)]
	public float BaseSpeed = 1f;
	public float BaseHealth;
	public float InitialUltimateCharge=0;
	public int team; // team 0 spawns at bottom, team 1 spawns at top

	[Space(20)]
	[Header("Weapon Settings")]
	[Space(5)]
	public FireMode Gun;
	public UltimateBase UltimateLauncher;
	public float MyFireRate=1.2f;//time between each shot
	public float AmmoFillTime;//ammo refill time in seconds
	public float CurrentAmmo,CurrentUltimateCharge;
	protected float FireRateDelay;//counter for fire rate



	[Space(20)]
	[Header("Materials & Textures")]
	[Space(5)]
	public Renderer AIModel;
	public Material NormalAI,InvisAIFriendly,InvisAIEnemy;
	public bool isInvisible, isForcedInvisible;//isinvisible is used when player is in grass, isforcedinvisible is used for ultimates such as the ninja ultimate


	[Space(20)]
	[Header("UI Settings")]
	[Space(5)]
	public Text NameText;
	public CanvasGroup MyStats;
	public Image AmmoImage;


	[Space(20)]
	[Header("AI Settings")]
	[Space(5)]

	public NavMeshAgent Navi;
	public Transform[] SafePoints; //points to hide when health is low
	protected Transform currentSafe;
	public float MaxDistance,MinDistance;//maxdistance enemy range for AI to fire
	public Transform closestenemy;
	public PlayerController MainPlayer;

	//Other variables
	int grasscount;
	protected GameManager GM;



	void Start () {
		Navi.speed = BaseSpeed*2.5f;

		CurrentUltimateCharge = InitialUltimateCharge;
		GM = FindObjectOfType<GameManager> ();
		MainPlayer = FindObjectOfType<PlayerController> ();
		CurrentAmmo = 3;
		AmmoImage.fillAmount = 1;
		FireRateDelay = 0f;



		//set layers based on AI team
		if (MainPlayer.team == team) {
			gameObject.layer = 9;
		} else {
			gameObject.layer = 10;
		}

		//set name text colors based on AI team
		NameText.color = Color.red;
		if (team == MainPlayer.team)
			NameText.color = Color.green;
	}


	//called from bullet script when it collides with an enemy
	public void AddUltimate(float n)
	{
		CurrentUltimateCharge += n;
		if (CurrentUltimateCharge > 1) {
			CurrentUltimateCharge = 1;

		}
	}

	//called from (Ninja) ultimate to force AI into invisibility 
	public void ForceInvisible()
	{
		isForcedInvisible = true;
		if (team == MainPlayer.team)
			AIModel.material = InvisAIFriendly;
		else {
			AIModel.material = InvisAIEnemy;
			MyStats.alpha = 0;
		}
	}

	//called from (Ninja) ultimate to force AI out of invisibility 
	public void ForceVisible()
	{
		isForcedInvisible = false;
		MyStats.alpha = 0.75f;
		if(!isInvisible)
			AIModel.material = NormalAI;
	}


	//reset invisibilty on death
	public void DeathReset()
	{
		grasscount = 0;
		Navi.speed = BaseSpeed*2.5f;
		isForcedInvisible = false;
		MyStats.alpha = 0.75f;
		isInvisible = false;
		AIModel.material = NormalAI;
	}


	//keeps track of Grass collisions if collisions are > 0 make AI invisible
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Grass") {
			grasscount++;
			if (grasscount == 1) {
				isInvisible = true;
				if (team == MainPlayer.team)
					AIModel.material = InvisAIFriendly;
				else {
					AIModel.material = InvisAIEnemy;
					MyStats.alpha = 0;
				}

			}
		}
	}

	//keeps track of Grass collisions if collisions are = 0 make AI visible
	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Grass") {
			grasscount--;
			if (grasscount == 0) {
				isInvisible = false;
				MyStats.alpha = 0.75f;

				if(!isForcedInvisible)
					AIModel.material = NormalAI;
			}
		}
	}



	// Update is called once per frame
	public virtual void Update () {

		//For AI Scripting in Inherited Class

	}
}
