using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class sc_Button_input : MonoBehaviour
{

    public GameObject enemy;
    public GameObject weapoon;
    public GameObject Move_Buttons;
    public GameObject Up_Button;
    public GameObject Down_Button;
    public GameObject Right_Button;
    public GameObject Left_Button;
    public GameObject Start_Button;
    public GameObject Move_Button;
    public GameObject Aim_Button;
    public GameObject Shot_Button;
    public GameObject Canvas1;
    public GameObject Canvas2;

    public Text Move_Text;
    private sc_field_cell Current_cell_check;
    public int Max_Moves = 20;
    private int Count_of_Moves = 0;

    bool Check_Button;

    public void Action_aim()
    {
        Move_Buttons.SetActive(false);
        sc_event_controller.player_tactic_aim_event(enemy, weapoon);
        Count_of_Moves++;
        Check_everything();
    }
    public void Action_fire()
    {
        Move_Buttons.SetActive(false);
        sc_event_controller.player_tactic_shot_event(enemy, weapoon);
        Count_of_Moves++;
        Check_everything();
    }

    public void Action_move()
    {
        Move_Buttons.SetActive(true);
    }

    public void Action_move_Up()
    {
        sc_event_controller.player_tactic_move_event(0, 1);
        Count_of_Moves++;
        Check_everything();
    }

    public void Action_move_Down()
    {
        sc_event_controller.player_tactic_move_event(0, -1);
        Count_of_Moves++;
        Check_everything();
    }

    public void Action_move_Right()
    {
        sc_event_controller.player_tactic_move_event(1, 0);
        Count_of_Moves++;
        Check_everything();
    }

    public void Action_move_Left()
    {
        sc_event_controller.player_tactic_move_event(-1, 0);
        Count_of_Moves++;
        Check_everything();
    }
    public void Action_LetsGo()
    {
        Count_of_Moves = 0;
        Canvas1.SetActive(false);
        sc_event_controller.end_tactik_phase_event();
        Move_Buttons.SetActive(false);
    }
    public void End_war_stage()
    {
        Check_everything();
        Canvas1.SetActive(true);
    }


    public void Start()
    {
        Check_everything();
        sc_event_controller.end_war_phase += End_war_stage;
    }

    public void Check_everything()
    {

        Move_Text.text = "Moves: " + Count_of_Moves + "/20";
        if (Count_of_Moves < Max_Moves)
        {

            Start_Button.GetComponent<Button>().interactable = false;
            Move_Button.GetComponent<Button>().interactable = true;
            Aim_Button.GetComponent<Button>().interactable = true;
            Shot_Button.GetComponent<Button>().interactable = true;

        }
        else
        {
            Start_Button.GetComponent<Button>().interactable = true;
            Move_Button.GetComponent<Button>().interactable = false;
            Aim_Button.GetComponent<Button>().interactable = false;
            Shot_Button.GetComponent<Button>().interactable = false;
            Canvas2.SetActive(false);
        }
        Current_cell_check = GameObject.Find("player").GetComponent<sc_player>().current_cell;
        if (Check_Button = Current_cell_check.CompareTag("No_way_to_Right"))
        {
            Right_Button.SetActive(false);
            Up_Button.SetActive(true);
            Down_Button.SetActive(true);
            Left_Button.SetActive(true);


        }
        if (Check_Button = Current_cell_check.CompareTag("No_way_to_RightUp"))
        {
            Right_Button.SetActive(false);
            Up_Button.SetActive(false);
            Down_Button.SetActive(true);
            Left_Button.SetActive(true);

        }
        if (Check_Button = Current_cell_check.CompareTag("No_way_to_RightDown"))
        {
            Right_Button.SetActive(false);
            Up_Button.SetActive(true);
            Down_Button.SetActive(false);
            Left_Button.SetActive(true);
        }
        if (Check_Button = Current_cell_check.CompareTag("No_way_to_Left"))
        {
            Right_Button.SetActive(true);
            Up_Button.SetActive(true);
            Down_Button.SetActive(true);
            Left_Button.SetActive(false);
        }
        if (Check_Button = Current_cell_check.CompareTag("No_way_to_LeftUp"))
        {
            Right_Button.SetActive(true);
            Up_Button.SetActive(false);
            Down_Button.SetActive(true);
            Left_Button.SetActive(false);
        }

        if (Check_Button = Current_cell_check.CompareTag("No_way_to_LeftDown"))
        {
            Right_Button.SetActive(true);
            Up_Button.SetActive(true);
            Down_Button.SetActive(false);
            Left_Button.SetActive(false);
        }
        if (Check_Button = Current_cell_check.CompareTag("No_way_to_Up"))
        {
            Right_Button.SetActive(true);
            Up_Button.SetActive(false);
            Down_Button.SetActive(true);
            Left_Button.SetActive(true);
        }
        if (Check_Button = Current_cell_check.CompareTag("No_way_to_Down"))
        {
            Right_Button.SetActive(true);
            Up_Button.SetActive(true);
            Down_Button.SetActive(false);
            Left_Button.SetActive(true);
        }
        if (Check_Button = Current_cell_check.CompareTag("Free_Fly"))
        {
            Right_Button.SetActive(true);
            Up_Button.SetActive(true);
            Down_Button.SetActive(true);
            Left_Button.SetActive(true);
        }

    }
}
