using UnityEngine;
using UnityEngine.UI;

public class MedkitDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform MedkitRectTransform; // Drag the health bar's RectTransform here
    [SerializeField] private Camera mainCamera; // Assign your main camera here
    [SerializeField] private PlayerMovement player; // Reference to player
    [SerializeField] private Image medkitBar;
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private Vector3 MedkitBarOffset = new Vector3(0, 0, 0); // Adjust the Y position to move the bar above the player
    [SerializeField] private InventoryManager inventory;

    private void Update()
    {
        // Keep the health bar fixed relative to the camera (in screen space)
        UpdateBar();

        // Update the health bar position based on the player's health
        Vector3 playerScreenPosition = mainCamera.WorldToScreenPoint(player.transform.position + MedkitBarOffset);
        MedkitRectTransform.position = playerScreenPosition;
    }

    private void UpdateBar()
    {
        // Update health bar position to follow the player
        Vector3 playerPosition = player.transform.position + MedkitBarOffset;

        // Convert world position to screen position (this works for World Space Canvas)
        MedkitRectTransform.position = mainCamera.WorldToScreenPoint(playerPosition);

        // Optional: You can add a log or visual check to see the health range
        int medkitAmount = inventory.MedKit;
        if (medkitAmount == 0)
        {
            medkitBar.sprite = sprites[0];
        }
        else if (medkitAmount == 1)
        {
            medkitBar.sprite = sprites[1];
        }
        else if (medkitAmount == 2)
        {
            medkitBar.sprite = sprites[2];
        }

    }
}
