using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using ETLproj.Data;
using ETLproj.Models;

namespace ETLproj.Services;

public static class ETLDataService
{
    public static List<TripData> ExtractFromCsv(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower(),
            TrimOptions = TrimOptions.Trim
        };

        using (var reader = new StreamReader(filePath))

        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<TripDataMap>();

            var records = csv.GetRecords<TripData>().ToList();

            foreach (var record in records)
            {
                record.Id = Guid.NewGuid();
                record.StoreAndFwdFlag = record.StoreAndFwdFlag == "Y" ? "Yes" : "No";
            }

            return records;
        }
    }

    public static HashSet<TripData> RemoveInvalidData(HashSet<TripData> records)
    {
        var validRecords = new HashSet<TripData>();

        foreach (var record in records)
            if (TripDataValidator.IsValid(record))
                validRecords.Add(record);

        var invalids = records.Except(validRecords).ToHashSet();
        WriteInvalidsToFile(invalids);
        return validRecords;
    }

    public static void WriteInvalidsToFile(HashSet<TripData> invalids)
    {
        using (var writer = new StreamWriter("Invalids/invalids.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            csv.WriteRecords(invalids);
    }

    public static HashSet<TripData> RemoveDuplicates(List<TripData> records)
    {
        var distinctRecords = records.ToHashSet(new TripDataComparer());
        var duplicates = records.Except(distinctRecords).ToList();
        WriteDuplicatesToFile(duplicates);
        return distinctRecords;
    }

    public static void WriteDuplicatesToFile(List<TripData> duplicates)
    {
        using (var writer = new StreamWriter("Duplicates/duplicates.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            csv.WriteRecords(duplicates);
    }

    public static async Task InsertIntoDatabaseAsync(HashSet<TripData> distinctRecords)
    {
        if (distinctRecords == null || distinctRecords.Count == 0) return;

        using (var context = new TripDataDContext())
            await context.BulkInsertTripsAsync(distinctRecords);
    }
}