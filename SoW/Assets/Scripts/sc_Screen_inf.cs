using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sc_Screen_inf : MonoBehaviour {

	public Text Change_hit;
	public GameObject Heart1;
	public GameObject Heart2;
	public GameObject Heart3;
    public GameObject Roma;


	// Use this for initialization
	void Start () {
        Roma.SetActive(true);
	}
   
	// Update is called once per frame
	void Update () {
		if (sc_player.HpForYurii == 3) 
		{
			Heart3.SetActive (false);
		
		}
		if (sc_player.HpForYurii == 2) 
		{
			Heart2.SetActive (false);
		}
		if (sc_player.HpForYurii == 1) 
		{
			Heart1.SetActive (false);
		}
	
		//"change to hit -- " + sc_player.HitChangeForYurii +
		//Change_hit.text =  "Hp = " + sc_player.HpForYurii;
	}
}
