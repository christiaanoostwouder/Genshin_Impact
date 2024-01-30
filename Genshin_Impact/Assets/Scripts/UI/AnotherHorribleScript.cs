using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AnotherHorribleScript : MonoBehaviour
{
    public int avgFPS;
    public TMP_Text tmpText;
    public float current;
    // Update is called once per frame

    private void OnEnable()
    {
        Application.targetFrameRate = 120;
    }
    void FixedUpdate()
    {
        current = Time.frameCount / Time.time;
        avgFPS = (int)current;
        tmpText.text = avgFPS.ToString() + "FPS";
    }
}
