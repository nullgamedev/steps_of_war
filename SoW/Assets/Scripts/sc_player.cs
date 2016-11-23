using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sc_player : MonoBehaviour {

    public static sc_player _;

    public GameObject obj_marker;
    public float duration_war_action = 0.8f;
    public Sprite sprite_cell_go;
    public Sprite sprite_cell_aim;
    public Sprite sprite_cell_fire;
    public GameObject popup_object;

	// created by Yurii .  IM SSSSSSSSSSOOOOOOOOOOOORRRRRRRRRRYYYYYYYYY

	public static float HitChangeForYurii;
	public static int HpForYurii;

	/// <summary>

	/// </summary>
	/// <value>The hitpoints.</value>

    public int hitpoints
    {
        get { return hp; }
        set {
            hp = Mathf.Max(value, 0);
            if (hp == 0)
                die();
        }
    }
    public sc_field_cell current_cell;
    public float dodge_chance = 0f;
    public float aim_effect = 0.5f;

    public int base_hp;
    int hp = 4;
    GameObject aim_weapoon;
    GameObject aim_target;
    int aim_count;
    action cur_action;

    enum phase_type { tactic, war }
    phase_type phase;

    float time = 0f;
    float time_start_action;
    bool was_shot;
    Vector3 current_war_position;
    enum action_type { go, aim, fire, _somersault }
    struct action
    {
        public action_type type;
        public GameObject target;
        public GameObject weapoon;
    }
    List<GameObject> marker_list;
    List<action> action_list;
    Animator animator;


    void Awake()
    {
        _ = this;
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        hp = base_hp;
        marker_list = new List<GameObject>();
        action_list = new List<action>();
        sc_event_controller.player_tactic_move += tactic_move;
        sc_event_controller.end_war_phase += start_tactic_phase;
        sc_event_controller.end_tactik_phase += start_war_phase;
        sc_event_controller.player_tactic_shot += tactic_shot;
        sc_event_controller.player_tactic_aim += tactic_aim;
        sc_event_controller.player_tactic_undo += tactic_undo;
        start_tactic_phase();
    }

    void calc_stats_for_war_action()
    {
        cur_action = action_list[0];
        //set animation
        switch(cur_action.type)
        {
            case action_type.go:
            {
                animator.SetTrigger("walk");
            } break;
            case action_type._somersault:
            {
                if (cur_action.target.transform.position.x - transform.position.x > 0.2f)
                    animator.SetTrigger("somersault_right");
                if (transform.position.x - cur_action.target.transform.position.x > 0.2f)
                    animator.SetTrigger("somersault_left");
                if (transform.position.y - cur_action.target.transform.position.y > 0.2f)
                    animator.SetTrigger("somersault_down");
                if (cur_action.target.transform.position.y - transform.position.y > 0.2f)
                    animator.SetTrigger("somersault_up");
            } break;
            case action_type.aim:
            case action_type.fire:
            {
                animator.SetTrigger("aim");
            } break;
        }
        //other calcs
        was_shot = false;
        bool aim_ok = false;
        if (cur_action.type == action_type.aim)
        {
            aim_ok = true;
            if (cur_action.target == aim_target && cur_action.weapoon == aim_weapoon)
                aim_count++;
            else
            {
                aim_count = 1;
                aim_target = cur_action.target;
                aim_weapoon = cur_action.weapoon;
            }
        }
        if (cur_action.type == action_type.fire && cur_action.target == aim_target && cur_action.weapoon == aim_weapoon)
            aim_ok = true;
        if (!aim_ok)
            aim_count = 0;

        if (cur_action.type == action_type.go)
            dodge_chance = 0.5f;
        else if (cur_action.type == action_type._somersault)
            dodge_chance = 1f;
        else dodge_chance = 0f;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (phase == phase_type.war)
        {
            if (time > time_start_action + duration_war_action)
            {
                time_start_action += duration_war_action;
                action_list.RemoveAt(0);
                if (action_list.Count == 0)
                {
                    sc_event_controller.end_war_phase_event();
                    return;
                }
                calc_stats_for_war_action();
            }
            switch (cur_action.type)
            {
                case action_type.go:
                case action_type._somersault:
                    {
                        Vector3 dist = cur_action.target.transform.position - transform.position;
                        float left_time = time_start_action + duration_war_action - time;
                        dist /= left_time;
                        dist *= Time.deltaTime;
                        transform.position += dist;
                    } break;
                case action_type.fire:
                    {
                        sc_weapoon w = cur_action.weapoon.GetComponent<sc_weapoon>();
                        if (!was_shot && time > time_start_action + w.time_shooting * duration_war_action)
                        {
                            animator.SetTrigger("fire");
                            float chance_hit = w.chance_hit * (1f + aim_count * aim_effect) - cur_action.target.GetComponent<sc_enemy>().dodge_chance;
							HitChangeForYurii = chance_hit;
                            w.shot(transform.position, cur_action.target.transform.position);
                            sc_popup_text a = Instantiate<GameObject>(popup_object).GetComponent<sc_popup_text>();
                            a.world_position = cur_action.target.transform.position;
                            if (Random.Range(0f, 1f) < chance_hit)
                            {
                                a.text = "-" + w.damage.ToString();
                                cur_action.target.GetComponent<sc_enemy>().hitpoints -= w.damage;
                            }
                            else
                                a.text = "miss";
                            a.text = chance_hit.ToString() + " | " + a.text;//!!!!!!!!!!!!!!!!!!!
                            was_shot = true;
                        }
                    } break;
            }
        }
		HpForYurii = hitpoints;

    }

    void start_tactic_phase()
    {
        animator.SetTrigger("stay");
        current_war_position = transform.position;
        transform.position = current_cell.transform.position;
        phase = phase_type.tactic;
        if (Vector3.Distance(current_war_position, transform.position) > 0.1)
        {
            action tmp = new action();
            tmp.type = action_type.go;
            tmp.target = current_cell.gameObject;
            action_list.Add(tmp);
        }
    }

    void tactic_move(int x, int y)
    {
        GameObject a = Instantiate<GameObject>(obj_marker);
        a.transform.position = transform.position;
        a.GetComponent<SpriteRenderer>().sprite = sprite_cell_go;
        marker_list.Add(a);
        if (x == 1)
            current_cell = current_cell.right;
        if (x == -1)
            current_cell = current_cell.left;
        if (y == 1)
            current_cell = current_cell.up;
        if (y == -1)
            current_cell = current_cell.down;
        transform.position = current_cell.transform.position;
        action tmp = new action();
        tmp.type = action_type.go;
        tmp.target = current_cell.gameObject;
        action_list.Add(tmp);
    }

    void tactic_shot(GameObject enemy, GameObject weapoon)
    {
        GameObject a = Instantiate<GameObject>(obj_marker);
        a.transform.position = transform.position;
        a.GetComponent<SpriteRenderer>().sprite = sprite_cell_fire;
        a.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        marker_list.Add(a);
        action tmp = new action();
        tmp.type = action_type.fire;
        tmp.target = enemy;
        tmp.weapoon = weapoon;
        action_list.Add(tmp);
    }

    void tactic_aim(GameObject enemy, GameObject weapoon)
    {
        GameObject a = Instantiate<GameObject>(obj_marker);
        a.transform.position = transform.position;
        a.GetComponent<SpriteRenderer>().sprite = sprite_cell_aim;
        marker_list.Add(a);
        action tmp = new action();
        tmp.type = action_type.aim;
        tmp.target = enemy;
        tmp.weapoon = weapoon;
        action_list.Add(tmp);
    }

    void tactic_undo()
    {
        action_list.RemoveAt(action_list.Count - 1);
        GameObject marker = marker_list[marker_list.Count - 1];
        transform.position = marker.transform.position;
        Destroy(marker);
        marker_list.RemoveAt(marker_list.Count - 1);
    }

    void start_war_phase()
    {
        transform.position = current_war_position;
        foreach (GameObject g in marker_list)
            Destroy(g);
        marker_list.Clear();

        if (action_list[0].type == action_type.go && action_list[1].type != action_type.go)
        {
            action tmp = new action();
            tmp.target = action_list[0].target;
            tmp.type = action_type._somersault;
            action_list[0] = tmp;
        }
        for (int i = 1; i < action_list.Count - 1; ++i)
        {
            if (action_list[i].type == action_type.go
                && action_list[i - 1].type != action_type.go
                && action_list[i + 1].type != action_type.go)
            {
                action tmp = new action();
                tmp.target = action_list[i].target;
                tmp.type = action_type._somersault;
                action_list[i] = tmp;
            }
        }
        if (action_list[action_list.Count - 1].type == action_type.go && action_list[action_list.Count - 2].type != action_type.go)
        {
            action tmp = new action();
            tmp.target = action_list[action_list.Count - 1].target;
            tmp.type = action_type._somersault;
            action_list[action_list.Count - 1] = tmp;
        }

        calc_stats_for_war_action();
        phase = phase_type.war;
        time_start_action = time;
    }

    void die()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        sc_event_controller.player_tactic_move -= tactic_move;
        sc_event_controller.end_war_phase -= start_tactic_phase;
        sc_event_controller.end_tactik_phase -= start_war_phase;
        sc_event_controller.player_tactic_shot -= tactic_shot;
        sc_event_controller.player_tactic_aim -= tactic_aim;
        sc_event_controller.player_tactic_undo -= tactic_undo;
    }

    /*void OnGUI()
    {
        Rect rect = new Rect(10f, 10f, 50f, 50f);
        GUI.Label(rect, dodge_chance.ToString());
    }*/
}
