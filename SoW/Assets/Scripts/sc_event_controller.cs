using UnityEngine;
using System.Collections;

public class sc_event_controller : MonoBehaviour {

    //public static sEventControler _;

    public delegate void void_event_container();
    public delegate void float_event_container(float x);

    //public static event float_event_container player_walk;
    public static event void_event_container end_tactik_phase;
    public static event void_event_container end_war_phase;

    /*void Awake()
    {
        _ = this;
    }*/

    public static void end_tactik_phase_event()
    {
        end_tactik_phase();
    }
    /*public static void player_walk_event(float x)
    {
        player_walk(x);
    }*/
    public static void end_war_phase_event()
    {
        end_war_phase();
    }
}
