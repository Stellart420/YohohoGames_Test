using UnityEngine;

public class Movement : MonoBehaviour, IInitializable
{
    [field: SerializeField] public Rigidbody RB { get; private set; }

    public bool isRunning => MoveDir().magnitude >= 0.3f;

    private bool _isInit;
    private float _speed;
    private FloatingJoystick _joystick;
    private Player _player;

    private Vector3 MoveDir() => new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);

    public void Init()
    {
        _joystick = MainController.Instance.GUIController.GamePanel.Joystick;
        _player = MainController.Instance.PlayerController.Player;
        _isInit = true;
    }

    public void SetSpeed(float value)
    {
        _speed = value;
    }

    private void FixedUpdate()
    {
        if (!_isInit)
            return;

        if (isRunning)
        {
            var moveDir = MoveDir();
            var speed = _speed * Time.fixedDeltaTime;

            RB.velocity = moveDir * speed;
            Rotate(moveDir);
        }
        else
        {
            RB.velocity = Vector3.zero;
        }

        _player.Animation.SetSpeed(Mathf.Clamp01(RB.velocity.magnitude));
    }

    void Rotate(Vector3 direction)
    {
        var rotation = Quaternion.LookRotation(direction);
        RB.transform.rotation = rotation;
    }



}
