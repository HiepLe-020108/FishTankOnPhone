using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//attach to main camera


public class SpawnFoodOnTouch : MonoBehaviour
{
    [SerializeField] private FoodData foodData;
    
    LayerMask layerMask = 1 << 6; //ignores all layer except for layer 6
    
    //need 4 gameobjects in the hierarchy
    private GameObject top;
    private GameObject bottom;
    private GameObject right;
    private GameObject left;

    private MoneyManager moneyManager; 
    [SerializeField] private bool buyFood = true;

    [SerializeField] private TMP_Text foodPriceText;

    [SerializeField] private Toggle toggleBuyFood;
    
    // Start is called before the first frame update
    void Start()
    {
        top = GameObject.Find("Top"); //NAME
        bottom = GameObject.Find("Bottom");
        right = GameObject.Find("Right");
        left = GameObject.Find("Left");
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        buyFood = toggleBuyFood.isOn;
        if (buyFood)
        {
            if (Input.GetMouseButtonDown(0) && !CheckIfMouseTouch() && moneyManager.TotalMoney >= foodData.priceOfFood )
            {
                SpawnFood();
            }
        }
        
        // print our price of the food
        foodPriceText.SetText(foodData.priceOfFood.ToString());
    }
    
    public void SpawnFood()
    {
        var mousePoint = GetMousePos();
        if (mousePoint.x > left.transform.position.x && mousePoint.x < right.transform.position.x &&
            mousePoint.y > bottom.transform.position.y && mousePoint.y < top.transform.position.y)//check if player spawn food inside the tank
        {
            GameObject clone = Instantiate(foodData.fishFood, new Vector3(mousePoint.x, mousePoint.y, 0), Quaternion.identity);
            moneyManager.SubMoney(foodData.priceOfFood);
        }
    }

    public bool CheckIfMouseTouch()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 9990f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2 GetMousePos()
    {
        var pos = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, -Camera.main.transform.position.z));
    }

    public void CheckIfPlayerWantFood()
    {
        buyFood = !buyFood;
    }
}
