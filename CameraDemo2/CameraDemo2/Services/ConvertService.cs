using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
using CameraDemo2.Models;
using Xamarin.Forms;

namespace CameraDemo2.Services
{
    public class ConvertService
    {
        public static ImageSource ImageFromBase64(string base64)
        {
            return ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(base64)));
        }

        public static async Task<string> Base64FromStream(Stream source)
        {
            string base64;
            var buffer = new byte[16 * 1024];
            var ms = new MemoryStream();

            int read;
            while ((read = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                await ms.WriteAsync(buffer, 0, read);
            }

            base64 = Convert.ToBase64String(buffer);
            return base64;  
        }
    }
}
