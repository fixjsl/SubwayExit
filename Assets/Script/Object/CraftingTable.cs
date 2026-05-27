using UnityEngine;

class CraftingTable : ItObjectBase
{
    public override bool isStuck => false;
    public override string InteractMessage => $"제작대 {InputBindings.Interact}";

    protected override void OnInteractInternal(Vector3 interacterPosition)
    {
        CraftingUI.Instance.Open();
        isInteracting = false;
    }
}
