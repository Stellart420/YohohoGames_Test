using UnityEngine;

public class GuiController : ControllerBase
{
    [field: SerializeField] public GamePanel GamePanel { get; private set; }

    public override void Init(MainController main)
    {
        base.Init(main);
        GamePanel.Init();
        GamePanel.Show();
    }
}
