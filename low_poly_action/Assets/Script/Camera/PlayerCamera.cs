using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLook;
    public static PlayerCamera Instance;
    
    private Camera camera;
    public Camera Camera => camera;

    private Transform tf;
    public Transform Tf => tf;
    
    [Header("Settings")] 
    // [SerializeField] private Transform _camPivot;
    private Vector3 camVeclocity;
    private float camSmooth = 1;

    [SerializeField] private float horizontalAngle;
    [SerializeField] private float verticalAngle;
    
    //MIN & MAX VALUE FOR VERTICAL
    [SerializeField] private float minValue = -30; 
    [SerializeField] private float maxValue = 60;

    //CAMERA COLLISION VALUE
    [SerializeField] private float currentCamPosZ;
    [SerializeField] private float newCamPosZ;
    [SerializeField] private float collisionOffset = 0.2f;
    [SerializeField] private LayerMask collisionLayers;
    
    private float horizontalRotationSpeed = 200;
    private float verticalRotationSpeed = 200;
    private Vector3 camPos;
    
    private PlayerSettingConfig playerSettingConfig => ConfigCenter.Instance.GetPlayerSetting();
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        tf = transform;
        camera = Camera.main;
        currentCamPosZ = camera.transform.localPosition.z;
        UpdateSetting();
    }
    
    public void HandleCamera()
    {
        // HandleRotation();
        HandleCollision();
    }

    public void RegisterFollow(Transform _target)
    {
        freeLook.Follow = _target;
    }
    public void RegisterLookAt(Transform _target)
    {
        freeLook.LookAt = _target;
    }

    public void UpdateSetting()
    {
        freeLook.m_XAxis.m_MaxSpeed = playerSettingConfig.CameraHorizontalSpeed * playerSettingConfig.CameraSensitivityMultiplier;
        freeLook.m_YAxis.m_MaxSpeed = playerSettingConfig.CameraVerticalSpeed * playerSettingConfig.CameraSensitivityMultiplier;
    }
    // private void HandleRotation()
    // {
    //     horizontalAngle += (ReceiveInput.Instance.LookInputValue.x * playerSettingConfig.CameraHorizontalSpeed * playerSettingConfig.CameraSensitivityMultiplier) * Time.deltaTime;
    //     verticalAngle -= (ReceiveInput.Instance.LookInputValue.y * playerSettingConfig.CameraVerticalSpeed * playerSettingConfig.CameraSensitivityMultiplier) * Time.deltaTime;
    //     verticalAngle = Mathf.Clamp(verticalAngle, minValue, maxValue);
    //
    //     // // Y <-> X BECAUSE OF ROTATION
    //     // var rotationValue = Vector3.zero;
    //     // rotationValue.y = horizontalAngle;
    //     // var camRotation = Quaternion.Euler(rotationValue);
    //     // transform.rotation = camRotation;
    //     //
    //     // rotationValue = Vector3.zero;
    //     // rotationValue.x = verticalAngle;
    //     // camRotation = Quaternion.Euler(rotationValue);
    //     // _camPivot.localRotation = camRotation;
    //     freeLook.m_XAxis.Value = horizontalAngle;
    //     freeLook.m_YAxis.Value = verticalAngle;
    // }

    private void HandleCollision()
    {
        newCamPosZ = currentCamPosZ;
        var dir = camera.transform.position - transform.position;
        dir.Normalize();
        if (Physics.SphereCast(transform.position, collisionOffset, dir, out var hit, Mathf.Abs(newCamPosZ),collisionLayers))
        {
            var distance = Vector3.Distance(transform.position, hit.point);
            newCamPosZ = collisionOffset - distance;
        }
        if (Mathf.Abs(newCamPosZ) < collisionOffset)
        {
            newCamPosZ = -collisionOffset;
        }
        camPos.z = Mathf.Lerp(camera.transform.localPosition.z, newCamPosZ, 0.1f);
        camera.transform.localPosition = camPos;
    }
    
}
