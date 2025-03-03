// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.Encrypt
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace m2ostnextservice.Models
{
  public class Encrypt
  {
    private const string initVector = "pemgail9uzpgzl88";
    private const int keysize = 256;

    public string EncryptString(string plainText, string passPhrase)
    {
      byte[] bytes1 = Encoding.UTF8.GetBytes("pemgail9uzpgzl88");
      byte[] bytes2 = Encoding.UTF8.GetBytes(plainText);
      byte[] bytes3 = new PasswordDeriveBytes(passPhrase, (byte[]) null).GetBytes(32);
      RijndaelManaged rijndaelManaged = new RijndaelManaged();
      rijndaelManaged.Mode = CipherMode.CBC;
      ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(bytes3, bytes1);
      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write);
      cryptoStream.Write(bytes2, 0, bytes2.Length);
      cryptoStream.FlushFinalBlock();
      byte[] array = memoryStream.ToArray();
      memoryStream.Close();
      cryptoStream.Close();
      return Convert.ToBase64String(array);
    }

    public string DecryptString(string cipherText, string passPhrase)
    {
      byte[] bytes1 = Encoding.ASCII.GetBytes("pemgail9uzpgzl88");
      byte[] buffer = Convert.FromBase64String(cipherText);
      byte[] bytes2 = new PasswordDeriveBytes(passPhrase, (byte[]) null).GetBytes(32);
      RijndaelManaged rijndaelManaged = new RijndaelManaged();
      rijndaelManaged.Mode = CipherMode.CBC;
      ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(bytes2, bytes1);
      MemoryStream memoryStream = new MemoryStream(buffer);
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, decryptor, CryptoStreamMode.Read);
      byte[] numArray = new byte[buffer.Length];
      int count = cryptoStream.Read(numArray, 0, numArray.Length);
      memoryStream.Close();
      cryptoStream.Close();
      return Encoding.UTF8.GetString(numArray, 0, count);
    }
  }
}
