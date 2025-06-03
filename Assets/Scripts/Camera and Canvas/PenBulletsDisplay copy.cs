using UnityEngine;
using UnityEngine.UI;

public class PenBulletsDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform PenBulletsRectTransform; // Drag the health bar's RectTransform here
    [SerializeField] private Camera mainCamera; // Assign your main camera here
    [SerializeField] private PlayerMovement player; // Reference to player
    [SerializeField] private Image penBulletsBar;
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private Vector3 PenBulletsBarOffset = new Vector3(0, 0, 0); // Adjust the Y position to move the bar above the player
    [SerializeField] private InventoryManager inventory;

    private void Update()
    {

        // Update the health bar position based on the player's health
        Vector3 playerScreenPosition = mainCamera.WorldToScreenPoint(player.transform.position + PenBulletsBarOffset);
        PenBulletsRectTransform.position = playerScreenPosition;
        Vector3 playerPosition = player.transform.position + PenBulletsBarOffset;
        PenBulletsRectTransform.position = mainCamera.WorldToScreenPoint(playerPosition);
        UpdateBar();
    }

    public void UpdateBar()
    {


        // Optional: You can add a log or visual check to see the health range
        int penBulletsAmount = player.currentPenBullets;
        if (penBulletsAmount == 0)
        {
            penBulletsBar.sprite = sprites[0];
        }
        else if (penBulletsAmount == 1)
        {
            penBulletsBar.sprite = sprites[1];
        }
        else if (penBulletsAmount == 2)
        {
            penBulletsBar.sprite = sprites[2];
        }
        else if (penBulletsAmount == 3)
        {
            penBulletsBar.sprite = sprites[3];
        }
        else if (penBulletsAmount == 4)
        {
            penBulletsBar.sprite = sprites[4];
        }
    }
}
