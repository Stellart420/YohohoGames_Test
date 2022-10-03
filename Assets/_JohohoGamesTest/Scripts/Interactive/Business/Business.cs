using UnityEngine;

public class Business : MonoBehaviour
{
    [field: SerializeField] public BusinessStateMachine SM { get; private set; }

    private void Start()
    {
        SM.Init(this);
    }
}
