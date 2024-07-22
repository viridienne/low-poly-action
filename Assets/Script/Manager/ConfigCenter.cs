using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    Idle,
    Walking,
    Running,
}

public enum MovementType
{
    Free,
    LockOn,
}
public class ConfigCenter : MonoBehaviour
{
    public static ConfigCenter Instance;
    [SerializeField] private PlayerSettingConfig playerSetting;
    [SerializeField] private ConfigMovementSO configMovement;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public PlayerSettingConfig GetPlayerSetting()
    {
        return playerSetting;
    }
    public ConfigMovementSO GetConfigMovement()
    {
        return configMovement;
    }

    public void OnChange(SettingType _settingType, float _value)
    {
        switch (_settingType)
        {
            case SettingType.CameraSensitivity:
                _value = Mathf.Clamp(_value, 0.1f, 1f);
                playerSetting.CameraSensitivityMultiplier = _value;
                PlayerCamera.Instance.UpdateSetting();
                break;
        }
    }
}
