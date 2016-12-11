using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSV.Services
{
    public class ImageService
    {
        public List<string> ReadImageDetails(string path)
        {
            var imageDetails = Directory.GetFiles(path, "*.jpg*", SearchOption.AllDirectories)
                .ToList();
            return ParseImageNames(imageDetails);
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
