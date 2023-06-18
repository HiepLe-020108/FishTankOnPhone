using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 3;
    
    [SerializeField] private float fallingSpeed;
    [SerializeField] private MoneyManagerForLevel moneyManager; //a reference to the MoneyManager class, gameObject MoneyManager need to be a prefab
    [SerializeField] private string tagObjectThatCollectMoney;
    private GameObject bottom;
    [SerializeField] private GameObject thisObject;

    [FormerlySerializedAs("audioManager")] [SerializeField] private AudioSetting audioSetting;
    
    // Start is called before the first frame update
    void Start()
    {
        bottom = GameObject.Find("Bottom");
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManagerForLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > bottom.transform.position.y) //fall when the food is above the bottom of the tank
        {
            GetComponent<Transform>().position += new Vector3(0, -fallingSpeed, 0);
        }
        else
        {
            Destroy(thisObject, 5f);
        }
    }
    private void OnMouseDown()
    {
        PlaySoundCoinCollected();
        moneyManager.AddMoney(coinValue);
        Destroy(thisObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagObjectThatCollectMoney))
        {
            PlaySoundCoinCollected();
            moneyManager.AddMoney(coinValue);
            Destroy(thisObject);
        }
    }

    private void PlaySoundCoinCollected()
    {
        audioSetting.PlayOneShot("CoinCollected");
    }
}
