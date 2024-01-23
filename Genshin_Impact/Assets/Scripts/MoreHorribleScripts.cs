using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreHorribleScripts : MonoBehaviour
{
    public GameObject bow;
    void Update()
    {
        bool idk = CameraSwitch.shootingMode == true ? true : false;
        bow.SetActive(idk);
    }
}
