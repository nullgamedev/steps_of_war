using UnityEngine;
using System.Collections;

public class sc_wall : MonoBehaviour {

    int hp = 2;

    public int hitpoints
    {
        get { return hp; }
        set
        {
            if (value <= 0)
            {
                hp = 0;
                Destroy(gameObject);
            }
            else
                hp = value;
        }
    }

    void Start()
    {
        float z = -1f + transform.localPosition.y * 0.25f;
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
}
