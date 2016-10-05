using UnityEngine;
using System.Collections;

public class sc_tmp_player_input : MonoBehaviour {

    public GameObject enemy;
    public GameObject weapoon;

    bool pause = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            sc_event_controller.player_tactic_move_event(0, 1);
        if (Input.GetKeyDown(KeyCode.S))
            sc_event_controller.player_tactic_move_event(0, -1);
        if (Input.GetKeyDown(KeyCode.A))
            sc_event_controller.player_tactic_move_event(-1, 0);
        if (Input.GetKeyDown(KeyCode.D))
            sc_event_controller.player_tactic_move_event(1, 0);
        if (Input.GetKeyDown(KeyCode.Space))
            sc_event_controller.end_tactik_phase_event();
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pause)
                Time.timeScale = 1f;
            else
                Time.timeScale = 0f;
            pause = !pause;
        }
        if (Input.GetKeyDown(KeyCode.F))
            sc_event_controller.player_tactic_shot_event(enemy, weapoon);
        if (Input.GetKeyDown(KeyCode.T))
            sc_event_controller.player_tactic_aim_event(enemy, weapoon);
    }
}
