using UnityEngine;

public class P_Ammo : MonoBehaviour, IPickup
{
    private InventoryManager inventory;
    public event System.Action OnPickedUp; // Changed to explicit System.Action

    private void Awake()
    {
        inventory = FindFirstObjectByType<InventoryManager>();
    }

    // Interface implementation - THIS is what gets called when picked up
    public void Pickup()
    {
        if (inventory != null && inventory.UsedSlots < inventory.maxSlots)
        {
            inventory.AddAmmo();
            OnPickedUp?.Invoke(); // Trigger event BEFORE destruction
            Destroy(gameObject);
        }
    }

    // Optional: Keep this as a separate method if needed elsewhere
    public void PickUpAmmo()
    {
        Pickup(); // Just call the interface method
    }
}