using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private int ammo;
    private int penAmmo;
    private int medKit;

    public int Ammo { get => ammo; private set => ammo = value; }
    public int PenAmmo { get => penAmmo; private set => penAmmo = value; }
    public int MedKit { get => medKit; private set => medKit = value; }
    public int maxSlots = 2;  // Maximum number of slots the player can have

    // Get the current number of used slots
    public int UsedSlots => ammo + penAmmo + medKit;

    // Add ammo to the inventory
    public void AddAmmo()
    {
        if (UsedSlots < maxSlots)  // Check if there's room in inventory
        {
            ammo++;
        }
    }

    // Remove ammo from inventory when reloading
    public void UseAmmo()
    {
        ammo--;
    }

    // Add pen ammo to the inventory
    public void AddPenAmmo()
    {
        if (UsedSlots < maxSlots)  // Check if there's room in inventory
        {
            penAmmo++;
        }
    }

    // Remove pen ammo from inventory when reloading
    public void UsePenAmmo()
    {
        penAmmo--;
    }

    // Add medkit to the inventory
    public void AddMedKit()
    {
        if (UsedSlots < maxSlots)  // Check if there's room in inventory
        {
            medKit++;
        }
    }

    // Consume a medkit to restore health
    public void ConsumeMedKit()
    {
        medKit--;
    }
}
