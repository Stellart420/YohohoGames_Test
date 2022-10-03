using UnityEngine;

public class MainController : MonoBehaviour
{
    public static MainController Instance { get; private set; }

    [field: SerializeField] public CameraController CameraController { get; private set; }
    [field: SerializeField] public PlayerController PlayerController { get; private set; }
    [field: SerializeField] public GuiController GUIController { get; private set; }
    [field: SerializeField] public CurrencyController CurrencyController { get; private set; }
    [field: SerializeField] public ItemsController ItemsController { get; private set; }
    [field: SerializeField] public WorldController WorldController { get; private set; }

    private void Awake()
    {
        Instance = this;
        GUIController.Init(this);
        CameraController.Init(this);
        PlayerController.Init(this);
        CurrencyController.Init(this);
        ItemsController.Init(this);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
