using UnityEngine;
using UnityEngine.UI;

public class BulletsDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform BulletsRectTransform; // Drag the health bar's RectTransform here
    [SerializeField] private Camera mainCamera; // Assign your main camera here
    [SerializeField] private PlayerMovement player; // Reference to player
    [SerializeField] private Image bulletsBar;
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private Vector3 BulletsBarOffset = new Vector3(0, 0, 0); // Adjust the Y position to move the bar above the player
    [SerializeField] private InventoryManager inventory;

    private void Update()
    {

        // Update the health bar position based on the player's health
        Vector3 playerScreenPosition = mainCamera.WorldToScreenPoint(player.transform.position + BulletsBarOffset);
        BulletsRectTransform.position = playerScreenPosition;
        Vector3 playerPosition = player.transform.position + BulletsBarOffset;
        BulletsRectTransform.position = mainCamera.WorldToScreenPoint(playerPosition);
        UpdateBar();
    }

    public void UpdateBar()
    {


        // Optional: You can add a log or visual check to see the health range
        int bulletsAmount = player.currentBullets;
        if (bulletsAmount == 0)
        {
            bulletsBar.sprite = sprites[0];
        }
        else if (bulletsAmount == 1)
        {
            bulletsBar.sprite = sprites[1];
        }
        else if (bulletsAmount == 2)
        {
            bulletsBar.sprite = sprites[2];
        }
        else if (bulletsAmount == 3)
        {
            bulletsBar.sprite = sprites[3];
        }
        else if (bulletsAmount == 4)
        {
            bulletsBar.sprite = sprites[4];
        }
        else if (bulletsAmount == 5)
        {
            bulletsBar.sprite = sprites[5];
        }
        else if (bulletsAmount == 6)
        {
            bulletsBar.sprite = sprites[6];
        }
    }
}
