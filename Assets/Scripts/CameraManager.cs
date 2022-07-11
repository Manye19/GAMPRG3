using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;
    public static CameraManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CameraManager>();
            }
            return _instance;
        }
    }
    public Camera mainCamera;
    [HideInInspector] public CameraFollow camFollow;
    [SerializeField] private Transform panLimitTransform;
    [HideInInspector] public Vector2 panLimit;
    public static CameraMovedEvent onCameraMovedEvent = new CameraMovedEvent();
    [SerializeField] Room defaultRoom;

    private void Awake()
    {
        _instance = this;
        mainCamera = mainCamera ? mainCamera : GameObject.Find("Main Camera").GetComponent<Camera>();
        camFollow = mainCamera.GetComponent<CameraFollow>();
    }
    private void OnEnable()
    {
        
        Debug.Log("Hello onEnable");
    }

    private void OnDisable()
    {
        
    }
    
    private void Start()
    {
        panLimit = Vector2Abs(transform.position - panLimitTransform.position);
        ResetCamera();
    }

    Vector2 Vector2Abs(Vector2 p_vector2)
    {
        Vector2 ans = new Vector2(Mathf.Abs(p_vector2.x), Mathf.Abs(p_vector2.y));
        return ans;
    }
    public void ResetCamera()
    {
        Debug.Log("Invoked: " + defaultRoom.transform.position + ", " + panLimit);
        onCameraMovedEvent.Invoke(defaultRoom.transform.position, panLimit);
    }
}
