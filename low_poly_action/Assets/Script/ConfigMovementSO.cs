using UnityEngine;

[CreateAssetMenu(fileName = "ConfigMovementSO", menuName = "Config/Config Movement")]
public class ConfigMovementSO : ScriptableObject
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float rotationSpeed;
}
