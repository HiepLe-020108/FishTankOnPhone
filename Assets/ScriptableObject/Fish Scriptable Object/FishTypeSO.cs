using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Fish", order = 2)]
public class FishTypeSO : ScriptableObject
{
   [Header("Button section", order = 1)]
   public string FishName;
   public float FishPrice;
   public GameObject TheFish;//this is the gameObject that will spawn when player press buy button
   public Sprite FishImage;
   
   [Header("Fish Information", order = 2)]
   public float FishSpeed;
   public float FishStamina;//how long can the fish stay not eat
   public int FishType;
   public float timeNeedToGrow = 600f ;//this value is set in seconds
   public string foodTag;
   public string fishInfo;
   
   [Header("Spawn Object section", order = 3)]
   public GameObject objectToSpawn;
   public float timeNeedToSpawn;

   [Header("Fish information in text type", order = 4)]
   public string FishInformation;

}
[System.Serializable]
public class FishDataWrapper
{
   public List<FishTypeSO> fishList;

   public FishDataWrapper(List<FishTypeSO> fishList)
   {
      this.fishList = fishList;
   }
}