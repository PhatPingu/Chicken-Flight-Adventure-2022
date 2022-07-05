#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArrayCreatorTool))]
public class ArrayCreator_CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        ArrayCreatorTool arrayCreatorTool = (ArrayCreatorTool)target;

        if(GUILayout.Button("Update Values"))
        {
            arrayCreatorTool.UpdateObjectList();
            arrayCreatorTool.UpdateCoordinates();
        }

        if(GUILayout.Button("Set Global Variation"))
        {
            arrayCreatorTool.SetGlobalVariation();
        }

        if(GUILayout.Button("Create Spawner with Coords"))
        {
            arrayCreatorTool.CreateSpawner();
        }

        DrawDefaultInspector();
    }  
    
}
#endif