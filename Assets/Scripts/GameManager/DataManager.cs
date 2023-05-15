using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataManager", menuName = "Data Manager")]
public class DataManager : ScriptableObject
{
    public List<GameObject> gameObjects;
    public List<int> intValues;
    public List<string> stringValues;

    private static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<DataManager>("DataManager");
                if (instance == null)
                {
                    Debug.LogError("DataManager asset not found. Make sure to create a DataManager asset in the project.");
                }
            }
            return instance;
        }
    }

    public void Initialize()
    {
        gameObjects = new List<GameObject>();
        intValues = new List<int>();
        stringValues = new List<string>();
    }

    public void StoreTaggedObjects(params string[] tags)
    {
        foreach (string tag in tags)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
            gameObjects.AddRange(taggedObjects);
        }
    }


    public void RestoreTaggedObjects()
    {
        for (int i = gameObjects.Count - 1; i >= 0; i--)
        {
            if (gameObjects[i] == null)
            {
                gameObjects.RemoveAt(i);
            }
        }
    }
}
