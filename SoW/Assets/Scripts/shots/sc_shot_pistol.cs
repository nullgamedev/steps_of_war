using UnityEngine;
using System.Collections;

public class sc_shot_pistol : MonoBehaviour {

    public Material line_material;
    public float time_flash = 0.05f;
    public float time_fading = 0.05f;
    public float time_glow = 0.08f;
    public float track_width = 0.07f;

    float time = 0f;
    float alpha = 0f;
    LineRenderer line;
    float alpha_incr;
    float alpha_decr;

	// Use this for initialization
	void Start () {
        alpha_incr = 1f / time_flash;
        alpha_decr = 1f / time_fading;

        line = gameObject.AddComponent<LineRenderer>();
        line.material = line_material;
        Vector3[] tmp = new Vector3[2];
        tmp[0] = transform.position;
        tmp[1] = GetComponent<sc_any_shot>().target;
        line.SetPositions(tmp);
        line.useWorldSpace = true;
        line.SetWidth(track_width, track_width);
        line.SetColors(new Color(0, 0, 0, alpha), new Color(0, 0, 0, alpha));
	}
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;
        if (time < time_flash)
            alpha += alpha_incr * Time.deltaTime;
        if (time > time_flash + time_glow + time_fading)
            Destroy(gameObject);
        if (time > time_flash + time_glow)
            alpha -= alpha_decr;
        line.SetColors(new Color(0, 0, 0, alpha), new Color(0, 0, 0, alpha));
	}
}
