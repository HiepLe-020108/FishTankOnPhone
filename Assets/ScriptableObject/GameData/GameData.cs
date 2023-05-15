using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Custom/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public List<GameObject> gameObjects;
    public int MoneyAmount;
    public int NumberOfLevel;
}