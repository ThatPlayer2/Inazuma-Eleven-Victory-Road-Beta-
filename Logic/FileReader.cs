using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inazuma_Eleven_Victory_Road_Beta.Logic
{
    internal class FileReader
    {
        public static byte[] fileBytes;

        public static int FindPattern(byte[] source, byte[] pattern, int startOffset, int endOffset)
        {

            if (startOffset < 0 || endOffset > source.Length || startOffset > endOffset)
            {
                return -1;
            }


            for (int i = startOffset; i < endOffset - pattern.Length + 1; i++)
            {
                if (source.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                {
                    return i;
                }
            }
            return -1;
        }

        public static void LoadFile(byte[] bytes)
        {
            fileBytes = bytes;
        }
        //==============================================================================


        public static byte[] StringToByteArray(string hex)
        {
            if (hex == null)
            {
                throw new ArgumentNullException(nameof(hex), "The hex string cannot be null.");
            }

            hex = hex.Replace("-", ""); // Rimuovi eventuali caratteri "-" dalla stringa
            byte[] byteArray = new byte[hex.Length / 2];

            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            return byteArray;
        }
    }
}
