using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.DAO.Helper
{
    public static class CryptoHelper
    {
        private static byte[] KEYBYTES = Encoding.UTF8.GetBytes("51nc0soft_5.A.5."); //Mismo KEY usado en Javascript
        private static byte[] IV = Encoding.UTF8.GetBytes("_T1cs|Fon*5o_S45"); //Mismo IV usado en Javascript
        public static string EncryptString(this string textToCipher)
        {
            var res = EncryptStringToBytes(textToCipher);
            return Convert.ToBase64String(res);
        }
        public static byte[] EncryptStringToBytes(this string plainText)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            byte[] encrypted;
            // Create a RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = KEYBYTES;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }


        public static string DecryptString(this string cipherText)
        {
            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted);
            return decriptedFromJavascript;
        }



        private static string DecryptStringFromBytes(byte[] cipherText)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }



            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;



            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;



                rijAlg.Key = KEYBYTES;
                rijAlg.IV = IV;



                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);



                try
                {
                    // Create the streams used for decryption.
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    plaintext = "keyError";
                }
            }



            return plaintext;
        }

    }
}
