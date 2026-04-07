using UnityEngine;

public class Attack : PlayerState
{
    private bool canCombo;
    private bool comboPressed;
    private int ComboIndex = 0;
    private float comboPressTime = -99f;
    private const float comboMemory = 0.5f;

    public Attack(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        canChanged = false;
        canCombo = false;
        comboPressed = false;
    }

    public override void Enter()
    {
        canChanged = false;
        canCombo = false;
        comboPressed = false;
        player.Rb.linearVelocity = Vector3.zero;
        player.animator.CrossFade(player.attackHashes[ComboIndex], 0.15f, 1);
        player.status.UseStamina(player.currentWeapon.status.attackStamina);
    }

    public override void Exit()
    {
        if (comboPressed && ComboIndex < 2)
            ComboIndex++;
        else
            ComboIndex = 0;
    }

    public override void LogicUpdate()
    {
        if (player.bufferinput == StateType.Attack)
        {
            comboPressTime = Time.time;
            comboPressed = true;
        }
        if (comboPressed && Time.time > comboPressTime + comboMemory)
            comboPressed = false;
    }

    public override void PhysicalUpdate()
    {
    }

    public override void OnAnimationFinished()
    {
        if (!comboPressed)
            canChanged = true;
    }

    public override void OncanCombo()
    {
        canCombo = true;
        if (comboPressed)
            player.ChangeState<Attack>();
    }
}
