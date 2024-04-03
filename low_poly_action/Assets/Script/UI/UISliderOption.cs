using UnityEngine;
using UnityEngine.UI;


public class UISliderOption : MonoBehaviour
{
    [SerializeField] private SettingType settingType;
    [SerializeField] private Slider slider;

    private PlayerSettingConfig playerSettingConfig => ConfigCenter.Instance.GetPlayerSetting();
    private void Start()
    {
        if (slider)
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
            switch (settingType)
            {
                case SettingType.CameraSensitivity:
                    slider.value = playerSettingConfig.CameraSensitivityMultiplier;
                    break;
            }
        }
    }

    private void OnSliderValueChanged(float _arg0)
    {
        ConfigCenter.Instance.OnChange(settingType, _arg0);
    }

}
