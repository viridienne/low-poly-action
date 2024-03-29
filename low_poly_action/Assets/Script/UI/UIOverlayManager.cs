using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Launcher,
    MainMenu,
    InGame,
    Pause,
    GameOver
}

public class UIOverlayManager : MonoBehaviour
{
    public static UIOverlayManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public void SetUI(UIState _UIState)
    {
        switch (_UIState)
        {
            case UIState.Launcher:
                UILoader.Instance.LoadUILauncher();
                break;
            default:
                UILoader.Instance.LoadUILauncher(false);
                break;
        }
    }
}
