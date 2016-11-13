using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sc_first_touch : MonoBehaviour {

    public GameObject Green_screen;
    public GameObject Roma;
    public Text What_Roma_say;
    public GameObject Canvas1;
	// Use this for initialization
	void Start () {
        Roma.SetActive(true);
        Green_screen.SetActive(true);
        Canvas1.SetActive(false);
        What_Roma_say.text = "Hello, Soldier! Time to roll thats shit! \n  We have time till 9 december. 10 december it's time to push game to Apple Store and Google Play! \n Plz, Guys! Lets do this!";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseDown()
    {
        Start_Game();
    }
    public void Start_Game()
    {
        Green_screen.SetActive(false);
        Roma.SetActive(false);
        Canvas1.SetActive(true);
    }
}
