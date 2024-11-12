using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Repository
{
    public class EncryptDecrypt
    {
        public async Task<string> Encrypt(string input, string EncryptionKey = null)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                //string key = DNFINSResources.GetEncryptionKey("encryptionKey");
                string key = "umsumstLoanNcrKe";
                if (!string.IsNullOrWhiteSpace(key))
                {
                    byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
                    TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                    tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                    tripleDES.Mode = CipherMode.ECB;
                    tripleDES.Padding = PaddingMode.PKCS7;
                    ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                    tripleDES.Clear();
                    result =Convert.ToBase64String(resultArray, 0, resultArray.Length);
                }
            }
            return await Task.FromResult(result);
        }


        public async Task<string> Decrypt(string input, string EncryptionKey = null)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                try
                {
                    // string key = DNFINSResources.configuration.GetValue<string>("encryptionKey");
                    // string key = DNFINSResources.GetEncryptionKey("encryptionKey");
                    string key = "umsumstLoanNcrKe";
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        byte[] inputArray = Convert.FromBase64String(input);
                        TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                        tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                        tripleDES.Mode = CipherMode.ECB;
                        tripleDES.Padding = PaddingMode.PKCS7;
                        ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                        tripleDES.Clear();
                        result = UTF8Encoding.UTF8.GetString(resultArray);
                    }
                }
                catch (Exception ex)
                {
                    result = string.Empty;
                }
            }
            return await Task.FromResult(result);
        }
    }
}
