// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

// var salesFiles = FindFiles("stores");
var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesSummaryDir = Path.Combine(currentDirectory, "salesSummaryDir");
Directory.CreateDirectory(salesSummaryDir);

var salesFiles = FindFiles(storesDirectory);
var salesTotal = CalculateSalesTotal(salesFiles);

var salesOnlyFiles = FindSalesFiles(storesDirectory);
var salesSummary = WriteSalesSummary(salesOnlyFiles);

// File.WriteAllText(Path.Combine(salesTotalDir, "totals.txt"), String.Empty);
File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

File.WriteAllText(Path.Combine(salesSummaryDir, "summary.txt"), salesSummary);
// foreach (var file in salesFiles)
// {
//     Console.WriteLine(file);
// }

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        // The file name will contain the full path, so only check the end of it
        // if (file.EndsWith("sales.json"))
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}
IEnumerable<string> FindSalesFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        // The file name will contain the full path, so only check the end of it
        if (file.EndsWith("sales.json"))
        // if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

static string WriteSalesSummary(IEnumerable<string> salesFiles)
{
    var sb = new StringBuilder();
    sb.AppendLine("Sales Summary");
    sb.AppendLine("-----------------------------");

    decimal totalSales = 0;
    var storeDetails = new StringBuilder();

    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        if (data != null)
        {
            totalSales += (decimal)data.Total;
            storeDetails.AppendLine($"{Path.GetFileNameWithoutExtension(file)}: {data.Total:C}");
        }
    }

    sb.AppendLine($"Total Sales: {totalSales:C}");
    sb.AppendLine();
    sb.AppendLine("Details:");
    sb.Append(storeDetails);

    return sb.ToString();
}

record SalesData(double Total);

