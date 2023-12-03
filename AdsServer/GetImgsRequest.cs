using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace AdsServer
{
    internal class GetImgsRequest
    {
        [JsonIgnore]
        public static string ADFOLDER_DIR = "AdsFolder";
        [JsonIgnore]
        public static string ADFOLDER_HYPERLINK_FILE = "hyperlink.txt";
        [JsonIgnore]
        public static string ADFOLDER_IMGFILE = "img";
        string[] imageFileExtension = new string[] { ".png", ".jpeg", ".jpg", ".webp" };
        public GetImgsRequest(string id) {
            Success = true;
            var path = Path.Combine(ADFOLDER_DIR, id);
            hyperlink = File.ReadAllText(Path.Combine (path, ADFOLDER_HYPERLINK_FILE));
            for (int i = 0; i < imageFileExtension.Length; i++)
            {
               var extension = imageFileExtension[i];
                picture = TryGetFile((Path.Combine(path, ADFOLDER_IMGFILE + extension)));
                if (picture != null) { break; }
            }
      
            if (picture == null)
            {
                Success = false;
                message = "Cannot find the image";
            }
          
        }
        
        public bool Success;
        public string message;
        public string hyperlink;
        public byte[] picture;
        byte[] TryGetFile(string fileName)
        {
            byte[] res = null;
            try
            {
                res = File.ReadAllBytes(fileName);
            }
            catch
            {

            }
            return res;
        }
        
    }

}
