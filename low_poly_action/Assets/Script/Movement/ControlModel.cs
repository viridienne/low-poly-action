using UnityEngine;

public class ControlModel : MonoBehaviour
{
    private Animator animator;
    public Animator Animator => animator;
    private GameObject lookAtTarget;
    public GameObject LookAtTarget => lookAtTarget;
    private void Awake()
    {
        animator = gameObject.GetOrAddComponent<Animator>();
        lookAtTarget = new GameObject();
        lookAtTarget.transform.SetParent(transform);
        lookAtTarget.transform.localPosition = new Vector3(0,1.4f,0);
    }

}
