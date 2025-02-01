namespace ETLproj.Models;
public class TripDataComparer : IEqualityComparer<TripData>
{
    public bool Equals(TripData x, TripData y)
    {
        if (x == null || y == null)
            return false;

        return x.tpep_pickup_datetime == y.tpep_pickup_datetime &&
               x.tpep_dropoff_datetime == y.tpep_dropoff_datetime &&
               x.passenger_count == y.passenger_count;
    }

    public int GetHashCode(TripData obj)
    {
        return HashCode.Combine(obj.tpep_pickup_datetime, obj.tpep_dropoff_datetime, obj.passenger_count);
    }
}