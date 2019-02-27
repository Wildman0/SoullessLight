using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class SpawnPath : MonoBehaviour
{

    static SpawnPath()
    {
        SceneView.onSceneGUIDelegate += OnSceneGooey;
    }

    static void OnSceneGooey(SceneView sceneView)
    {
        if(SimpleAI.spawnPath == true)
        {
            if(Event.current.type == EventType.MouseDown)
            {
                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hitInfo;

                if(Physics.Raycast(worldRay, out hitInfo, 10000))
                {
                    GameObject pathPrefab = Resources.Load("Path", typeof(GameObject)) as GameObject;

                    GameObject prefabInstance = PrefabUtility.InstantiatePrefab(pathPrefab) as GameObject;

                    prefabInstance.transform.position = hitInfo.point;

                    EditorUtility.SetDirty(prefabInstance);
                }

            }
        }
    }
    
}
