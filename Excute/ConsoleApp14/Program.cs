using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;

namespace ConsoleApp14
{
	/// <summary>
	/// I WANT 
	/// ---1- File Encrypted
	/// ---2- Decrypt Method
	/// ---3- Execute native code
	/// </summary>
	class Program
	{
		
		static void Main(string[] args)
		{
			string key = "abcde";
			string FileEncrypted = @"D:\AtbashEncrypt.exe";
			Byte[] bytesEncrypted = Decryption(FileEncrypted, key);
			//Process info = new Process();
			IntPtr myAddNativeCodeBytesPtr = IntPtr.Zero;
			try

			{

				// Allocate the native buffer

				myAddNativeCodeBytesPtr =

					Marshal.AllocCoTaskMem(bytesEncrypted.Length);

				// Push the code bytes over

				Marshal.Copy(bytesEncrypted, 0,

					myAddNativeCodeBytesPtr, bytesEncrypted.Length);

				// Get a function pointer for the native code bytes

				MyAdd myAdd = (MyAdd)Marshal.GetDelegateForFunctionPointer(

					myAddNativeCodeBytesPtr, typeof(MyAdd));

				// Call the native code bytes

				Int32 result = myAdd(3, 4);

				// Did it work?

				Console.WriteLine("Result: {0}", result);

			}
			finally

			{

				// Free the native buffer

				if (myAddNativeCodeBytesPtr != IntPtr.Zero)

				{

					Marshal.FreeCoTaskMem(myAddNativeCodeBytesPtr);

					myAddNativeCodeBytesPtr = IntPtr.Zero;

				}

			}

			Console.ReadKey();
		}

		private static void RunInternalExe(string exeName, String pass)
		{
			//Verify the Payload exists
			if (!File.Exists(exeName))
				return;

			//Read the raw bytes of the file
			//byte[] resourcesBuffer = File.ReadAllBytes(exeName);

			//Decrypt bytes from payload
			byte[] decryptedBuffer = null;
			decryptedBuffer = Decryption(exeName, pass);

			//If .NET executable -> Run
			if (Encoding.Unicode.GetString(decryptedBuffer).Contains("</assembly>"))
			{
				//Load the bytes as an assembly
				Assembly exeAssembly = Assembly.Load(decryptedBuffer);

				//Execute the assembly
				object[] parameters = new object[1];  //Don't know why but fixes TargetParameterCountException
				exeAssembly.EntryPoint.Invoke(null, parameters);
			}
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
		const uint PAGE_EXECUTE_READWRITE = 0x40;
		const uint MEM_COMMIT = 0x1000;

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

		private delegate int IntReturner();
		private delegate Int32 MyAdd(Int32 x, Int32 y);

	}
}
