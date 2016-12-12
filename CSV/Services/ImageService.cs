using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSV.Services
{
    public class ImageService
    {
        public List<string> ReadImageDetails(string path)
        {
            try
            {
                var imageDetails = Directory.GetFiles(path, "*.jpeg*", SearchOption.AllDirectories)
                .ToList();
                return ParseImageNames(imageDetails);
            }
            catch
            {
                return null;
            }            
        }

        private List<string> ParseImageNames(List<string> imageDetails)
        {
            List<string> parsedImageNames = new List<string>();

            foreach (string image in imageDetails)
                parsedImageNames.Add(Path.GetFileNameWithoutExtension(image));
            return parsedImageNames;
        }
    }
}
