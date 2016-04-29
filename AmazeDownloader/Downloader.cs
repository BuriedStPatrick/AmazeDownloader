using System.Collections.Generic;
using System.Net;

namespace AmazeDownloader
{
    public class Downloader
    {
        public Dictionary<string, Data> Dictionary { get; set; }
        public char[] String { get; set; }

        public Downloader(Dictionary<string, Data> dictionary)
        {
            Dictionary = dictionary;
        }

        public void Start()
        {
            using (var client = new WebClient())
            {
                foreach (var set in Dictionary)
                {
                    SaveFile(set.Key, set.Value, client);
                }    
            }
        }

        private void SaveFile(string url, Data data, WebClient client)
        {
            client.DownloadFile(url, "output/" + data.GetFilename());
        }
    }
}
