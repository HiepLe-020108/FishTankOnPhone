using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIHomeSceneMenu : MonoBehaviour
{
        [Header("Buttons", order = 1)] 
        [SerializeField] private Button _shopOpenButton;
        [SerializeField] private Button _shopCloseButton;
        [SerializeField] private Button _loadLevel1Button;
        [SerializeField] private Button _loadLevel2Button;
        [SerializeField] private Button _openLevelSelectMenu;
        [SerializeField] private Button _closeLevelSelectMenu;
        [Header("GameObject", order = 2)] 
        [SerializeField] private GameObject shopMenu;
        [SerializeField] private GameObject levelSelectMenu;
        

        // Start is called before the first frame update
        void Start()
        {
           _shopOpenButton.onClick.AddListener(OpenShop);
           _shopCloseButton.onClick.AddListener(CloseShop);
           _loadLevel1Button.onClick.AddListener(LoadLevel1);
           _loadLevel2Button.onClick.AddListener(LoadLevel2);
           _openLevelSelectMenu.onClick.AddListener(OpenLevelSelectMenu);
           _closeLevelSelectMenu.onClick.AddListener(CloseLevelSelectMenu);
           
           //Set up UI for level
           shopMenu.SetActive(false);
           levelSelectMenu.SetActive(false);
        }
        
        private void OpenShop()
        {
             shopMenu.SetActive(true);
             Time.timeScale = 0f;
            
        }
        private void CloseShop()
        {
            shopMenu.SetActive(false);
            Time.timeScale = 1f;
            
        }

        private void OpenLevelSelectMenu()
        {
            levelSelectMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        private void CloseLevelSelectMenu()
        {
            levelSelectMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        private void LoadLevel1()
        {
            ScenesManager.Instance.LoadNewGame();
            Time.timeScale = 1f;
        }

        private void LoadLevel2()
        {
            ScenesManager.Instance.LoadLevel02Game();
            Time.timeScale = 1f;
        }
    
        
}

