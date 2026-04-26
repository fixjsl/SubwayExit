
using TMPro;
using UnityEngine;

public abstract class ItObjectBase : MonoBehaviour, Iinterectable
{
    public abstract bool isStuck { get; }
    public abstract string InteractMessage { get; }

    [SerializeField] private GameObject interactPrompt;
    private TextMeshPro promptText;

    protected bool isInteracting = false;
    private PlayerStateMachine currentPlayer;
    private float lastInteractTime = -Mathf.Infinity;

    protected virtual float interactCooldown => 0f;

    protected virtual void Start()
    {
        if (interactPrompt != null)
        {
            interactPrompt.TryGetComponent(out promptText);
            if (promptText == null)
                promptText = interactPrompt.GetComponentInChildren<TextMeshPro>();
            interactPrompt.SetActive(false);
        }
    }

    private void ShowPrompt()
    {
        if (interactPrompt == null) return;
        if (promptText != null) promptText.text = InteractMessage;
        interactPrompt.SetActive(true);
    }

    private void HidePrompt()
    {
        if (interactPrompt != null) interactPrompt.SetActive(false);
    }

    public void Oninterect(Vector3 interacterPosition)
    {
        Debug.Log("Interect called on " + gameObject.name);
        if (isInteracting || Time.time < lastInteractTime + interactCooldown) return;

        isInteracting = true;
        lastInteractTime = Time.time;
        OnInteractInternal(interacterPosition);

        if (promptText != null && interactPrompt.activeSelf)
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
        ShowPrompt();
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<PlayerStateMachine>(out var player)) return;
        player.ClearInteractable(this);
        if (player == currentPlayer) currentPlayer = null;
        HidePrompt();
    }

    private void OnDisable()
    {
        if (currentPlayer != null)
        {
            currentPlayer.ClearInteractable(this);
            currentPlayer = null;
        }
        HidePrompt();
    }
}
