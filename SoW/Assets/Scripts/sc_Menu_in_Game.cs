using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class sc_Menu_in_Game : MonoBehaviour {
    public GameObject Continue_Button;
    public GameObject Restart_Button;
    public GameObject Exit_Button;
    public GameObject Menu_Image;
    public GameObject Canvas1;
    public GameObject Player;
    public GameObject Enemy;
    public GameObject Text_Pause;
    private bool Check_mark = false;

    public void Click_Menu_Button()
    {
        if (Check_mark)
        {
            Continue_Button.SetActive(false);
            Restart_Button.SetActive(false);
            Exit_Button.SetActive(false);
            Menu_Image.SetActive(false);
            Text_Pause.SetActive(false);
            Canvas1.SetActive(true);
            Player.SetActive(true);
            Enemy.SetActive(true);
            Check_mark = false;
            Time.timeScale = 1f;
        }

        else
        {
            Continue_Button.SetActive(true);
            Restart_Button.SetActive(true);
            Exit_Button.SetActive(true);
            Menu_Image.SetActive(true);
            Text_Pause.SetActive(true);
            Canvas1.SetActive(false);
            Player.SetActive(false);
            Enemy.SetActive(false);
            Check_mark = true;
            Time.timeScale = 0f;
        }
    }

    public void Click_Continue_Button()
    {
        Continue_Button.SetActive(false);
        Restart_Button.SetActive(false);
        Exit_Button.SetActive(false);
        Menu_Image.SetActive(false);
        Text_Pause.SetActive(false);
        Canvas1.SetActive(true);
        Player.SetActive(true);
        Enemy.SetActive(true);
        Check_mark = false;
        Time.timeScale = 1f;

    }

    public void Click_Restart_Button()
    {
        Canvas1.SetActive(true);
        Player.SetActive(true);
        Enemy.SetActive(true);
        Application.LoadLevel(Application.loadedLevel);
    }
    public void Click_Exit_Button()
    {
        Application.LoadLevel("_Main_menu");
    }

   }

    
