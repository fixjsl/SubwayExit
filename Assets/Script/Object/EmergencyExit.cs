using UnityEngine;

public class EmergencyExit : ItObjectBase
{
    [SerializeField] private ItemBase keyItem;
    [SerializeField] private string noKeyMessage = "비상구 열쇠가 필요합니다";
    [SerializeField] private string hasKeyMessage = "탈출";

    public override bool isStuck => false;

    public override string InteractMessage
    {
        get
        {
            bool hasKey = keyItem != null &&
                          PlayerStateMachine.Instance != null &&
                          PlayerStateMachine.Instance.inventory.slots.ContainsKey(keyItem.itemcode);
            string msg = hasKey ? hasKeyMessage : noKeyMessage;
            return $"{msg} [{InputBindings.Interact}]";
        }
    }

    protected override void OnInteractInternal(Vector3 interacterPosition)
    {
        isInteracting = false;

        if (keyItem == null || !PlayerStateMachine.Instance.inventory.slots.ContainsKey(keyItem.itemcode))
        {
            RefreshPrompt();
            return;
        }

        GameManager.Instance.GameClear();
    }
}
