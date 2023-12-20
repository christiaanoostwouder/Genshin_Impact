using Cinemachine;

public static class CameraSwitch
{
    public static bool shootingMode;
    public static CinemachineFreeLook activeCamera;
    public static CinemachineFreeLook inactiveCamera;

    public static float SwitchCam(CinemachineFreeLook tpV, CinemachineFreeLook attckV, float speed)
    {
        shootingMode = !shootingMode;

        if (shootingMode == true)
        {
            attckV.Priority = 10;
            tpV.Priority = 0;
            speed = 0.2f;
            inactiveCamera = tpV;
            activeCamera = attckV;
        }
        else if (shootingMode == false)
        {
            attckV.Priority = 0;
            tpV.Priority = 10;
            speed = 1f;
            activeCamera = tpV;
            inactiveCamera = attckV;
        }

        return speed;
    }
}
