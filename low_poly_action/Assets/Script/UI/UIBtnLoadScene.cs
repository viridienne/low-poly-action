using UnityEngine;

public class UIBtnLoadScene : MonoBehaviour
{
    [SerializeField] private SceneEnum sceneEnum;

    public void LoadScene()
    {
        switch (sceneEnum)
        {
            case SceneEnum.Launcher:
                GameManager.Instance.StartGame();
                break;
        }
    }
}
