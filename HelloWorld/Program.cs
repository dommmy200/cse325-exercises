using Humanizer;
// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
Console.WriteLine("Quantities:");
HumanizeQuantities();

Console.WriteLine("\nDate/Times Manipulation:");
HumanizeDates();

static void HumanizeQuantities()
{
    Console.WriteLine("case".ToQuantity(0));    // zero cases
    Console.WriteLine("case".ToQuantity(1));    // one case
    Console.WriteLine("case".ToQuantity(5));    // five cases
}

static void HumanizeDates()
{
    DateTime pastDate = DateTime.UtcNow.AddHours(-24);
    Console.WriteLine(pastDate.Humanize());  // 24 hours ago

    DateTime futureDate = DateTime.UtcNow.AddDays(-2);
    Console.WriteLine(futureDate.Humanize()); // in 30 minutes

    TimeSpan timeSpan1 = TimeSpan.FromDays(1);
    Console.WriteLine(timeSpan1.Humanize()); // in 30 minutes

    TimeSpan timeSpan2 = TimeSpan.FromDays(16);
    Console.WriteLine(timeSpan2.Humanize()); // in 30 minutes
}