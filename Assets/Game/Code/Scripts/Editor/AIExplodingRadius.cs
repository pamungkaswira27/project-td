using ProjectTD;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AttackStateEnemySelfExploding))]
public class AIExplodingRadius : Editor
{
    private void OnSceneGUI()
    {
        Handles.color = Color.blue;
        AttackStateEnemySelfExploding rad = (AttackStateEnemySelfExploding)target;

        Handles.DrawWireArc(rad.transform.position, rad.transform.up, rad.transform.forward, 360f, rad.GetRadiusExplode);
    }
}
