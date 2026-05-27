using UnityEngine;

[CreateAssetMenu(fileName = "PlaceableItem", menuName = "Scriptable Objects/Item/PlaceableItem")]
public class PlaceableItem : ItemBase
{
    public GameObject placePrefab;

    private void Reset() => itemType = ItemType.Consumable;

    public override void OnUse(PlayerStateMachine player)
    {
        ItemPlacementController.Instance.StartPlacement(this);
    }
}
