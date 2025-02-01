namespace ETLproj.Models;

public static class TripDataValidator
{
    public static bool IsValid(TripData record)
    {
        return record.PickupDatetime != null &&
               record.DropoffDatetime != null &&
               record.PassengerCount != null && record.PassengerCount > 0 &&
               record.TripDistance != null && record.TripDistance > 0 &&
               record.FareAmount != null && record.FareAmount > 0 &&
               record.TipAmount != null && record.TipAmount >= 0 &&
               record.PULocationID != null && record.PULocationID >= 0 &&
               record.DOLocationID != null && record.DOLocationID >= 0 &&
               !string.IsNullOrEmpty(record.StoreAndFwdFlag) &&
               (record.StoreAndFwdFlag == "Yes" || record.StoreAndFwdFlag == "No");
    }
}