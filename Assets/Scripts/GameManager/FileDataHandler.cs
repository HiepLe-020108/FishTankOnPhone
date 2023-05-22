using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;

    public FileDataHandler(string dirPath, string fileName, bool encryption)
    {
        this.dataDirPath = dirPath;
        this.dataFileName = fileName;
        this.useEncryption = encryption;

        if (!Directory.Exists(dataDirPath))
        {
            Directory.CreateDirectory(dataDirPath);
        }
    }

    public GameData Load()
    {
        string filePath = GetFilePath();
        if (File.Exists(filePath))
        {
            try
            {
                string data = File.ReadAllText(filePath);
                if (useEncryption)
                {
                    // Decrypt the data if encryption is enabled
                    data = DecryptData(data);
                }
                return JsonUtility.FromJson<GameData>(data);
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to load data: " + ex.Message);
            }
        }

        return null;
    }

    public void Save(GameData data)
    {
        string filePath = GetFilePath();
        string jsonData = JsonUtility.ToJson(data);

        if (useEncryption)
        {
            // Encrypt the data if encryption is enabled
            jsonData = EncryptData(jsonData);
        }

        try
        {
            File.WriteAllText(filePath, jsonData);
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to save data: " + ex.Message);
        }
    }

    private string GetFilePath()
    {
        return Path.Combine(dataDirPath, dataFileName);
    }

    private string EncryptData(string data)
    {
        // Implement your encryption logic here
        // You can use any encryption algorithm of your choice
        // Make sure to return the encrypted data as a string

        return data;
    }

    private string DecryptData(string data)
    {
        // Implement your decryption logic here
        // You can use any decryption algorithm that matches your encryption logic
        // Make sure to return the decrypted data as a string

        return data;
    }
}
