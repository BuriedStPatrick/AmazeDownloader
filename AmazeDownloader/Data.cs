namespace AmazeDownloader
{
    public class Data
    {
        private string Name { get; set; }
        private string Date { get; set; }

        public Data(string name, string date)
        {
            Name = name;
            Date = date;
        }

        public string GetFilename()
        {
            var name =  Date.Replace('/', '_')
                   + " - "
                   + MakeValidFileName(Name);
            return Truncate(name, 175) + ".pdf";
        }

        private static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        private static string MakeValidFileName(string name)
        {
            var invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            var invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_").Replace("&nbsp;", string.Empty);
        }
    }
}