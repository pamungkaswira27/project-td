using ProjectTD;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AIPatrol))]
public class AIEnemyPatrollingRadius : Editor
{
    private void OnSceneGUI()
    {
        Handles.color = Color.green;
        AIPatrol radiusSpots = (AIPatrol) target;

        Handles.DrawWireArc(radiusSpots.transform.position, radiusSpots.transform.up, radiusSpots.transform.forward, 360f, radiusSpots.RadiusPatrol);
    }
}
