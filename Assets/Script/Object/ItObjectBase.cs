
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItObjectBase : MonoBehaviour, Iinterectable
{
    public abstract bool isStuck { get; }
    public abstract string InteractMessage { get; }
    [SerializeField] private GameObject TextObject;
    [SerializeField] private TextMeshPro promptText;

    protected bool isInteracting = false;
    private PlayerStateMachine currentPlayer;
    private float lastInteractTime = -Mathf.Infinity;

    protected virtual float interactCooldown => 0f;

    protected virtual void Start()
    {
        TextObject.SetActive(false);
    }


    public void Oninterect(Vector3 interacterPosition)
    {
        Debug.Log("Interect called on " + gameObject.name);
        if (isInteracting || Time.time < lastInteractTime + interactCooldown) return;

        isInteracting = true;
        lastInteractTime = Time.time;
        OnInteractInternal(interacterPosition);

   
            promptText.text = InteractMessage;

        if (currentPlayer != null && !gameObject.activeInHierarchy)
        {
            currentPlayer.ClearInteractable(this);
            currentPlayer = null;
        }
    }
    protected abstract void OnInteractInternal(Vector3 interacterPosition);

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerStateMachine>(out var player)) return;
        currentPlayer = player;
        player.SetInteractable(this);
        promptText.text = InteractMessage;
        TextObject.SetActive(true);
        OnPlayerEntered(player);
    }

    protected virtual void OnPlayerEntered(PlayerStateMachine player) { }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<PlayerStateMachine>(out var player)) return;
        player.ClearInteractable(this);
        if (player == currentPlayer) currentPlayer = null;
        TextObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (currentPlayer != null)
        {
            currentPlayer.ClearInteractable(this);
            currentPlayer = null;
        }
        TextObject.SetActive(false);
    }
}
