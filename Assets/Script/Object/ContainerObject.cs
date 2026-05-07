using System.Collections.Generic;
using UnityEngine;

public class ContainerObject : ItObjectBase
{
    [SerializeField] private LootTable lootTable;
    [SerializeField] private ContainerUI containerUI;
    private bool open = false;
    private List<(ItemBase item, int count)> contents;

    [SerializeField] private Transform RotatePivot;

    public override bool isStuck => false;
    public override string InteractMessage => open ? $"닫기 [{InputBindings.Interact}]" : $"상자 열기 [{InputBindings.Interact}]";

    protected override void OnInteractInternal(Vector3 interacterPosition)
    {

        if (contents == null)
            contents = lootTable != null ? lootTable.Roll() : new List<(ItemBase, int)>();

        isInteracting = false;
        containerUI.Open(contents);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.TryGetComponent<PlayerStateMachine>(out _))
            containerUI.Close();
    }


}
