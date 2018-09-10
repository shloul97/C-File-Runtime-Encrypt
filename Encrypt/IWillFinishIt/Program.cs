using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;

namespace IWillFinishIt
{
	/// <summary>
	/// i want to 
	/// 1- encrypt method
	/// 2- decrypt method
	/// Soo .. i want to launc "execute" to decrypt file
	/// 
	/// </summary>
	class Program
	{
		public class EncryptionClass
		{
		/// <summary>
		/// 
		/// i will encrypt File with XOR algorathim
		/// 
		/// </summary>
		/// <param name="file"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		/// 

				///This programe Encrypt File in XOR 
				///To Excute <<<<Runtime>>>> Encrypted 
			public static byte[] Encryption(string file, string key)
			{
				//FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
				byte[] fsBytes = File.ReadAllBytes(file);
				byte[] keyBytes = Encoding.ASCII.GetBytes(key);
				byte[] fsEncryptByte;
				
				for (int i = 0; i <= key.Length - 1; i++)
				{
					fsBytes[i] = (byte)(fsBytes[i] ^ keyBytes[i]);
				}


				return fsBytes;
			}
			public static byte[] Decryption(string file, string key)
			{
				byte[] fsBytes = File.ReadAllBytes(file);
				byte[] keyBytes = Encoding.ASCII.GetBytes(key);
				byte[] fsEncryptByte;

				for (int i = 0; i <= key.Length - 1; i++)
				{
					fsBytes[i] = (byte)(keyBytes[i] ^ fsBytes[i]);
				}
				return fsBytes;
			}
		}
		static void Main(string[] args)
		{
			Console.WriteLine("..........File Bytes.........");
			string file = @"D:\Atbash.exe"; //File Will Be Encrypted <---------------
			string key = "abcde";
			
			byte[] fileBytes = File.ReadAllBytes(file);
			for (int i = 0; i <= fileBytes.Length-1; i++)
			{
				Console.Write(fileBytes[i] + " ");
			}
			Console.WriteLine("\n \n"); 
			Console.WriteLine("..........File Encrypted Bytes.........");
			byte[] fileEncryptedBytes = EncryptionClass.Encryption(file, key);
			File.WriteAllBytes(@"D:\AtbashEncrypt.exe", fileEncryptedBytes);

			string fileE = @"D:\AtbashEncrypt.exe"; //You want this file name in Excute programe
			for (int i = 0; i <= fileEncryptedBytes.Length - 1; i++)
			{
				//File Encrypted Byte Copy output To paste in Excute Programe 
				Console.Write(fileEncryptedBytes[i] + " ");
			}
			Console.WriteLine("\n \n");
			Console.WriteLine("..........File Decrypted Bytes.........");
			byte[] fileDecryptedBytes = EncryptionClass.Decryption(fileE, key);
			for (int i = 0; i <= fileDecryptedBytes.Length - 1; i++)
			{
				Console.Write(fileDecryptedBytes[i] + " ");
			}
			Console.ReadKey();
		}
	}
}
