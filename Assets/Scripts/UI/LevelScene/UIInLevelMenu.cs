using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UIInLevelMenu : MonoBehaviour
{
    [Header("Button", order = 1)] 
    [SerializeField] private List<Button> _mainMenuButtons = new List<Button>();
    [SerializeField] private List<Button> _pauseButtons = new List<Button>();
    [SerializeField] private List<Button> _resetButtons = new List<Button>();
    [SerializeField] private List<Button> _toHomeButtons = new List<Button>();
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _soundMenuButton;
    
    [Header("GameObject", order = 2)] 
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject inLevelUI;
    [SerializeField] private GameObject PrePlayMenu;
    [SerializeField] private GameObject doneMenu;
    [SerializeField] private GameObject SoundMenu;
    
    [Header("Reference", order = 3)] 
    [SerializeField] private SpawnEnemy spawnEnemyInEnemyManager;//this is here to start spawn enemy countdown after we close preplay menu
    [FormerlySerializedAs("_audioManager")] [SerializeField] private AudioSetting audioSetting;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _pauseButtons.Count; i++)
        {
            _pauseButtons[i].onClick.AddListener(Pause);
        }
        for (int i = 0; i < _resetButtons.Count; i++)
        {
            _resetButtons[i].onClick.AddListener(Restart);
        }
        for (int i = 0; i < _toHomeButtons.Count; i++)
        {
            _toHomeButtons[i].onClick.AddListener(ToHomeScene);
        }

        _resumeButton.onClick.AddListener(Resume);
        _playButton.onClick.AddListener(ChangeFromPrePlayMenuToInLevelUI);
        _nextLevelButton.onClick.AddListener(LoadNextLevel);
        _soundMenuButton.onClick.AddListener(ToSoundMenu);
        
        
        //Set up UI for level
        pauseMenu.SetActive(false);
        inLevelUI.SetActive(false);
        doneMenu.SetActive(false);
        SoundMenu.SetActive(false);
        PrePlayMenu.SetActive(true);
        
    }

    private void LoadNextLevel()
    {
        PlayButtonClickSoundEffect();
        ScenesManager.Instance.LoadNextScene();
        Time.timeScale = 1f;
    }
    private void ToHomeScene()
    {   
        PlayButtonClickSoundEffect();
        ScenesManager.Instance.LoadHomeScene();
        Time.timeScale = 1f;
    }
    private void Pause()
    {
        PlayButtonClickSoundEffect();
         pauseMenu.SetActive(true);
         inLevelUI.SetActive(false);
         SoundMenu.SetActive(false);
         Time.timeScale = 0f;
    }
    
    private void Resume()
    {
        PlayButtonClickSoundEffect();
        pauseMenu.SetActive(false);
        inLevelUI.SetActive(true);
        Time.timeScale = 1f;
    }

    private void ChangeFromPrePlayMenuToInLevelUI()
    {
        PlayButtonClickSoundEffect();
        PrePlayMenu.SetActive(false);
        Time.timeScale = 1f;
        inLevelUI.SetActive(true);
        spawnEnemyInEnemyManager.StartSpawnFunction();
    }
    private void ToSoundMenu()
    {
        PlayButtonClickSoundEffect();
        pauseMenu.SetActive(false);
        SoundMenu.SetActive(true);
    }

    private void Restart()
    {
        PlayButtonClickSoundEffect();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    void PlayButtonClickSoundEffect()
    {
        audioSetting.PlayOneShot("Click");
    }

}
