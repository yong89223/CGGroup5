using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {
	public Transform Pivot;
	public static int CurrentChar;//static variable to pass character to next scene
	// Use this for initialization
	void Start () {
		CurrentChar = 0;
		Pivot.transform.eulerAngles = new Vector3 (0, 0, 0);
	}
	public void nextChar()
	{
		CurrentChar += 1;
		if (CurrentChar == 3)
			CurrentChar = 0;
	}
	public void StartGame()
	{
		SceneManager.LoadScene (1);
	}
	public void prevChar()
	{
		CurrentChar -= 1;
		if (CurrentChar == -1)
			CurrentChar = 2;
	}
	// Update is called once per frame
	void Update () {

		Pivot.transform.eulerAngles = Vector3.MoveTowards(Pivot.transform.eulerAngles,new Vector3 (0,CurrentChar*120 , 0),Time.deltaTime*360);

	}
}
