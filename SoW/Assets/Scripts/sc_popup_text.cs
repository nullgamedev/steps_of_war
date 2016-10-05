using UnityEngine;
using System.Collections;

public class sc_popup_text : MonoBehaviour {

    public string text;
    public Vector3 world_position;
    public Font font;
    public int font_size;
    public FontStyle font_style;
    public Color text_color;
    public float upper_speed;
    public float time_life;

    float rect_width = 75f;
    float rect_height = 30f;
    float alpha = 1f;
    float alpha_decr;
    float y_offset = 0f;
    float time = 0f;

    void Start()
    {
        alpha_decr = 1f / time_life;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > time_life)
            Destroy(gameObject);
        alpha -= alpha_decr * Time.deltaTime;
        y_offset += upper_speed * Time.deltaTime;
    }

    void OnGUI()
    {
        Vector3 screen_pos = Camera.main.WorldToScreenPoint(world_position);
        Rect rect = new Rect(screen_pos.x, Screen.height - screen_pos.y - rect_height - y_offset, rect_width, rect_height);

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.LowerLeft;
        style.font = font;
        style.fontSize = font_size;
        style.fontStyle = font_style;
        style.normal.textColor = new Color(text_color.r, text_color.g, text_color.b, alpha);
        //style.normal.textColor = text_color;

        GUI.Label(rect, text, style);
    }
}
