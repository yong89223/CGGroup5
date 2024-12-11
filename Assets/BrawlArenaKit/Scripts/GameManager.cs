using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
	public List<GameObject> Team1Player;
	public List<GameObject> Team2Player;
	public Transform t1Spawn,t2Spawn;//spawn points
	public GameObject [] PlayerPrefabs;//for instantiating selected player
	public Transform [] Team1SafePoints;
	public Transform [] Team2SafePoints;
	public Image RespwanTimer;
	bool isRespawning;
	int playerTeam;
	public GameObject Player;
	public Image UltimateChargeFillImage;
	public RightJoystick RJoy;
	public LeftJoystick LJoy;
	public FollowPlayer MainCam;
	public Animator skull;//skull animator turned on when ultimate is full
	public Button UltimateButton;


	// get a list of all enemies in game
	public List<GameObject> GetEnemyTeam(int myteam)
	{
		if (myteam == 0)
			return Team2Player;
		return Team1Player;
	}


	void Awake() {
		isRespawning = false;
		//Instantiate player, set camera target to player, set joystick targets to player, set Ultimate Button listener to player ultimate
		Player = Instantiate(PlayerPrefabs[MainMenu.CurrentChar]);
		Player.SetActive (true);
		Player.GetComponent<PlayerController> ().UltimateChargeImage = UltimateChargeFillImage;
		Player.GetComponent<PlayerController> ().RightJ = RJoy;
		Player.GetComponent<PlayerController> ().LeftJ = LJoy;
		MainCam.target = Player.transform;
		Player.GetComponent<PlayerController> ().skullAnimator = skull;
		Player.transform.position = new Vector3 (t1Spawn.position.x, 0.5f, t1Spawn.transform.position.z);
		UltimateButton.onClick.AddListener(Player.GetComponent<PlayerController> ().UseUltimate);


	}
	// Use this for initialization
	void Start () {

		//populate lists of AIs for both teams
		AIController[] AIs = FindObjectsOfType<AIController> ();
		foreach (AIController go in AIs) {
			if (go.team == 0) {
				go.SafePoints = Team1SafePoints;
				Team1Player.Add (go.gameObject);
			} else {
				go.SafePoints = Team2SafePoints;

				Team2Player.Add (go.gameObject);
			}
		}

		//add player to Player's team
		PlayerController pc = FindObjectOfType <PlayerController> ();
		if (pc.team == 0)
			Team1Player.Add (pc.gameObject);
		else
			Team2Player.Add (pc.gameObject);

	}
	
	//kill a character
	public void KillPlayer(GameObject Player)
	{
		if (Player.GetComponent<AIController> ())
			Player.GetComponent<AIController> ().DeathReset ();//reset invisiblity for AI
		else
		{
			Player.GetComponent<PlayerController> ().DeathReset ();//reset invisiblity for Player
			RespwanTimer.transform.parent.gameObject.SetActive(true);
			RespwanTimer.fillAmount = 0;
			isRespawning = true;//show and reset respawn countdown
		}
		Player.SetActive (false);
		StartCoroutine(Respawn(Player));//wait and respawn
	}

	void Update()
	{
		//show time till respawn
		if (isRespawning) {
			RespwanTimer.fillAmount += Time.deltaTime / 5;
			if (RespwanTimer.fillAmount >= 1) {
				isRespawning = false;
				RespwanTimer.transform.parent.gameObject.SetActive (false);
			}
		}
	}


	IEnumerator Respawn(GameObject Player)
	{
		yield return new WaitForSeconds (5);
		Player.SetActive (true);
		if(Team1Player.Find(o => o == Player))
		{
			Player.transform.position = new Vector3 (t1Spawn.position.x + Random.Range (-2.1f, 2.1f), t1Spawn.position.y, t1Spawn.position.z);
		}
		else 
			Player.transform.position = new Vector3 (t2Spawn.position.x + Random.Range (-2.1f, 2.1f), t2Spawn.position.y, t2Spawn.position.z);

	}

}
