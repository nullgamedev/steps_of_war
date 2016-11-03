using UnityEngine;
using System.Collections;

public class sc_event_controller : MonoBehaviour {

    //public static sEventControler _;

    public delegate void void_event_container();
    public delegate void intint_event_container(int x, int y);
    public delegate void gogo_event_container(GameObject a, GameObject b);

    public static event void_event_container end_tactik_phase;
    public static event void_event_container end_war_phase;
    public static event intint_event_container player_tactic_move;
    public static event gogo_event_container player_tactic_shot;
    public static event gogo_event_container player_tactic_aim;
    public static event void_event_container player_tactic_undo;

    /*void Awake()
    {
        _ = this;
    }*/

    public static void end_tactik_phase_event()
    {
        end_tactik_phase();
    }
    public static void end_war_phase_event()
    {
        end_war_phase();
    }
    public static void player_tactic_move_event(int x, int y)
    {
        player_tactic_move(x, y);
    }
    public static void player_tactic_shot_event(GameObject enemy, GameObject weapoon)
    {
        player_tactic_shot(enemy, weapoon);
    }
    public static void player_tactic_aim_event(GameObject enemy, GameObject weapoon)
    {
        player_tactic_aim(enemy, weapoon);
    }
    public static void player_tactik_undo_event()
    {
        player_tactic_undo();
    }
}
