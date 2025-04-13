using Serilog;

namespace WebAutomationFramework.Utilities
{
    public class CommonMethods
    {
        private readonly Random _rand = new();

        public static string GetDate(string fromDate, string days)
        {
            var day = int.Parse(days);
            var splitDate = fromDate.Split('/');
            var dd = int.Parse(splitDate[0]);
            var mm = int.Parse(splitDate[1]);
            var yyyy = int.Parse(splitDate[2]);

            DateTime dt = new(yyyy, mm, dd);
            var toDate = dt.AddDays(day).ToString("dd/MM/yyyy");
            Log.Debug("To Date is calculated as : {0}", toDate);
            return toDate;
        }

        public void WaitInSeconds(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        public static void CreateFolder(string filePath)
        {
            try
            {
                Directory.CreateDirectory(filePath);
            }
            catch (Exception ex)
            {
                Log.Error("Couldn't create the directory in the file path {0} due to {1}",
                    filePath, ex.Message);
            }
        }

        public string GenerateRandomString(int lengthOfStringToGenerate)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, lengthOfStringToGenerate)
                .Select(s => s[_rand.Next(s.Length)]).ToArray());
        }
    }
}