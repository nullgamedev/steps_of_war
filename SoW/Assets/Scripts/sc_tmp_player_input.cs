using UnityEngine;
using System.Collections;

public class sc_tmp_player_input : MonoBehaviour {

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
    }
}
