﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sc_enemy : MonoBehaviour {

    public float duration_war_action = 0.8f;
    public GameObject popup_object;
    public sc_field_cell current_cell;
    public float dodge_chance = 0f;
    public float dodge_chance_stay = 0f;
    public float dodge_chance_run = 0.5f;
    public float dodge_chance_somersault = 1f;
    public float aim_effect = 0.5f;
    public GameObject my_weapoon;
    public int hitpoints
    {
        get { return hp; }
        set
        {
            hp = Mathf.Max(value, 0);
            if (hp == 0)
                die();
        }
    }

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
    List<action> action_list;
    Animator animator;

    enum action_type { go, aim, fire, somersault }
    struct action
    {
        public action_type type;
        public GameObject target;
        public GameObject weapoon;
    }

    public enum ai_type
    {
        go_left, go_right, go_up, go_down,
        somersault_left, somersault_right, somersault_up, somersault_down,
        aim,
        fire
    }
    public ai_type[] action_array;


    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        hp = base_hp;
        action_list = new List<action>();
        sc_event_controller.end_war_phase += start_tactic_phase;
        sc_event_controller.end_tactik_phase += start_war_phase;
        start_tactic_phase();
    }

    void calc_stats_for_war_action()
    {
        cur_action = action_list[0];
        //set animation
        switch (cur_action.type)
        {
            case action_type.go:
                {
                    animator.SetTrigger("walk");
                } break;
            case action_type.somersault:
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
            dodge_chance = dodge_chance_run;
        else if (cur_action.type == action_type.somersault)
            dodge_chance = dodge_chance_somersault;
        else dodge_chance = dodge_chance_stay;
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
                case action_type.somersault:
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
                            shot(w);
                        }
                    } break;
            }
        }
    }

    void shot(sc_weapoon w)
    {
        animator.SetTrigger("fire");
        sc_popup_text a = Instantiate<GameObject>(popup_object).GetComponent<sc_popup_text>();
        if (sc_player._.current_cell.up_wall == null)
        {
            float chance_hit = w.chance_hit * (1f + aim_count * aim_effect) - cur_action.target.GetComponent<sc_player>().dodge_chance;
            if (Random.Range(0f, 1f) < chance_hit)
            {
                a.text = "-" + w.damage.ToString();
                cur_action.target.GetComponent<sc_player>().hitpoints -= w.damage;
            }
            else
                a.text = "miss";
            //a.text = chance_hit.ToString() + " | " + a.text;
        }
        else
        {
            cur_action.target = sc_player._.current_cell.up_wall.gameObject;
            a.text = "-" + w.damage.ToString();
            cur_action.target.GetComponent<sc_wall>().hitpoints -= w.damage;
        }
        w.shot(transform.position, cur_action.target.transform.position);
        a.world_position = cur_action.target.transform.position;
        was_shot = true;
    }

    void start_tactic_phase()
    {
        animator.SetTrigger("stay");
        action_list.Clear();
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

        foreach (ai_type act in action_array)
        {
            switch (act)
            {
                //go
                case ai_type.go_down:
                    {
                        current_cell = current_cell.down;
                        action tmp = new action();
                        tmp.type = action_type.go;
                        tmp.target = current_cell.gameObject;
                        action_list.Add(tmp);
                    } break;
                case ai_type.go_up:
                    {
                        current_cell = current_cell.up;
                        action tmp = new action();
                        tmp.type = action_type.go;
                        tmp.target = current_cell.gameObject;
                        action_list.Add(tmp);
                    } break;
                case ai_type.go_left:
                    {
                        current_cell = current_cell.left;
                        action tmp = new action();
                        tmp.type = action_type.go;
                        tmp.target = current_cell.gameObject;
                        action_list.Add(tmp);
                    } break;
                case ai_type.go_right:
                    {
                        current_cell = current_cell.right;
                        action tmp = new action();
                        tmp.type = action_type.go;
                        tmp.target = current_cell.gameObject;
                        action_list.Add(tmp);
                    } break;
                //somersault
                case ai_type.somersault_down:
                    {
                        current_cell = current_cell.down;
                        action tmp = new action();
                        tmp.type = action_type.somersault;
                        tmp.target = current_cell.gameObject;
                        action_list.Add(tmp);
                    } break;
                case ai_type.somersault_up:
                    {
                        current_cell = current_cell.up;
                        action tmp = new action();
                        tmp.type = action_type.somersault;
                        tmp.target = current_cell.gameObject;
                        action_list.Add(tmp);
                    } break;
                case ai_type.somersault_left:
                    {
                        current_cell = current_cell.left;
                        action tmp = new action();
                        tmp.type = action_type.somersault;
                        tmp.target = current_cell.gameObject;
                        action_list.Add(tmp);
                    } break;
                case ai_type.somersault_right:
                    {
                        current_cell = current_cell.right;
                        action tmp = new action();
                        tmp.type = action_type.somersault;
                        tmp.target = current_cell.gameObject;
                        action_list.Add(tmp);
                    } break;
                //aim
                case ai_type.aim:
                    {
                        action tmp = new action();
                        tmp.type = action_type.aim;
                        tmp.target = sc_player._.gameObject;
                        tmp.weapoon = my_weapoon;
                        action_list.Add(tmp);
                    } break;
                //shot
                case ai_type.fire:
                    {
                        action tmp = new action();
                        tmp.type = action_type.fire;
                        tmp.target = sc_player._.gameObject;
                        tmp.weapoon = my_weapoon;
                        action_list.Add(tmp);
                    } break;
            }
        }
    }

    void start_war_phase()
    {
        transform.position = current_war_position;
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
        sc_event_controller.end_war_phase -= start_tactic_phase;
        sc_event_controller.end_tactik_phase -= start_war_phase;
    }
}
