using Cinemachine;

public static class CameraSwitch
{
    public static bool shootingMode;

    public static void SwitchCam(CinemachineFreeLook tpV, CinemachineVirtualCamera attckV)
    {
        shootingMode = !shootingMode;

        if (shootingMode == true)
        {
            attckV.Priority = 10;
            tpV.Priority = 0;
        }
        else if (shootingMode == false)
        {
            attckV.Priority = 0;
            tpV.Priority = 10;
        }
    }
}
