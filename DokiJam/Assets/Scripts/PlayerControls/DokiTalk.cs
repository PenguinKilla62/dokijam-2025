using UnityEngine;
using UnityEngine.Tilemaps;

public class DokiTalk : MonoBehaviour
{
    public LayerMask interactableLayer; // Layer for interactable objects
    public Tilemap interactableTilemap; // Tilemap for interactable objects
    public YarnCommandHandler yarnCommandHandler; // Reference to YarnCommandHandler for yarn stuff
    InputSystem_Actions inputActions;
    Vector2 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        inputActions.Player.Move.Enable();
        inputActions.Player.Interact.Enable();

        moveInput = Vector2.down;
    }

    void UpdatePOV()
    {
        if (inputActions.Player.Move.ReadValue<Vector2>() == Vector2.zero) {
            return;
        }
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
    }

    void CheckInteract()
    {
        if (inputActions.Player.Interact.IsPressed())
        {
            Debug.Log("Interact button pressed");
            // Add interaction logic here
            // Raycast2D to check for interactable objects
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveInput, 3f, interactableLayer);
            Debug.DrawRay(transform.position, moveInput, Color.green, 3f);
            Vector2 hitPoint = hit.point;
            Vector3Int cellPos = interactableTilemap.WorldToCell(hitPoint - hit.normal * 0.01f);
            TileBase tile = interactableTilemap.GetTile<TileBase>(cellPos);
            if (hit.collider != null)
            {
                Debug.Log("Interacted with: " + hit.collider.gameObject.name);
                // Call a method on the interactable object, e.g., hit.collider.GetComponent<Interactable>().Interact();
            }
            else
            {
                Debug.Log("No interactable object found in range.");
            }
            if (tile != null)
            {
                Debug.Log("Interacted with tile: " + tile.name);
                yarnCommandHandler.PlayYarn("Office_"+tile.name);
                // Call a method on the tile, e.g., interactableTilemap.GetComponent<Interactable>().Interact(cellPos);
            }
            else
            {
                Debug.Log("No tile found at the interacted position.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePOV();
        CheckInteract();
    }
}
