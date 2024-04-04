using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLook;
    public static PlayerCamera Instance;
    private Transform tf;
    public Transform Tf => tf;
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
        UpdateSetting();
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
}
