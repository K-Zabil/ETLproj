using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
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
            var records = csv.GetRecords<TripData>().ToList();

            foreach (var record in records) {
                if(record.store_and_fwd_flag == "Y") record.store_and_fwd_flag = "Yes";
                else if(record.store_and_fwd_flag == "N") record.store_and_fwd_flag = "No";
                else record.store_and_fwd_flag = "";
            }

            return records;
        }
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
        using (var writer = new StreamWriter("duplicates.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(duplicates);
        }
    }
}