using KYC_EDDPortal.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KYC_EDDPortal.Services
{
    public class UtilityService: IUtilityService
    {
        private static ILogger<UtilityService> _logging;
        private readonly string _secretKey;
        private readonly string _iv;
        private string menu_string;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _hcontext;
        public UtilityService(ILogger<UtilityService> logging, IHttpContextAccessor hcontext, IConfiguration configuration)
        {
            _logging = logging;
            _hcontext = hcontext;
            _configuration = configuration;
            _secretKey = _configuration["AppSettings:Key"]; 
            _iv = _configuration["AppSettings:Iv"]; 
        }
        public async Task<string> GeneratedMenuHtml(int RoleId, List<string> permissions)
        {
            var request = _hcontext.HttpContext.Request;

            var mainMenu = RoleId == 9999 ? MenuItemRepository.Menus() : MenuItemRepository.Menus();

            // Call MapMenuItemToPermission to map permissions to menu item codes
            var menuCodes = MapMenuItemToPermission(permissions);

            var MainMenuFiltered = mainMenu.OrderBy(t => t.Sequence).Select(x => x.Group).Distinct();

            foreach (var item in MainMenuFiltered)
            {
                var _items = mainMenu.Where(x => x.Group == item).OrderBy(x => x.Sequence);
                var hasSubItems = false;

                foreach (var x in _items)
                {
                    // Check if the menu code is in menuCodes (permissions)
                    if (menuCodes.Contains(x.Code))
                    {
                        if (!hasSubItems)
                        {
                            menu_string += $"<li class=\"active open\">\r\n";
                            menu_string += $"<a href=\"javascript:void(0);\" class=\"menu-toggle\"><i class=\"zmdi zmdi-apps\"></i><span style=\"color: black;\">{item}</span></a>\r\n";
                            menu_string += "<ul class=\"ml-menu\">\r\n";
                            hasSubItems = true;
                        }

                        menu_string += $"<li><a href=\"/Menu/ItemRun/{x.Code.Trim()}[]\" style=\"color: black;\">{x.Description}</a></li>\r\n";
                    }
                }

                if (hasSubItems)
                {
                    menu_string += "</ul>\r\n";
                    menu_string += "</li>\r\n";
                }
            }

            return menu_string;
        }
        public string Encrypt(string plaintext, CancellationToken cancellation)
        {
            try
            {
                // var keys = AesEncryptionRepository.ReadUserKey(username);
                //var getencryption = _encryptiondata.GetById(1);


                /// new LogHelper().Info(string.Format("Encrypting Plain Text"));
                using (Aes myAes = Aes.Create())
                {
                    //myAes.Key = Encoding.UTF8.GetBytes(getencryption.Key);
                    //myAes.IV = Encoding.UTF8.GetBytes(getencryption.Iv);

                    myAes.Key = Encoding.UTF8.GetBytes(_secretKey);
                    myAes.IV = Encoding.UTF8.GetBytes(_iv);

                    // Encrypt the string to an array of bytes.
                    byte[] encrypted = EncryptStringToBytes_Aes(plaintext, myAes.Key, myAes.IV);
                    string ciphertext = ByteArrayToString(encrypted);
                    //  new LogHelper().Info(string.Format("Encryption returned-{0}", ciphertext));
                    return ciphertext;
                }
            }
            catch (Exception ex)
            {
                _logging.LogError(ex.ToString(), "Encrypt");
                throw ex;
            }
        }
        private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor,
                    CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }
        public string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        public string Decrypt(string ciphertext, CancellationToken cancellation)
        {
            try
            {
                // var keys = AesEncryptionRepository.ReadUserKey(username);

                /// new LogHelper().Info(string.Format("Decrypting Ciphertext"));
                using (Aes myAes = Aes.Create())
                {
                    myAes.Key = Encoding.UTF8.GetBytes(_secretKey);
                    myAes.IV = Encoding.UTF8.GetBytes(_iv);

                    //myAes.Key = Encoding.UTF8.GetBytes(_secretKey);
                    //myAes.IV = Encoding.UTF8.GetBytes(_iv);

                    // Convert the ciphertext (hex string) back to byte array
                    byte[] encryptedBytes = StringToByteArray(ciphertext);

                    // Decrypt the byte array to plaintext
                    string plaintext = DecryptBytesToString_Aes(encryptedBytes, myAes.Key, myAes.IV);

                    // new LogHelper().Info(string.Format("Decryption returned-{0}", plaintext));
                    return plaintext;
                }
            }
            catch (Exception ex)
            {
                _logging.LogError(ex.ToString(), "Decrypt");
                throw ex;
            }
        }
        public byte[] StringToByteArray(string hex)
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
        private static string DecryptBytesToString_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext = null;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor,
                        CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
        private List<string> MapMenuItemToPermission(List<string> permissions)
        {
            var codeMap = new Dictionary<string, string>
            {
                { "INI", "A01" },
                { "KYC", "A02" },
                { "EDD", "A03" },
               { "REV", "A04" },
            };

            var codes = new List<string>();

            foreach (var code in permissions)
            {
                if (codeMap.ContainsKey(code))
                {
                    codes.Add(codeMap[code]);
                }
            }

            return codes;
        }
    }
}
