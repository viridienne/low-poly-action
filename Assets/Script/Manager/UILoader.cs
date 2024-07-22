using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UILoader : MonoBehaviour
{
    [SerializeField] private Transform uiParent;
    public static UILoader Instance;
    
    private Dictionary<string,GameObject> uiDict = new Dictionary<string, GameObject>();
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        uiDict = new Dictionary<string, GameObject>();
    }
    
    public IEnumerator LoadUIAsync(string _guid)
    {
        var _operation = Addressables.LoadAssetAsync<GameObject>(_guid);
        yield return _operation;
        var _prefab = _operation.Result;
        var _gameObject = Instantiate(_prefab,uiParent);
        _gameObject.SetActive(true);
        AddUI(_guid, _gameObject);
    }

    public void AddUI(string _guid, GameObject _ui)
    {
        uiDict.TryAdd(_guid, _ui);
    }
    public bool HasUI(string _guid)
    {
        return uiDict.ContainsKey(_guid);
    }
    
    public void LoadUILauncher(bool _enable = true)
    {
        var _guid = UIPrefabGuid.UI_LAUNCHER;
        
        if (_enable)
        {
            if (!HasUI(_guid))
            {
                StartCoroutine(LoadUIAsync(_guid));
            }
            else
            {
                uiDict[_guid].SetActive(true);
            }
        }
        else
        {
            Debug.Log("Disable UI Launcher");
            if (HasUI(_guid))
            {
                uiDict[_guid].SetActive(false);
            }
        }
    }
}
