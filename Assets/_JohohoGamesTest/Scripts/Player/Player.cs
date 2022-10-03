using UnityEngine;

public class Player : MonoBehaviour, IInitializable
{
    [field: SerializeField] public PlayerData Data { get; private set; }
    [field: SerializeField] public Movement Movement { get; private set; }
    [field: SerializeField] public PlayerAnimator Animation { get; private set; }
    [field: SerializeField] public PlayerInventory Inventory { get; private set; }
    
    public void Init()
    {
        Movement.SetSpeed(Data.Speed);
        Movement.Init();
        Inventory.Init(this);
    }

}
