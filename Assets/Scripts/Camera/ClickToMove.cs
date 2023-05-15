
using System.Collections;

using Lean.Touch;

using UnityEngine;


public class ClickToMove : MonoBehaviour
{
    [SerializeField] private Camera cameraReference;
    [SerializeField] private float cameraMinSize = 10f;
    [SerializeField] private float cameraMaxSize = 25f;
    [SerializeField] private float zoomStep = 0.5f;
    Coroutine moveAndzoomCoroutine;

    private float cameraSaveSize;

    [SerializeField] private GameObject cameraObject;
    [SerializeField] private GameObject childThatClickedToGetSmall;
    [SerializeField] private GameObject childThatClickedToGetBig;

    [SerializeField] private float duration;
    private Vector3 bigCameraPosition;
    private Vector3 smallCameraPosition;

    private float timeSinceLastExecution = 0f;
    private bool canExecute = true;

    private void Start()
    {
        cameraSaveSize = cameraReference.orthographicSize;
        bigCameraPosition = cameraReference.transform.position;
        smallCameraPosition = childThatClickedToGetSmall.transform.position;

        childThatClickedToGetSmall.SetActive(true);
        childThatClickedToGetBig.SetActive(false);
    }

    void Update()
    {
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

    public void OnChildClicked(GameObject child)
    {
        if (!canExecute)
        {
            return;
        }

        if (child.name == "FishTankHere")
        {
            StartCoroutine(DelayedActivateScripts(duration + .1f));
            StartCoroutine(DelaySetActiveChild(childThatClickedToGetSmall, false, duration));
            StartCoroutine(DelaySetActiveChild(childThatClickedToGetBig, true, duration));
            MoveAndZoom(smallCameraPosition, -zoomStep);
            canExecute = false;
        }

        if (child.name == "NotFishTank")
        {
            StartCoroutine(DelayedDeactivateScripts(duration - duration));
            StartCoroutine(DelaySetActiveChild(childThatClickedToGetSmall, true, duration / 2));
            StartCoroutine(DelaySetActiveChild(childThatClickedToGetBig, false, 0f));
            MoveAndZoom(bigCameraPosition, zoomStep);
            canExecute = false;
        }
    }

    void MoveAndZoom(Vector3 camPos, float _step)
    {
        float targetZoom = Mathf.Clamp(cameraReference.orthographicSize + _step, cameraMinSize, cameraMaxSize);
        Vector3 targetPosition = new Vector3(camPos.x, camPos.y, cameraObject.transform.position.z);
        if (moveAndzoomCoroutine != null)
            StopCoroutine(moveAndzoomCoroutine);

        moveAndzoomCoroutine = StartCoroutine(MoveAndZoomCoroutine(targetPosition, targetZoom));
    }

    IEnumerator MoveAndZoomCoroutine(Vector3 targetPosition, float targetZoom)
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
        yield return new WaitForSeconds(delayTime);

        LeanDragCamera cameraScripts = cameraObject.GetComponent<LeanDragCamera>();
        cameraScripts.enabled = true;
        LeanPinchCamera cameraScripts2 = cameraObject.GetComponent<LeanPinchCamera>();
        cameraScripts2.enabled = true;
        CameraBoundaries cameraScripts3 = cameraObject.GetComponent<CameraBoundaries>();
        cameraScripts3.enabled = true;
    }

    IEnumerator DelayedDeactivateScripts(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        LeanDragCamera cameraScripts = cameraObject.GetComponent<LeanDragCamera>();
        cameraScripts.enabled = false;
        LeanPinchCamera cameraScripts2 = cameraObject.GetComponent<LeanPinchCamera>();
        cameraScripts2.enabled = false;
        CameraBoundaries cameraScripts3 = cameraObject.GetComponent<CameraBoundaries>();
        cameraScripts3.enabled = false;
    }
}

   
