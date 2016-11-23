using UnityEngine;
using System.Collections;

public class sc_field_controller : MonoBehaviour {

    void Start()
    {
        sc_event_controller.end_tactik_phase += hide;
        sc_event_controller.end_war_phase += show;
    }

    void hide()
    {
        gameObject.SetActive(false);
    }

    void show()
    {
        gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        sc_event_controller.end_tactik_phase -= hide;
        sc_event_controller.end_war_phase -= show;
    }
}
