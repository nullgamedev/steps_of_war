using UnityEngine;
using System.Collections;

public class sc_player : MonoBehaviour {

    public static sc_player _;
    public GameObject obj_marker;
    
    public sc_field_cell current_cell;
    enum phase_type { tactic, war };
    phase_type phase = phase_type.tactic;


    void Awake()
    {
        _ = this;
    }

    void Start()
    {
        transform.position = current_cell.transform.position;
        sc_event_controller.player_tactic_move += tactic_move;
    }

    void tactic_move(int x, int y)
    {
        Instantiate(obj_marker, transform.position, Quaternion.identity);
        if (x == 1)
            current_cell = current_cell.right;
        if (x == -1)
            current_cell = current_cell.left;
        if (y == 1)
            current_cell = current_cell.up;
        if (y == -1)
            current_cell = current_cell.down;
        transform.position = current_cell.transform.position;
        //Instantiate(obj_marker, transform.position, Quaternion.identity);
    }
}
