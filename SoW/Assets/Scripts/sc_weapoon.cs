using UnityEngine;
using System.Collections;

public class sc_weapoon : MonoBehaviour
{
    public GameObject shot_object;
    public int damage;
    public float chance_hit;
    public float time_shooting; //relative (0.5 means shoot at middle action)

    public void shot(Vector3 from, Vector3 target)
    {
        GameObject a = Instantiate<GameObject>(shot_object);
        a.transform.position = from;
        a.GetComponent<sc_any_shot>().target = target;
    }
}
