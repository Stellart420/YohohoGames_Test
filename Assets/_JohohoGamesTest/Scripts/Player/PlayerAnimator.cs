using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IInitializable
{
    [SerializeField] private Animator _animator;

    private static int SPEED_HASH = Animator.StringToHash("Speed_f");

    public void SetSpeed(float value)
    {
        _animator.SetFloat(SPEED_HASH, value);
    }
}
