using UnityEngine;
using System.Collections.Generic;

public class PlayerPrefsManager : MonoBehaviour
{
    // Lưu trữ các khóa của PlayerPrefsManager
    private const string PLAYER_COUNT_KEY = "player_count";
    private const string UNLOCKED_PLAYER_TYPES_KEY = "unlocked_player_types";
    private const string DIAMOND_COUNT_KEY = "diamond_count";
    private const string COMPLETED_LEVELS_KEY = "completed_levels";

    // Thiết lập giá trị mặc định cho các khóa của PlayerPrefsManager
    private const int DEFAULT_PLAYER_COUNT = 0;
    private const string DEFAULT_UNLOCKED_PLAYER_TYPES = "";
    private const int DEFAULT_DIAMOND_COUNT = 0;
    private const string DEFAULT_COMPLETED_LEVELS = "";

    // Lưu trữ các giá trị của PlayerPrefsManager
    private static int playerCount;
    private static string unlockedPlayerTypes;
    private static int diamondCount;
    private static string completedLevels;

    // Hàm khởi tạo
    private void Start()
    {
        // Lấy các giá trị từ PlayerPrefs
        playerCount = PlayerPrefs.GetInt(PLAYER_COUNT_KEY, DEFAULT_PLAYER_COUNT);
        unlockedPlayerTypes = PlayerPrefs.GetString(UNLOCKED_PLAYER_TYPES_KEY, DEFAULT_UNLOCKED_PLAYER_TYPES);
        diamondCount = PlayerPrefs.GetInt(DIAMOND_COUNT_KEY, DEFAULT_DIAMOND_COUNT);
        completedLevels = PlayerPrefs.GetString(COMPLETED_LEVELS_KEY, DEFAULT_COMPLETED_LEVELS);
    }

    // Lưu trữ các giá trị vào PlayerPrefs
    public static void Save()
    {
        PlayerPrefs.SetInt(PLAYER_COUNT_KEY, playerCount);
        PlayerPrefs.SetString(UNLOCKED_PLAYER_TYPES_KEY, unlockedPlayerTypes);
        PlayerPrefs.SetInt(DIAMOND_COUNT_KEY, diamondCount);
        PlayerPrefs.SetString(COMPLETED_LEVELS_KEY, completedLevels);
    }

    // Các phương thức getter và setter cho các giá trị của PlayerPrefsManager
    public static int PlayerCount
    {
        get { return playerCount; }
        set { playerCount = value; }
    }
    public static void SetPlayerCount(int count)
    {
        PlayerPrefs.SetInt("PlayerCount", count);
    }

    public static int GetPlayerCount()
    {
        return PlayerPrefs.GetInt("PlayerCount", 0);
    }
    public static void SetFishList(List<FishTypeSO> fishList)
    {
        string json = JsonUtility.ToJson(new FishDataWrapper(fishList));
        PlayerPrefs.SetString("fishList", json);
    }

    public static List<FishTypeSO> GetFishList()
    {
        string json = PlayerPrefs.GetString("fishList", "");
        if (string.IsNullOrEmpty(json))
        {
            return new List<FishTypeSO>();
        }
        return JsonUtility.FromJson<FishDataWrapper>(json).fishList;
    }


    public static string UnlockedPlayerTypes
    {
        get { return unlockedPlayerTypes; }
        set { unlockedPlayerTypes = value; }
    }

    public static int DiamondCount
    {
        get { return diamondCount; }
        set { diamondCount = value; }
    }

    public static string CompletedLevels
    {
        get { return completedLevels; }
        set { completedLevels = value; }
    }
}
