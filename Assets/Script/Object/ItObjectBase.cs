
using UnityEngine;

public abstract class ItObjectBase : MonoBehaviour, Iinterectable
{
    public abstract bool isStuck { get; }
    protected bool isInteracting = false;
    private PlayerStateMachine currentPlayer;
    private float lastInteractTime = -Mathf.Infinity;

    protected virtual float interactCooldown => 0f;

    public void Oninterect(Vector3 interacterPosition)
    {
        Debug.Log("Interect called on " + gameObject.name);
        if (isInteracting || Time.time < lastInteractTime + interactCooldown) return;

        isInteracting = true;
        lastInteractTime = Time.time;
        OnInteractInternal(interacterPosition);

        // 상호작용 대상이 비활성화되거나 파괴되면 자동으로 리스트에서 제거
        if (currentPlayer != null && !gameObject.activeInHierarchy)
        {
            currentPlayer.ClearInteractable(this);
            currentPlayer = null;
        }
    }
    protected abstract void OnInteractInternal(Vector3 interacterPosition);

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerStateMachine>();
        if (player != null)
        {
            currentPlayer = player;
            player.SetInteractable(this);
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerStateMachine>();
        if (player != null)
        {
            if (!isInteracting)
            {
                player.ClearInteractable(this);
                if (player == currentPlayer)
                    currentPlayer = null;
            }
        }
    }

    private void OnDisable()
    {
        if (currentPlayer != null)
        {
            currentPlayer.ClearInteractable(this);
            currentPlayer = null;
        }
    }
}
