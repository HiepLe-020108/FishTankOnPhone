
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "FishFood", order = 1)]
public class FoodData : ScriptableObject
{
    public string nameOfFood;
    public float priceOfFood = 5f;
    public float nutritionValue = 5f;
    public GameObject fishFood;
    public float fallingSpeed;
}
