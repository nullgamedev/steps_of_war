using UnityEngine;
using System.Collections;

public class sc_touch_object : MonoBehaviour {
    public GameObject enemy;
    public GameObject weapoon;
	// Use this for initialization
	void Start () {
       

    }

    // Update is called once per frame
    void Update() {
    }
        public void OnMouseDown ()
        {
        sc_event_controller.player_tactic_aim_event(enemy, weapoon);
        }
}
