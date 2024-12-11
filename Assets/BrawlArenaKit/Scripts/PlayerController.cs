using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	[Header("Base Stats")]
	[Space(5)]

	public float BaseSpeed;
	public float BaseHealth;
	public float InitialUltimateCharge=0;
	public int team;

	[Space(20)]
	[Header("Weapon Settings")]
	[Space(5)]

	public FireMode Gun;//Player Gun gameobject
	public UltimateBase UltimateLauncher;//Player ultimate launcher gameobject
	public GameObject GunPivot;//Aim : line for shotgun and machine, target for throwables
	public float AmmoFillTime;//in seconds
	float CurrentAmmo,CurrentUltimateCharge;
	public bool IsThrowable=false;

	[Space(20)]
	[Header("Materials & Textures")]
	[Space(5)]

	public Material InvisGrass;
	public Material NormalGrass;
	public Material InvisPlayer,NormalPlayer;
	public Renderer PlayerModel;
	public bool isInvisible, isForcedInvisible;//isinvisible keeps track of invisbility from grass, isforcedinvisible keeps track of invisibility from Ultimates


	[Space(20)]
	[Header("UI Settings")]
	[Space(5)]

	public Image AmmoAmountBar;
	public Image UltimateChargeImage;
	public Animator skullAnimator;
	public LeftJoystick LeftJ;
	public RightJoystick RightJ;



	//other required variables
	float xDir,yDir; 				//Move direction values from joysticks
	float BulletXDir,BulletYDir; 	//Aim direction values from joysticks
	bool isAiming; 		
	float dragtimer;				//To check if player has held aim joystick long enough to enable aiming
	int grasscount;					//used for checking if player is in grass
	GameManager GM;

	//invis ultimate is used
	public void ForceInvisible()
	{
		isForcedInvisible = true;
		PlayerModel.material = InvisPlayer;
	}

	//invis ultimate is deactivated
	public void ForceVisible()
	{
		isForcedInvisible = false;
		if(!isInvisible)
			PlayerModel.material = NormalPlayer;
	}

	//reset invisibility and speed on death
	public void DeathReset()
	{
		grasscount = 0;
		GetComponent<Animator> ().speed = BaseSpeed;
		isForcedInvisible = false;
		isInvisible = false;
		PlayerModel.material = NormalPlayer;

	}

	//keep track of grass collisions and keep player invisible if still colliding with grass
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Grass") {
			grasscount++;
			if (grasscount == 1) {
				isInvisible = true;
				PlayerModel.material = InvisPlayer;
			}
			col.gameObject.GetComponentInChildren<Renderer> ().material = InvisGrass;
		}
	}

	//make player invisible when not colliding with grass
	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Grass") {
			grasscount--;
			if (grasscount == 0) {
				isInvisible = false;
				if(!isForcedInvisible)
					PlayerModel.material = NormalPlayer;
			}
			col.gameObject.GetComponentInChildren<Renderer> ().material = NormalGrass;
		}
	}


	// Use this for initialization
	void Start () {
		GetComponent<Animator> ().speed = BaseSpeed;
		GunPivot.SetActive (false);
		CurrentUltimateCharge = InitialUltimateCharge;
		UltimateChargeImage.fillAmount = CurrentUltimateCharge;
		CurrentAmmo = 3;
		dragtimer = 0;
		isAiming = false;
		RightJ.ondragstarted  += MarkIsAiming ;
		RightJ.ondragended  += MarkIsNotAiming ;
		if (CurrentUltimateCharge >= 1) {
			CurrentUltimateCharge = 1;
			skullAnimator.enabled = true;
		}

	}

	//called from bullet when it collides with an enemy
	public void AddUltimate(float n)
	{
		CurrentUltimateCharge += n;
		if (CurrentUltimateCharge >= 1) {
			CurrentUltimateCharge = 1;
			skullAnimator.enabled = true;
		}
		UltimateChargeImage.fillAmount = CurrentUltimateCharge;
	}

	//called from ultimate button assigned from gamemanager
	public void UseUltimate()
	{
		if (CurrentUltimateCharge >= 1&&gameObject.activeInHierarchy) {
			CurrentUltimateCharge = 0;
			UltimateChargeImage.fillAmount = CurrentUltimateCharge;
			UltimateLauncher.Fire ();
			skullAnimator.enabled = false;

		}
	}

	//called when aim button is released assigned in Start()
	void MarkIsNotAiming()
	{
		if (CurrentAmmo > 1) {
			CurrentAmmo -= 1;
			if (GunPivot.activeInHierarchy) {
				if(IsThrowable)
					Gun.Fire (GunPivot.transform.rotation, 8,GunPivot.transform.position);
				else
					Gun.Fire (GunPivot.transform.rotation, 8);

			} else {
				if(IsThrowable)
					Gun.Fire (transform.rotation, 8,GunPivot.transform.position+(transform.forward*5));
				else
					Gun.Fire (transform.rotation, 8);

			}

		}
		isAiming = false;
		GunPivot.SetActive (false);
		if (IsThrowable) {
				GunPivot.transform.position = new Vector3(transform.position.x,GunPivot.transform.position.y,transform.position.z);
		}
	}

	//called when aim button is pressed assigned in Start()
	void MarkIsAiming()
	{
		isAiming = true;
	}

	
	void Update () {

		//activate aim if aim joystick is held for more than 0.1 seconds, will fire towards player front if it is released before 0.1 seconds
		if (isAiming) {
			dragtimer += Time.deltaTime;
			if (dragtimer>0.1f)
				GunPivot.SetActive (true);

		}
		else
			dragtimer = 0;

		//change player rotation based on left joystick position and move player based on left joystick distance from center
		xDir = LeftJ.GetInputDirection ().x;
		yDir = LeftJ.GetInputDirection ().y;
		if (Mathf.Abs (xDir) > 0.1f || Mathf.Abs (yDir) > 0.1f) {
			GetComponent<Animator> ().SetFloat ("Speed", (Mathf.Abs (xDir)+Mathf.Abs (yDir)));
			transform.eulerAngles = new Vector3 (0, Mathf.Atan2(xDir,yDir)*Mathf.Rad2Deg, 0);
		}
		else
			GetComponent<Animator> ().SetFloat ("Speed",0);

		//for machine and shotgun rotate aim pivot
		if (!IsThrowable) {
			BulletXDir = RightJ.GetInputDirection ().x;
			BulletYDir = RightJ.GetInputDirection ().y;
			if (Mathf.Abs (BulletXDir) > 0.1f || Mathf.Abs (BulletYDir) > 0.1f) {
				GunPivot.transform.eulerAngles = new Vector3 (0, Mathf.Atan2 (BulletXDir, BulletYDir) * Mathf.Rad2Deg, 0);
			}
		} 

		//for grenade types move aim target based on right joystick position
		else {
			BulletXDir = RightJ.GetInputDirection ().x;
			BulletYDir = RightJ.GetInputDirection ().y;
			GunPivot.transform.position += new Vector3 (BulletXDir*Time.deltaTime*10, 0, BulletYDir*Time.deltaTime*10);

			//set range of throwable
			GunPivot.transform.localPosition = Vector3.ClampMagnitude (GunPivot.transform.localPosition, 70);
		}

		//refill ammo over time if less than 3
		if (CurrentAmmo < 3) {
			CurrentAmmo += Time.deltaTime/AmmoFillTime;
			AmmoAmountBar.fillAmount = CurrentAmmo / 3;
		}
	}
}
