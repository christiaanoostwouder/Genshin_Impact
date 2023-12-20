using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorribleScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = CameraSwitch.shootingMode == true ? new Color(0f, 0f, 0f, 100f) : new Color(0f, 0f, 0f, 0f);
    }    
}
