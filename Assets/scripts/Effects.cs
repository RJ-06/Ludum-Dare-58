using System.Collections;
using Unity.Cinemachine;
using UnityEngine;


public class Effects : ScriptableObject
{ //this is a scriptable object - all functions should be public static, accessible from other scripts


    public static float hitStunTime;
    public static bool timeFrozen = false;

    public static IEnumerator HitStun() 
    {
        if (timeFrozen == true) yield return null;

        timeFrozen = false;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(hitStunTime);

        timeFrozen = true;
    }

    public static IEnumerator HitStun(float stunTime)
    {
        if (timeFrozen == true) yield return null;

        timeFrozen = false;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(stunTime);

        timeFrozen = true;
    }

    public static IEnumerator ScreenShake(CinemachineCamera cam, float intensity, float timer) 
    {
        CinemachineBasicMultiChannelPerlin mcp = cam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        if (mcp == null) yield return null;

        mcp.AmplitudeGain = intensity;      
        yield return new WaitForSeconds(timer);
        mcp.AmplitudeGain = 0;

    }

}
