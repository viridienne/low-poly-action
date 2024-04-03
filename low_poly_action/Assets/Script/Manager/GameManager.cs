using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        UIOverlayManager.Instance.SetUI(UIState.Launcher);
    }
    
    public void StartGame()
    {
        SceneLoader.Instance.LoadScene(SceneEnum.SampleScene);
        UIOverlayManager.Instance.SetUI(UIState.InGame);
    }

    private void Update()
    {
        var _currentScene = SceneLoader.Instance.CurrentScene;
        switch (_currentScene)
        {
            case SceneEnum.Launcher:
                break;
            case SceneEnum.SampleScene:
                PlayerCamera.Instance.HandleCamera();
                break;
        }
    }
}
