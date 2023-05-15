using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;

//attach to companion manager
//companion manager will be parent of all companion, whose are all not active
public class ChooseCompanion : MonoBehaviour
{
    [SerializeField] private float numberOfCompanionCanHave;
    private int activeCompanion = 0;
    private bool isThereActiveTextInformation = false;//this is to check if there are any textInformation open 
    
    [FormerlySerializedAs("_buttons")] 
    //this is list of buttons that will appear in preplay UI
    [SerializeField] private List<SetUpChooseCompanionButton> buttonsOnList;
    //this is list of all object that hold text object that show info of companions
    [SerializeField] private List<GameObject> textInformationOnList;
    //this is the list of companion that attach to companion manager
    [SerializeField] private List<GameObject> companionList = new List<GameObject>();
    //this is list of images that will show player which companions will appear on scene after they hit play button
    [SerializeField] private List<GameObject> imageList = new List<GameObject>();

    private bool stateOfCompanion;

    private bool canAdd;
    // Start is called before the first frame update
    void Start()
    {
        stateOfCompanion = false;
        for (int i = 0; i < buttonsOnList.Count; i++)//count over all buttons and deactivate all object
            //also deactivate all text of information for fish
        //then set value of object from list of Companion to buttons UI list
        //number of object from all list need to be equal
        {
            var index = i;
            companionList[index].SetActive(stateOfCompanion);
            textInformationOnList[index].SetActive(stateOfCompanion);
            imageList[index].SetActive(stateOfCompanion);
            buttonsOnList[index].SetupButton(companionList[index],() => AddCompanionToScene(index));
        }
    }

    private void Update()
    {
    }
    //this function use to let player choose a set number of companion
    
    private void AddCompanionToScene(int index)
    {

        if (activeCompanion < numberOfCompanionCanHave) canAdd = true;
        if (activeCompanion >= numberOfCompanionCanHave) canAdd = false;

        if (companionList[index].activeSelf == true)
        {
            companionList[index].SetActive(false);
            imageList[index].SetActive(false);
            activeCompanion--;
        }
        
        if (companionList[index].activeSelf == false && canAdd)
        {
            companionList[index].SetActive(true);
            activeCompanion++;
            imageList[index].SetActive(true);
            for (int i = 0; i < textInformationOnList.Count; i++)
            {
                textInformationOnList[i].SetActive(false);
            }
            textInformationOnList[index].SetActive(true);
        }
    }
}
