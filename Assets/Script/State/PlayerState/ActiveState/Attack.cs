
using UnityEngine;

public class Attack : PlayerState
{
    private bool canCombo;
    private bool comboPressed;
    private int ComboIndex = 0;
    private float comboPressTime = -99f;
    private const float comboMemory = 0.5f;
    private int enterFrame;

    public Attack(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        canChanged = false;
        canCombo = false;
        comboPressed = false;
    }

    public override void Enter()
    {
        enterFrame = Time.frameCount;
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
        if (comboPressed && canCombo)
        {
            Debug.Log($"combo! {ComboIndex}, canCombo: {canCombo}, comboPressed: {comboPressed}");
            player.ChangeState<Attack>();
        }
            
        if (comboPressed && Time.time > comboPressTime + comboMemory)
            comboPressed = false;
    }

    public override void PhysicalUpdate()
    {
                player.Rb.linearVelocity = new Vector3(
                player.MoveInput * player.status.currentspeed,
                player.Rb.linearVelocity.y,
                0f);
    }

    public override void OnAnimationFinished()
    {
        if (Time.frameCount <= enterFrame) return;
        canChanged = true;
    }

    public override void OncanCombo()
    {
        if (Time.frameCount <= enterFrame) return;
        canCombo = true;
        Debug.Log($"combo! {ComboIndex}, canCombo: {canCombo}, comboPressed: {comboPressed}");
    }
}
