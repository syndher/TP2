using UnityEngine;
using UnityEngine.UI;

public class HPDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform healthBarRectTransform; // Drag the health bar's RectTransform here
    [SerializeField] private Camera mainCamera; // Assign your main camera here
    [SerializeField] private PlayerMovement player; // Reference to player
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image healthBar;

    [SerializeField] private Vector3 healthBarOffset = new Vector3(0, 0, 0); // Adjust the Y position to move the bar above the player

    private void Update()
    {
        // Keep the health bar fixed relative to the camera (in screen space)
        UpdateBar();

        // Update the health bar position based on the player's health
        Vector3 playerScreenPosition = mainCamera.WorldToScreenPoint(player.transform.position + healthBarOffset);
        healthBarRectTransform.position = playerScreenPosition;
    }

    private void UpdateBar()
    {
        // Update health bar position to follow the player
        Vector3 playerPosition = player.transform.position + healthBarOffset;
        
        // Convert world position to screen position (this works for World Space Canvas)
        healthBarRectTransform.position = mainCamera.WorldToScreenPoint(playerPosition);

        // Optional: You can add a log or visual check to see the health range
        int healthRange = Mathf.FloorToInt(player.currentHealth / 10) * 10;
        switch (healthRange)
        {
            case 90:
                healthBar.sprite = sprites[0];
                break;
            case 80:
                healthBar.sprite = sprites[1];
                break;
            case 70:
                healthBar.sprite = sprites[2];
                break;
            case 60:
                healthBar.sprite = sprites[3];
                break;
            case 50:
                healthBar.sprite = sprites[4];
                break;
            case 40:
                healthBar.sprite = sprites[5];
                break;
            case 30:
                healthBar.sprite = sprites[6];
                break;
            case 20:
                healthBar.sprite = sprites[7];
                break;
            case 10:
                healthBar.sprite = sprites[8];
                break;
            case 0:
                healthBar.sprite = sprites[9];
                break;
            default:

                break;
        }
    }
}
