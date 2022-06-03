#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArrayCreatorTool))]
public class ArrayCreator_CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ArrayCreatorTool arrayCreatorTool = (ArrayCreatorTool)target;
        if(GUILayout.Button("Create Spawner with Coords"))
        {
            arrayCreatorTool.CreateSpawner();
        }

        if(GUILayout.Button("Update Values"))
        {
            arrayCreatorTool.UpdateCoordinates();
            arrayCreatorTool.UpdateObjectList();
        }
    }  
    
}
#endif