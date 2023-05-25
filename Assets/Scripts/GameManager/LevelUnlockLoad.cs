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

   private int levelUnlockINT;
    
    private void Start()
    {
        // Kiểm tra giá trị từ script khác và cập nhật trạng thái tương tác của các nút UI
        UpdateButtonInteraction();
    }

    public void LoadData(GameData data)
    {
        levelUnlockINT = data.completedLevels;
    }

    public void SaveData(GameData data) { }

    public void UpdateButtonInteraction()
    {
        int inputValue = levelUnlockINT; // Lấy giá trị int từ script khác

        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (inputValue >= levelValues[i])
            {
                // Nếu giá trị int lớn hơn hoặc bằng giá trị int của nút, cho phép tương tác
                levelButtons[i].interactable = true;
            }
            else
            {
                // Ngược lại, không cho phép tương tác
                levelButtons[i].interactable = false;
            }
        }
    }
}
