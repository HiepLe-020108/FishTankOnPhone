using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 1f;
    public float Speed { get { return speed; } }
    public static PlayerController Instance;

    public bool SaveData
    {
        get { return false; }
    }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0), Space.World);
	}

}
