using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//this script will decide if buttons for change to level is interactable or not 
//attach to a game object in HomeScene on sommething attach to _MANAGER_

public class LevelUnlockLoad : MonoBehaviour, IDataPersistence
{
   [SerializeField] private List<Button> levelButtons;   // Danh sách các nút UI đại diện cho các level
   [SerializeField] private List<int> levelValues;       // Danh sách giá trị int tương ứng với từng nút

   [SerializeField] private int levelUnlockINT;
    
    private void Start()
    {
        StartCoroutine(DelayedButtonInteraction());
    }

    public void LoadData(GameData data)
    {
        levelUnlockINT = data.completedLevels;
    }
    public void SaveData(GameData data) { }
    public void UpdateButtonInteraction()
    {
        int inputValue = levelUnlockINT;

        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (inputValue >= levelValues[i])
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }
    private IEnumerator DelayedButtonInteraction()
    {
        yield return new WaitForSeconds(0.01f);
        UpdateButtonInteraction();
    }
}
