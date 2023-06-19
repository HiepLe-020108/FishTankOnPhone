using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldButtonToShow :MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
public float holdTime = 2f;
public TMP_Text textToShow;
public string textToDisplay = "Default text";

private bool isHolding = false;
private float holdStartTime;

private void Start()
{
    this.gameObject.SetActive(false);
}

public void OnPointerDown(PointerEventData eventData)
{
    isHolding = true;
    holdStartTime = Time.time;
    StartCoroutine(HoldCoroutine());
}

public void OnPointerUp(PointerEventData eventData)
{
    isHolding = false;
    textToShow.gameObject.SetActive(false);
}

private IEnumerator HoldCoroutine()
{
    while (isHolding && Time.time - holdStartTime < holdTime)
    {
        yield return null;
    }

    if (isHolding)
        {
            // Show text
            textToShow.text = textToDisplay;
            textToShow.gameObject.SetActive(true);
        }
}
}
