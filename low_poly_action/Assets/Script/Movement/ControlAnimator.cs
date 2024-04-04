using UnityEngine;

public class ControlAnimator : MonoBehaviour
{
    
    private Animator animator;
    private static readonly int VelocityX = Animator.StringToHash("velocityX");
    private static readonly int VelocityZ = Animator.StringToHash("velocityZ");
   private static readonly int Jump = Animator.StringToHash("jump");
   private static readonly int EndJump = Animator.StringToHash("endJump");
    public void RegisterAnimator(Animator _animator)
    {
        animator = _animator;
    }
    public void UpdateAnimation(float _veloX, float _veloY)
    {
        animator.SetFloat(VelocityX, _veloX);
        animator.SetFloat(VelocityZ, _veloY);
    }
    public void OnJump()
    {
        animator.SetTrigger(Jump);
    }
    public void OnEndJump()
    {
        animator.SetTrigger(EndJump);
    }
}
