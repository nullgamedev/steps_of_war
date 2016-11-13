using UnityEngine;
using System.Collections;

public class sc_touch_object : MonoBehaviour {
    public GameObject enemy;
    public GameObject weapoon;
	//public Touch_Objects Touch_Objects;
	// Use this for initialization
	void Start () {
       

    }

    // Update is called once per frame
    void Update() {
		/*if (Input.GetTouch.phase == TouchPhase.Began) 
		{
			sc_event_controller.player_tactic_aim_event(enemy, weapoon);
			 
		}*/
    }
	public void OnMouseDown ()
	{
		sc_event_controller.player_tactic_aim_event(enemy, weapoon);
		sc_Button_input.Count_of_Moves++;
		sc_Button_input.No_Aim = true;
		sc_Button_input.Enemy_to_Aim = 1;

	}
	
}
