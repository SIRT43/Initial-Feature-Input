#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace InitialSolution.Inputs
{
    [CustomEditor(typeof(EventTrigger))]
    public class EventTrigger_Inspector : Editor
    {
        public override void OnInspectorGUI()
        {
            EventTrigger target = this.target as EventTrigger;

            GUILayout.BeginVertical();

            target.pointerEnter = EditorGUILayout.Toggle("Pointer Enter (Exit)", target.pointerEnter);
            if (target.pointerEnter)
                target.onPointerEnter =
                    EditorGUILayout.ObjectField("输出到", target.onPointerEnter, typeof(PointerEventInput), false) as PointerEventInput;

            EditorGUILayout.Space();

            target.pointerDown = EditorGUILayout.Toggle("Pointer Down (Up)", target.pointerDown);
            if (target.pointerDown)
                target.onPointerDown =
                    EditorGUILayout.ObjectField("输出到", target.onPointerDown, typeof(PointerEventInput), false) as PointerEventInput;

            EditorGUILayout.Space();

            target.dragEvent = EditorGUILayout.Toggle("Drag Begin (End)", target.dragEvent);
            if (target.dragEvent)
                target.onBeginDrag =
                    EditorGUILayout.ObjectField("输出到", target.onBeginDrag, typeof(PointerEventInput), false) as PointerEventInput;

            GUILayout.EndVertical();
        }
    }
}
#endif
