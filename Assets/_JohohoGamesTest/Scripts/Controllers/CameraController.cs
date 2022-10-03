using Cinemachine;
using UnityEngine;

public class CameraController : ControllerBase
{
    [SerializeField] private CinemachineVirtualCamera _currentCamera;

    public CinemachineVirtualCamera CurrentCamera => _currentCamera;

    public override void Init(MainController main)
    {
        base.Init(main);
    }

    public void SetFollow(Transform target)
    {
        _currentCamera.Follow = target;
    }
}

public class ControllerBase : MonoBehaviour
{
    internal MainController _mainController;

    public virtual void Init(MainController main)
    {
        _mainController = main;
    }
}