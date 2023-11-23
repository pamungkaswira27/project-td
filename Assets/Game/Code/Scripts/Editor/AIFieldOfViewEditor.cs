using UnityEngine;
using UnityEditor;

namespace ProjectTD
{
    [CustomEditor(typeof(AIFieldOfView))]
    public class AIFieldOfViewEditor : Editor
    {
        private void OnSceneGUI()
        {
            AIFieldOfView _aIFieldOfView = (AIFieldOfView)target;

            Handles.color = Color.cyan;
            Handles.DrawWireArc(_aIFieldOfView.transform.position, Vector3.up, Vector3.forward, 360f, _aIFieldOfView.Radius);

            Vector3 rightViewAngle = CalculateDirectionFromAngle(_aIFieldOfView.transform.eulerAngles.y, _aIFieldOfView.Angle / 2);
            Vector3 leftViewAngle = CalculateDirectionFromAngle(_aIFieldOfView.transform.eulerAngles.y, -_aIFieldOfView.Angle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(_aIFieldOfView.transform.position, _aIFieldOfView.transform.position + rightViewAngle * _aIFieldOfView.Radius);
            Handles.DrawLine(_aIFieldOfView.transform.position, _aIFieldOfView.transform.position + leftViewAngle * _aIFieldOfView.Radius);

            if (_aIFieldOfView.CanSeePlayer)
            {
                Handles.color = Color.green;
                Handles.DrawLine(_aIFieldOfView.transform.position, _aIFieldOfView.Target.position);
            }
        }

        private Vector3 CalculateDirectionFromAngle(float eulerY, float angleInDegree)
        {
            angleInDegree += eulerY;
            return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDegree * Mathf.Deg2Rad));
        }
    }
}