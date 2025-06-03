using UnityEngine;

public class P_MedKit : MonoBehaviour, IPickup
{
    private InventoryManager inventory;
    public event System.Action OnPickedUp;  // Explicit System.Action

    private void Awake()
    {
        inventory = FindFirstObjectByType<InventoryManager>();
    }

    // Interface implementation - main pickup handler
    public void Pickup()
    {
        if (inventory != null && inventory.UsedSlots < inventory.maxSlots)
        {
            inventory.AddMedKit();
            OnPickedUp?.Invoke();  // Trigger event before destruction
            Destroy(gameObject);
        }
    }

    // Optional: Keep as separate method if needed elsewhere
    public void PickUpMedKit()
    {
        Pickup();  // Just call the interface method
    }

}