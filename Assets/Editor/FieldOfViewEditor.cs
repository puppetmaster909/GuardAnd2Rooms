using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FieldOfView))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRaidus);
        Vector3 viewAngleA = fov.dirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.dirFromAngle(fov.viewAngle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRaidus);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRaidus);

        Handles.color = Color.red;
        foreach(Transform visiblePlayer in fov.visiblePlayers)
        {
            Handles.DrawLine(fov.transform.position, visiblePlayer.position);
        }
    }
}
