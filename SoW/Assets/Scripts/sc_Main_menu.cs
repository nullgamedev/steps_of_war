using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class sc_Main_menu : MonoBehaviour {

	public void StartGame() {
		SceneManager.LoadScene("roman_battle_area", LoadSceneMode.Single);
	
	}
	public void ExitGame() {
		Application.Quit ();
	
	}
	public void Option()  {
		
	
	
	}

}
