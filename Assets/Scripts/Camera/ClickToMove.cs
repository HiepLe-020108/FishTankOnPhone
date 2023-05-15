using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

//this will attach to object that will contain childObject that will decine whether camera will move and zoom 
public class ClickToMove : MonoBehaviour
{
    [SerializeField] private Camera cameraReference;
    [SerializeField] private float cameraMinSize = 10f;
    [SerializeField] private float cameraMaxSize = 25f;
    [SerializeField] private float zoomStep = 0.5f;
    [SerializeField] private float zoomSpeed = 0.025f;
    Coroutine moveAndzoomCoroutine;

    private float cameraSaveSize;
    
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject childThatClickedToGetSmall;//this will decide the position of camera at small state
    [SerializeField] private GameObject childThatClickedToGetBig;//we need both of child object here to activate and deactivate them 
    //I need that name cause the camera go small and big base on what child object player click on
    
    [SerializeField] private float duration;
    private Vector3 bigCameraPosition;
    private Vector3 smallCameraPosition;
    
    private float timeSinceLastExecution = 0f;
    private bool canExecute = true;
    private int timeClick = 0;
    private void Start()
    {
        cameraSaveSize = cameraReference.orthographicSize;
        bigCameraPosition = cameraReference.transform.position;
        smallCameraPosition = childThatClickedToGetSmall.transform.position;
        //activate and deactivate childobejct so that player can only see what they should see
        childThatClickedToGetSmall.SetActive(true);
        childThatClickedToGetBig.SetActive(false);
    }


    void Update()
    {
        // Update the timer
        if (!canExecute)
        {
            timeSinceLastExecution += Time.deltaTime;
            if (timeSinceLastExecution >= duration)
            {
                canExecute = true;
                timeSinceLastExecution = 0f;
            }
        }
    }
    


    //this will be call in child of the object this script attach to
    //remember you need the name of the game object
    public void OnChildClicked(GameObject child)
    {
        //if player have already click on object then they cant click on it for a set time(duration)
        if (!canExecute)
        {
            return;
        }
        //Move the camera to position of fishtank, while that happen, also make camera size smaller
        //Deactivate child object(the one player just click on), we need to do that asap to make sure player cant click on it twice
        //Activate child object that when player click on it, camera will go back to it origin position and also zoom out
        if (child.name == "FishTankHere")
        { 
            StartCoroutine(DelayedActivateScripts(duration + .1f));
            StartCoroutine(DelaySetActiveChild(childThatClickedToGetSmall, false, duration));
            StartCoroutine(DelaySetActiveChild(childThatClickedToGetBig, true, duration));
            MoveAndZoom(smallCameraPosition,-zoomStep);
            canExecute = false;
        }
        //Move the camera to it origin position, while that happen, also make camera size bigger
        //Deactivate child object(the one player just click on), we need to do that asap to make sure player cant click on it twice
        //Activate child object that when player click on it, camera will get to fishtank position, also make camera smaller
        if (child.name == "NotFishTank")
        {
            StartCoroutine(DelayedDeactivateScripts(duration - duration));
            StartCoroutine(DelaySetActiveChild(childThatClickedToGetSmall, true, duration/2));
            StartCoroutine(DelaySetActiveChild(childThatClickedToGetBig, false, 0f));
            MoveAndZoom(bigCameraPosition,zoomStep);
            canExecute = false;
        }
    }
    
    //Zoom function
    void MoveAndZoom(Vector3 camPos,float _step)
    {
        float targetZoom = Mathf.Clamp(cameraReference.orthographicSize + _step,
            cameraMinSize, cameraMaxSize);
        Vector3 targetPosition = camPos;
        if (moveAndzoomCoroutine != null)
            StopCoroutine(moveAndzoomCoroutine);

        moveAndzoomCoroutine = StartCoroutine(MoveAndZoomCoroutine(targetPosition, targetZoom));
    }

    IEnumerator MoveAndZoomCoroutine(Vector3 targetPosition,float targetZoom)
    {
        Vector3 startPosition = cameraReference.transform.position;
        float startZoom = cameraReference.orthographicSize;
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            cameraReference.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            cameraReference.orthographicSize = Mathf.Lerp(startZoom, targetZoom, t);
            yield return null;
        }
        cameraReference.transform.position = targetPosition;
        cameraReference.orthographicSize = targetZoom;
    }

    IEnumerator DelaySetActiveChild(GameObject child, bool childState, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        child.SetActive(childState);
    }
    
    IEnumerator DelayedActivateScripts(float delayTime)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayTime);

        // Execute the function after the delay
        //activate all scripts of camera that we need
        LeanDragCamera cameraScripts = camera.GetComponent<LeanDragCamera>();
        cameraScripts.enabled = true;
        LeanPinchCamera cameraScripts2 = camera.GetComponent<LeanPinchCamera>();
        cameraScripts2.enabled = true;
        CameraBoundaries cameraScripts3 = camera.GetComponent<CameraBoundaries>();
        cameraScripts3.enabled = true;
    }
    
    
    IEnumerator DelayedDeactivateScripts(float delayTime)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayTime);

        // Execute the function after the delay
        //activate all scripts of camera that we need
        LeanDragCamera cameraScripts = camera.GetComponent<LeanDragCamera>();
        cameraScripts.enabled = false;
        LeanPinchCamera cameraScripts2 = camera.GetComponent<LeanPinchCamera>();
        cameraScripts2.enabled = false;
        CameraBoundaries cameraScripts3 = camera.GetComponent<CameraBoundaries>();
        cameraScripts3.enabled = false;
    }
        
}
