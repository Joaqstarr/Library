using UnityEditor;
using UnityEngine;
using Level.MovingPlatform;

namespace Editor.Level.MovingPlatform
{
    [CustomEditor(typeof(MovingPlatformBehavior))]
    public class MovingPlatformEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            MovingPlatformBehavior platform = (MovingPlatformBehavior)target;

            // Hide Unity's default move/rotate/scale tools while in edit mode
            Tools.hidden = platform.editMode;

            if (platform.localWaypoints == null) return;

            for (int i = 0; i < platform.localWaypoints.Length; i++)
            {
                Vector3 worldPos = platform.transform.parent.TransformPoint(platform.localWaypoints[i]);

                if (platform.editMode)
                {
                    EditorGUI.BeginChangeCheck();
                    Vector3 newWorldPos = Handles.PositionHandle(worldPos, Quaternion.identity);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(platform, "Move Waypoint");
                        platform.localWaypoints[i] = platform.transform.parent.InverseTransformPoint(newWorldPos);
                        EditorUtility.SetDirty(platform);
                    }

                    if (i == 0)
                    {
                        platform.transform.position = newWorldPos;
                    }
                }

                if (i < platform.localWaypoints.Length - 1)
                {
                    Vector3 nextWorldPos = platform.transform.parent.TransformPoint(platform.localWaypoints[i + 1]);
                    Handles.color = Color.green;
                    Handles.DrawLine(worldPos, nextWorldPos);
                }
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MovingPlatformBehavior platform = (MovingPlatformBehavior)target;

            GUILayout.Space(10);

            // Toggle button
            if (GUILayout.Toggle(platform.editMode, platform.editMode ? "Finish Editing Waypoints" : "Edit Waypoints", "Button"))
            {
                platform.editMode = true;
            }
            else
            {
                platform.editMode = false;
            }

            // Ensure the scene view repaints immediately
            if (GUI.changed)
            {
                SceneView.RepaintAll();
            }
        }

        private void OnDisable()
        {
            // Restore Unity's default tools if we exit edit mode or deselect
            Tools.hidden = false;
        }
    }
}