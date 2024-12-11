//Health Script used on both player and AIs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthController : MonoBehaviour {
	[HideInInspector]
	public float MaxHealth,Health;
	public Image HealthBar;
	float hittimer;
	AIController MyAIController;
	PlayerController MyPlayerController;
	GameManager GM;


	// Use this for initialization
	void Start () {
		GM = FindObjectOfType<GameManager> ();
		MyAIController = GetComponent<AIController> ();
		MyPlayerController= GetComponent<PlayerController> ();

		if (MyAIController) {
//			MaxHealth = MyAIController.BaseHealth;
		} else
			MaxHealth = MyPlayerController.BaseHealth;
		Health = MaxHealth;
		hittimer = 0;
		HealthBar.fillAmount = Health / MaxHealth;

	}
	void OnEnable () {
		Health = MaxHealth;
		hittimer = 0;
		HealthBar.fillAmount = Health / MaxHealth;

	}

	//called from heal ultimate or update
	public void addHealth(float n)
	{
		if (Health < MaxHealth)
			Health += n;
		else
			Health = MaxHealth;
		HealthBar.fillAmount = Health / MaxHealth;

	}

	//called from enemy bullet on collision
	public void DeductHealth(float n)
	{
		Health -= n;
		HealthBar.fillAmount = Health / MaxHealth;
		hittimer = 2f;
		if (Health <= 0)
			GM.KillPlayer (this.gameObject);

	}
	// Update is called once per frame
	void Update () {
		if (hittimer > 0)
			hittimer -= Time.deltaTime;
		if (hittimer <= 0 && Health < MaxHealth) {
			Health += Time.deltaTime * 50;
			HealthBar.fillAmount = Health / MaxHealth;

		}
	}
}
