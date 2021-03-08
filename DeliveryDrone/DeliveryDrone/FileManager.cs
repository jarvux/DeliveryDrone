using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryDrone
{
    public class FileManager
    {
        private const int DefaultBufferSize = 4096;
        private const FileOptions DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

        public static async Task<List<string>> ReadAllLinesAsync(string path)
        {
            var lines = new List<string>();

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, DefaultOptions))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }
    }
}