using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform AmmoRectTransform; // Drag the health bar's RectTransform here
    [SerializeField] private Camera mainCamera; // Assign your main camera here
    [SerializeField] private PlayerMovement player; // Reference to player
    [SerializeField] private Image AmmoBar;
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private Vector3 AmmoBarOffset = new Vector3(0, 0, 0); // Adjust the Y position to move the bar above the player
    [SerializeField] private InventoryManager inventory;

    private void Update()
    {
        // Keep the health bar fixed relative to the camera (in screen space)
        UpdateBar();

        // Update the health bar position based on the player's health
        Vector3 playerScreenPosition = mainCamera.WorldToScreenPoint(player.transform.position + AmmoBarOffset);
        AmmoRectTransform.position = playerScreenPosition;
    }

    private void UpdateBar()
    {
        // Update health bar position to follow the player
        Vector3 playerPosition = player.transform.position + AmmoBarOffset;
        
        // Convert world position to screen position (this works for World Space Canvas)
        AmmoRectTransform.position = mainCamera.WorldToScreenPoint(playerPosition);

        // Optional: You can add a log or visual check to see the health range
        int ammoAmount = inventory.Ammo;
        if (ammoAmount == 0)
        {
            AmmoBar.sprite = sprites[0];
        }
        else if (ammoAmount == 1)
        {
            AmmoBar.sprite = sprites[1];
        }
        else if (ammoAmount == 2)
        {
            AmmoBar.sprite = sprites[2];
        }
    }
}
