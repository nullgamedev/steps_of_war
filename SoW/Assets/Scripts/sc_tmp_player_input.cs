using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class sc_tmp_player_input : MonoBehaviour {

    public GameObject enemy;
    public GameObject weapoon;
    public GameObject Move_Buttons;
    bool pause = false;

    public void Action_aim() {
        Move_Buttons.SetActive(false);
        sc_event_controller.player_tactic_shot_event(enemy, weapoon);
    }
    public void Action_fire() {
        Move_Buttons.SetActive(false);
        sc_event_controller.player_tactic_aim_event(enemy, weapoon);
    }

    public void Action_move() {
        Move_Buttons.SetActive(true);
    }

    public void Action_move_Up() {
        sc_event_controller.player_tactic_move_event(0, 1);
    }

    public void Action_move_Down(){
        sc_event_controller.player_tactic_move_event(0, -1);
    }

    public void Action_move_Right(){
        sc_event_controller.player_tactic_move_event(1, 0);
    }

    public void Action_move_Left(){
        sc_event_controller.player_tactic_move_event(-1, 0);
    }
    public void Action_LetsGo() {
        sc_event_controller.end_tactik_phase_event();
        Move_Buttons.SetActive(false);
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pause)
                Time.timeScale = 1f;
            else
                Time.timeScale = 0f;
            pause = !pause;
        }
    }
}
