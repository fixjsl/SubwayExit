using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Execution : PlayerState
{
    private MonsterStateMachine target;
    private float damage;
    public Execution(PlayerStateMachine player) : base(player)
    {
        canChanged = false;
    }

    public void setTarget(MonsterStateMachine monster)
    {
        target = monster;
    }
    public override void Enter()
    {
        canChanged = false;
        damage = player.currentWeapon.status.attack * player.currentWeapon.status.execution_m;
        player.gameObject.layer = Layercache.Dodge; // ¹«Àû
        player.Rb.linearVelocity = Vector3.zero;
    }
    public override void Exit() 
    {
        player.gameObject.layer = Layercache.Player;
    }

    public void OnDamage()
    {
        if (target != null)
        {
            target.OnExeHit(damage);
        }
    }


}
