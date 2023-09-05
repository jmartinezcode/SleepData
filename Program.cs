// ask for input
using System.Globalization;

Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
string resp = Console.ReadLine();

if (resp == "1")
{
    // create data file

    // ask a question
    Console.WriteLine("How many weeks of data is needed?");
    // input the response (convert to int)
    int weeks = int.Parse(Console.ReadLine());
    // determine start and end date
    DateTime today = DateTime.Now;
    // we want full weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    // subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
    // random number generator
    Random rnd = new Random();
    // create file
    StreamWriter sw = new StreamWriter("data.txt");

    // loop for the desired # of weeks
    while (dataDate < dataEndDate)
    {
        // 7 days in a week
        int[] hours = new int[7];
        for (int i = 0; i < hours.Length; i++)
        {
            // generate random number of hours slept between 4-12 (inclusive)
            hours[i] = rnd.Next(4, 13);
        }
        // M/d/yyyy,#|#|#|#|#|#|#
        // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");// add 1 week to date
        dataDate = dataDate.AddDays(7);
    }
    sw.Close();
}
else if (resp == "2")
{
    string file = "data.txt";
    StreamReader sr = new StreamReader(file);
    while (!sr.EndOfStream)
    {
        string line = sr.ReadLine();
        string[] arr = line.Split(",");
        string dateString = arr[0];
        string data = arr[1];
        DateTime weekStartDate = DateTime.ParseExact(dateString, "M/d/yyyy", CultureInfo.InvariantCulture);
        int[] hoursOfSleep = Array.ConvertAll(data.Split("|"), int.Parse);
        DisplayReport(weekStartDate, hoursOfSleep);
    }


    
 
}
static void DisplayReport(DateTime date, int[] hoursOfSleep)
{
    Console.WriteLine($"Week of {date:MMM, dd, yyyy}");
    PrintDaysAndDashes();
    for (int i = 0; i < hoursOfSleep.Length; i++)
    {
        Console.Write($"{hoursOfSleep[i],3}");
    }
    Console.WriteLine("\n");
    
}
static void PrintDaysAndDashes()
{
    string[] days = { "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa" };
    string dash = "--";
    foreach (var day in days)
    {
        Console.Write($"{day,3}");
    }
    Console.WriteLine();
    for (int i = 0; i < days.Length; i++)
    {
        Console.Write($"{dash,3}");
    }
    Console.WriteLine();

}


