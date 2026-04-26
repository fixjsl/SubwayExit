using System.Collections.Generic;
using UnityEngine;

public class ContainerObject : ItObjectBase
{
    [SerializeField] private LootTable lootTable;
    [SerializeField] private ContainerUI containerUI;
    private bool looted = false;
    private List<(ItemBase item, int count)> contents;

    public override bool isStuck => false;
    public override string InteractMessage => looted ? "빈 상자" : "상자 열기  [F]";

    protected override void OnInteractInternal(Vector3 interacterPosition)
    {
        if (looted) return;

        if (contents == null)
            contents = lootTable != null ? lootTable.Roll() : new List<(ItemBase, int)>();

        isInteracting = false;
        containerUI.Open(contents, OnContentsChanged);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.TryGetComponent<PlayerStateMachine>(out _))
            containerUI.Close();
    }

    private void OnContentsChanged()
    {
        if (contents.Count == 0)
            looted = true;
    }
}
