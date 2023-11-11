using ProjectTD;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AIAlertSystem))]
public class AIAlertRadiusEditor : Editor
{
    private void OnSceneGUI()
    {
        Handles.color = Color.red;
        AIAlertSystem alertRad = (AIAlertSystem) target;
        Handles.DrawWireArc(alertRad.transform.position, alertRad.transform.up, alertRad.transform.forward, 360f, alertRad.AlertRadius);
    }
}
