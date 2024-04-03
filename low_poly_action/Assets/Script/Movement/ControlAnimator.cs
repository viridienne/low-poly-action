using UnityEngine;

public class ControlAnimator : MonoBehaviour
{
    
    private Animator animator;
    private static readonly int VelocityX = Animator.StringToHash("velocityX");
    private static readonly int VelocityZ = Animator.StringToHash("velocityZ");
   
    public void RegisterAnimator(Animator _animator)
    {
        animator = _animator;
    }
    public void UpdateAnimation(float _veloX, float _veloY)
    {
        animator.SetFloat(VelocityX, _veloX);
        animator.SetFloat(VelocityZ, _veloY);
    }
}
