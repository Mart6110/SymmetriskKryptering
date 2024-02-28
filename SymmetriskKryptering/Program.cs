using SymmetriskKryptering.Classes;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Text;

namespace SymmetriskKryptering
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Choose a symmetric encryption algorithm:");
                Console.WriteLine("1. AES");
                Console.WriteLine("2. DES");
                Console.WriteLine("3. Exit.");

                int choice;
                // Input validation
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                    continue;
                }

                SymmetricAlgorithm algorithm;
                // Select encryption algorithm based on user's choice
                switch (choice)
                {
                    case 1:
                        algorithm = new AesCryptoServiceProvider(); // AES algorithm
                        break;
                    case 2:
                        algorithm = new DESCryptoServiceProvider(); // DES algorithm
                        break;
                    case 3:
                        return; // Exit the program
                    default:
                        Console.WriteLine("Invalid choice. Exiting."); // Invalid input
                        return;
                }

                SymmetricEncryptor encryptor = new SymmetricEncryptor(algorithm); // Create encryptor object
                encryptor.GenerateKeys(); // Generate encryption keys

                Console.WriteLine("Enter your plaintext message:");
                string input = Console.ReadLine();

                Console.WriteLine($"Original Text: " + input);

                // Encryption
                var watch = Stopwatch.StartNew(); // Start measuring encryption time
                string cipherText = encryptor.Encrypt(input); // Encrypt plaintext
                watch.Stop(); // Stop measuring time
                Console.WriteLine($"Cipher Text (Base64): {cipherText}"); // Display encrypted text
                Console.WriteLine($"Cipher Text (HEX): {BitConverter.ToString(Encoding.UTF8.GetBytes(cipherText))}"); // Display encrypted text in HEX format
                Console.WriteLine($"Encryption Time: {watch.ElapsedMilliseconds} ms"); // Display encryption time

                // Decryption
                watch.Restart(); // Restart the stopwatch
                string decryptedText = encryptor.Decrypt(cipherText); // Decrypt ciphertext
                watch.Stop(); // Stop measuring time
                Console.WriteLine($"Decrypted Text: {decryptedText}"); // Display decrypted text
                Console.WriteLine($"Decrypted Text (HEX): {BitConverter.ToString(Encoding.UTF8.GetBytes(decryptedText))}"); // Display decrypted text in HEX format
                Console.WriteLine($"Decryption Time: {watch.ElapsedMilliseconds} ms"); // Display decryption time
            }
        }
    }
}
