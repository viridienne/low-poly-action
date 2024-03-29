using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBtnLoadScene : MonoBehaviour
{
    [SerializeField] private SceneEnum sceneEnum;

    public void LoadScene()
    {
        SceneLoader.Instance.LoadScene(sceneEnum);
    }
}
