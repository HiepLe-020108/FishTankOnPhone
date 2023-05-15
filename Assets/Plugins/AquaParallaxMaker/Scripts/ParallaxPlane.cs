using UnityEngine;

public class ParallaxPlane : MonoBehaviour {

    [SerializeField]
    private Transform mainContainer;
    private Transform additContainer;
    private Transform tempContainer;
    private float mapSizeX;
    private float halfMapSizeX;
    private Vector3 deltaPositionAC; // delta position of additional container
    private Vector3 mainContainerPosition;
    private bool canUpdate = false;

    /// <summary>
    /// Use this method for cretae infinite map
    /// </summary>
    /// <param name="mapSizeX"></param>
    /// <param name="camera"></param>
    public void CreateInfinitePlane(float mapSizeX, float cameraPosX)
    {
        this.mapSizeX = mapSizeX;
        halfMapSizeX = mapSizeX / 2f;
        deltaPositionAC = new Vector3(mapSizeX, 0, 0);  // Debug.Log("Duplicate main container: " + name);
        if(mainContainer)  additContainer = Instantiate(mainContainer, transform);
        canUpdate = (mainContainer && additContainer);
        UpdateInfinitePlane(cameraPosX);
    }
	
    /// <summary>
    /// Use this method for update infinite plane
    /// </summary>
    /// <param name="cameraPosX"></param>
	public void UpdateInfinitePlane (float cameraPosX)
    {
        if (!canUpdate) return;
        mainContainerPosition = mainContainer.position;
        //1 set addit container position
        if (cameraPosX > mainContainerPosition.x)
        {
            additContainer.position = mainContainerPosition + deltaPositionAC;
        }
        else
        {
            additContainer.position = mainContainerPosition - deltaPositionAC;
        }

        //2 swap containers mainContainer <-> additContainer
        if ((cameraPosX > mainContainerPosition.x + halfMapSizeX) || (cameraPosX < mainContainerPosition.x - halfMapSizeX))
        {
            tempContainer = mainContainer;
            mainContainer = additContainer;
            additContainer = tempContainer;
        }
	}
}
