using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SymmetriskKryptering.Classes
{
    public class SymmetricEncryptor
    {
        private readonly SymmetricAlgorithm algorithm;

        // Constructor for SymmetricEncryptor class
        public SymmetricEncryptor(SymmetricAlgorithm algorithm)
        {
            this.algorithm = algorithm;
        }

        // Method to generate encryption keys
        public void GenerateKeys()
        {
            algorithm.GenerateKey(); // Generate encryption key
            algorithm.GenerateIV(); // Generate initialization vector (IV)
            Console.WriteLine("Encryption Key (Base64): " + Convert.ToBase64String(algorithm.Key)); // Display encryption key
            Console.WriteLine("Initialization Vector (Base64): " + Convert.ToBase64String(algorithm.IV)); // Display IV
        }

        // Method to encrypt plaintext
        public string Encrypt(string plaintext)
        {
            ICryptoTransform encryptor = algorithm.CreateEncryptor(); // Create encryptor object
            byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext); // Convert plaintext to bytes
            byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length); // Encrypt plaintext
            return Convert.ToBase64String(cipherBytes); // Convert encrypted bytes to Base64 string
        }

        // Method to decrypt ciphertext
        public string Decrypt(string ciphertext)
        {
            ICryptoTransform decryptor = algorithm.CreateDecryptor(); // Create decryptor object
            byte[] cipherBytes = Convert.FromBase64String(ciphertext); // Convert Base64 string to bytes
            byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length); // Decrypt ciphertext
            return Encoding.UTF8.GetString(plainBytes); // Convert decrypted bytes to string
        }
    }
}
