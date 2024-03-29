using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
#endif

[Serializable]
public class UIAsset
{
    public string Key;
    public Object RootObject;
}
[CreateAssetMenu(fileName = "UIPrefabConfig", menuName = "Config/UI Prefab Config")]
public class UIPrefabConfig : ScriptableObject
{
    [TableList]
    public List<UIAsset> UIPrefabs;

    #if UNITY_EDITOR
    [Button]
    public void BuildAddressable()
    {
        List<AddressableAssetEntry> _entries = new List<AddressableAssetEntry>();

        foreach (var _assetObject in UIPrefabs)
        {
            var _guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(_assetObject.RootObject));
            
            AddressableAssetEntry _entry; 

            _entry = !HasEntry(_guid) ? CreateAssetEntry(_assetObject.RootObject, _assetObject.Key,"UIPrefab") : GetEntry(_guid);
            _entries.Add(_entry);
        }

        
        if (_entries.Count == 0)
        {
            Debug.LogError("No entry created!");
            return;
        }
        //write guids static class, this class can convert key to guid
        var _guids = _entries.Select(_x => _x != null && !_x.guid.IsNullOrWhitespace()).ToArray();
        var _keys = UIPrefabs.Select(_x => _x.Key).ToArray();
        var _content = "public static class UIPrefabGuid\n{\n";
        for (int i = 0; i < _guids.Length; i++)
        {
            _content += $"\tpublic const string { _keys[i] } = \"{ _guids[i] }\";\n";
        }
        _content += "}";
        System.IO.File.WriteAllText(Application.dataPath + "/Script/ConfigSO/UIPrefabGuid.cs", _content);
    }

    [Button]
    public void RenameEntries()
    {
        //rename all entries after keys
        foreach (var _assetObject in UIPrefabs)
        {
            var _guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(_assetObject.RootObject));
            var _entry = GetEntry(_guid);
            if (_entry != null)
            {
                _entry.address = _assetObject.Key;
                AddressableAssetSettingsDefaultObject.Settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, _entry, true);
            }
        }
    }
    
    public AddressableAssetEntry CreateAssetEntry(Object _source, string _address, string _groupName)
    {
        if (_source == null || string.IsNullOrEmpty(_groupName) || !AssetDatabase.Contains(_source))
            return null;
        var _settings = AddressableAssetSettingsDefaultObject.Settings;
        var _path = AssetDatabase.GetAssetPath(_source);
        var _guid = AssetDatabase.AssetPathToGUID(_path);
        var _group = !HasGroup(_groupName) ? CreateGroup(_groupName) : GetGroup(_groupName);
        
        var _entry = _settings.CreateOrMoveEntry(_guid, _group);
        _entry.address = _address;
        _settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, _entry, true);
        return _entry;
    }

    public bool HasGroup(string _groupName)
    {
        var _settings = AddressableAssetSettingsDefaultObject.Settings;
        return _settings.groups.Any(_x => _x.Name.Equals(_groupName));
    }
    
    public AddressableAssetGroup CreateGroup(string _groupName)
    {
        var _settings = AddressableAssetSettingsDefaultObject.Settings;
        var _group = _settings.CreateGroup(_groupName, false, false, true, _settings.DefaultGroup.Schemas);
        _settings.SetDirty(AddressableAssetSettings.ModificationEvent.GroupAdded, _group, true);
        return _group;
    }
    
    public AddressableAssetGroup GetGroup(string _groupName)
    {
        var _settings = AddressableAssetSettingsDefaultObject.Settings;
        return _settings.groups.FirstOrDefault(_x => _x.Name.Equals(_groupName));
    }
    
    public bool HasEntry(string _guid)
    {
        var _settings = AddressableAssetSettingsDefaultObject.Settings;
        return _settings.groups.Any(_x => _x.entries.Any(_y => _y.guid.Equals(_guid)));
    }
    public AddressableAssetEntry GetEntry(string _guid)
    {
        var _settings = AddressableAssetSettingsDefaultObject.Settings;
        return _settings.groups.SelectMany(_x => _x.entries).FirstOrDefault(_y => _y.guid.Equals(_guid));
    }
    #endif
}
