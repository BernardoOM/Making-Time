using UnityEngine;
using UnityEditor;

public class CreatePrefab : MonoBehaviour
{
    [MenuItem("Project Tools/Make Prefab")]
    static void MakePrefab()
    {
		GameObject[] selectedObjects = Selection.gameObjects;

		if(selectedObjects.Length > 0)
		{
			foreach(GameObject GO in selectedObjects)
			{
				string localPath = "Assets/Prefabs/" + GO.name + ".prefab";

				if(!AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject)))
				{	CreateNew(GO, localPath);	}
				else if(EditorUtility.DisplayDialog("Replace Existing Prefab?",
													"You already prefabbed this. Want to change it?",
													"Sure", "Not Really"))
				{	CreateNew(GO, localPath);	}
			}
			AssetDatabase.Refresh();
		}
    }

	static void CreateNew(GameObject GO, string localPath)
	{
		Transform objParent = GO.transform.parent;
		Object prefab = PrefabUtility.CreateEmptyPrefab (localPath);
		PrefabUtility.ReplacePrefab (GO, prefab, ReplacePrefabOptions.ConnectToPrefab);
	}
}
