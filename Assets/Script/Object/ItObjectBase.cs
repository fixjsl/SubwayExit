using UnityEngine;

public abstract class ItObjectBase : MonoBehaviour, Iinterectable
{
    public abstract bool isStuck { get; }
    protected bool isInteracting = false;

    public void Oninterect(Vector3 interacterPosition)
    {
        if (isInteracting) return;
        isInteracting = true;
        OnInteractInternal(interacterPosition);
    }
    protected abstract void OnInteractInternal(Vector3 interacterPosition);

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerStateMachine>();
        if (player != null) player.SetInteractable(this);
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerStateMachine>();
        if (player != null) player.ClearInteractable();
    }
}
