using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController instance = null;
    public static CameraController Get
    {
        get
        {
            if (instance == null) instance = new CameraController();
            return instance;
        }
    }
    public GameObject player;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
