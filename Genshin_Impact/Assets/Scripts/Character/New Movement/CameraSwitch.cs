using Cinemachine;
using UnityEngine;

public static class CameraSwitch
{
    public static bool shootingMode;
    public static CinemachineFreeLook activeCamera;
    public static CinemachineFreeLook inactiveCamera;

    public static float SwitchCam(CinemachineFreeLook tpV, CinemachineFreeLook attckV, GameObject bow, Animator anim, float speed)
    {
        shootingMode = !shootingMode;

        if (shootingMode == true)
        {
            bow.SetActive(true);
            anim.SetBool("ShootingState", true);
            attckV.Priority = 10;
            tpV.Priority = 0;
            speed = 0.05f;
            inactiveCamera = tpV;
            activeCamera = attckV;
        }
        else if (shootingMode == false)
        {
            bow.SetActive(false);
            anim.SetBool("ShootingState", false);
            attckV.Priority = 0;
            tpV.Priority = 10;
            speed = 1f;
            activeCamera = tpV;
            inactiveCamera = attckV;
        }

        return speed;
    }
}
