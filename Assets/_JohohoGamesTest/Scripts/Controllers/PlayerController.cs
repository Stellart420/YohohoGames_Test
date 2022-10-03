using UnityEngine;

public class PlayerController : ControllerBase
{
    [field: SerializeField] public Player Player { get; private set; }

    [SerializeField] private bool _setCameraFollow = true;

    public override void Init(MainController main)
    {
        base.Init(main);
        Player.Init();

        if (_setCameraFollow)
        {
            _mainController.CameraController.SetFollow(Player.transform);
        }
    }
}
