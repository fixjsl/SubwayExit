using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPlacementController : MonoBehaviour
{
    public static ItemPlacementController Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatics() => Instance = null;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Material ghostMaterial;

    private PlaceableItem currentItem;
    private GameObject ghost;
    private float fixedZ;
    private bool isPlacing;
    private int startFrame;

    void Awake() => Instance = this;

    public void StartPlacement(PlaceableItem item)
    {
        if (isPlacing) return;
        currentItem = item;
        fixedZ = 10.82681f;
        PlayerStateMachine.Instance.SetMovementLocked(true);
        ghost = Instantiate(item.placePrefab);
        DisableGhostComponents();
        ApplyGhostMaterial();
        isPlacing = true;
        startFrame = Time.frameCount;
    }

    void Update()
    {
        if (!isPlacing) return;
        UpdateGhostPosition();
        if (Time.frameCount <= startFrame) return;
        if (Mouse.current.leftButton.wasPressedThisFrame) ConfirmPlacement();
        else if (Keyboard.current.escapeKey.wasPressedThisFrame) CancelPlacement();
    }

    private void UpdateGhostPosition()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        float camZ = Camera.main.WorldToScreenPoint(new Vector3(0f, 0f, fixedZ)).z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, camZ));
        float x = worldPos.x;

        Ray downRay = new Ray(new Vector3(x, 100f, fixedZ), Vector3.down);
        float groundY = PlayerStateMachine.Instance.transform.position.y;
        if (Physics.Raycast(downRay, out RaycastHit hit, 200f, groundLayer)
            && Vector3.Dot(hit.normal, Vector3.up) > 0.7f)
            groundY = hit.point.y;
        ghost.transform.position = new Vector3(x, groundY, fixedZ);
    }

    private void ConfirmPlacement()
    {
        if (!PlayerStateMachine.Instance.inventory.RemoveItem(currentItem))
        {
            CancelPlacement();
            return;
        }
        Instantiate(currentItem.placePrefab, ghost.transform.position, ghost.transform.rotation);
        EndPlacement();
    }

    private void CancelPlacement() => EndPlacement();

    private void EndPlacement()
    {
        PlayerStateMachine.Instance.SetMovementLocked(false);
        Destroy(ghost);
        ghost = null;
        currentItem = null;
        isPlacing = false;
    }

    private void DisableGhostComponents()
    {
        foreach (var col in ghost.GetComponentsInChildren<Collider>())
            col.enabled = false;
        foreach (var script in ghost.GetComponentsInChildren<MonoBehaviour>())
            script.enabled = false;
    }

    private void ApplyGhostMaterial()
    {
        if (ghostMaterial == null) return;
        foreach (var r in ghost.GetComponentsInChildren<Renderer>())
        {
            var mats = new Material[r.materials.Length];
            for (int i = 0; i < mats.Length; i++) mats[i] = ghostMaterial;
            r.materials = mats;
        }
    }
}
