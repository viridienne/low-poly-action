using UnityEngine;

public static class Utility 
{
    public static T GetOrAddComponent<T>(this GameObject _gameObject) where T : Component
    {
        T _component = _gameObject.GetComponent<T>();
        if (_component == null)
        {
            _component = _gameObject.AddComponent<T>();
        }
        return _component;
    }
}
