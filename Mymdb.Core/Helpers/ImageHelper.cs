using System.IO;

namespace Mymdb.Core.Helpers
{
    public static class ImageHelper
    {
        public static byte[] ConvertToByte(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static Stream ConvertToStream(this byte[] array)
        {
            return new MemoryStream(array);
        }
    }
}
