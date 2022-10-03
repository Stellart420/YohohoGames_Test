using UnityEngine;

public class WorldController : ControllerBase
{
    [field: SerializeField] public Transform ItemsParent { get; private set; }
    [field: SerializeField] public Transform UnitsParent { get; private set; }
}
