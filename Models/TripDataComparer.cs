namespace ETLproj.Models;
public class TripDataComparer : IEqualityComparer<TripData>
{
    public bool Equals(TripData? x, TripData? y)
    {
        if (x == null || y == null) return false;

        return x.PickupDatetime == y.PickupDatetime &&
               x.DropoffDatetime == y.DropoffDatetime &&
               x.PassengerCount == y.PassengerCount;
    }

    public int GetHashCode(TripData obj) => HashCode.Combine(obj.PickupDatetime, obj.DropoffDatetime, obj.PassengerCount);
}