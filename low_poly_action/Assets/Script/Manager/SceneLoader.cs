using System;
using System.Collections;
using UnityEngine;

public enum SceneEnum
{
    Launcher,
    SampleScene
}


public static class SceneUtility
{
    public static string GetSceneName(SceneEnum _sceneEnum)
    {
        switch (_sceneEnum)
        {
            case SceneEnum.Launcher:
                return "Launcher";
            case SceneEnum.SampleScene:
                return "SampleScene";
            default:
                throw new ArgumentOutOfRangeException(nameof(_sceneEnum), _sceneEnum, null);
        }
    }
}

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private SceneEnum currentScene;
    
    
    private Coroutine loadSceneCoroutine;
    private Coroutine loadSceneAdditiveCoroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    
    public void LoadScene(SceneEnum _sceneEnum)
    {
       var _scene =  SceneUtility.GetSceneName(_sceneEnum);
        
        StartCoroutine(LoadSceneAsync(_scene));
    }

    public void LoadSceneAdditive(SceneEnum _sceneEnum)
    {
        var _scene =  SceneUtility.GetSceneName(_sceneEnum);

        StartCoroutine(LoadSceneAdditiveAsync(_scene));
    }

    private IEnumerator LoadSceneAdditiveAsync(string _sceneName)
    {
        var _operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        while (!_operation.isDone)
            yield return new WaitForEndOfFrame();
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation _operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        while (!_operation.isDone)
            yield return new WaitForEndOfFrame();
    }
}
