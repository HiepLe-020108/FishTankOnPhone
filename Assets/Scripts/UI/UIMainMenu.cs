using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;
    // Start is called before the first frame update
    void Start()
    {
        _newGameButton.onClick.AddListener(StartNewGame);
    }

    private void StartNewGame()
    {
        ScenesManager.Instance.LoadNewGame();
        //this below line is use for load scence with value pass in
        //ScenesManager.Instance.LoadScene(ScenesManager.Scene.Level01);
    }
}
