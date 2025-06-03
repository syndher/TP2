public interface IPickup
{
    event System.Action OnPickedUp;
    void Pickup();
}