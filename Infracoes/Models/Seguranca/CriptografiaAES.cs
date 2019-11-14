using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infracoes.Models.Seguranca
{
    public class CriptografiaAES
    {
        private RijndaelManaged aes;

        public CriptografiaAES()
        {
            aes = new RijndaelManaged();
            aes.Mode = CipherMode.CBC;
            aes.Key = Encoding.UTF8.GetBytes("2fd03c26-275c-4f65-8d73-3a4c4320"); // 256 bits (32 bytes)
            aes.IV = Encoding.UTF8.GetBytes("47d3a9c1-d1c6b76"); // 128 bits (16 bytes)
        }

        public string Encriptar(string textoPlano)
        {
            ICryptoTransform cifrador = aes.CreateEncryptor();

            byte[] bTextoPlano = Encoding.UTF8.GetBytes(textoPlano);

            MemoryStream msCifrador = new MemoryStream();
            CryptoStream csCifrador = new CryptoStream(msCifrador, cifrador, CryptoStreamMode.Write);

            csCifrador.Write(bTextoPlano, 0, bTextoPlano.Length);
            csCifrador.FlushFinalBlock();

            msCifrador.Close();
            csCifrador.Close();

            return Convert.ToBase64String(msCifrador.ToArray());
        }

        public string Decriptar(string textoEncriptado)
        {
            ICryptoTransform cifrador = aes.CreateDecryptor();

            byte[] bTextoEncriptado = Convert.FromBase64String(textoEncriptado);
            byte[] bTextoPlano = new byte[bTextoEncriptado.Length];

            MemoryStream msCifrador = new MemoryStream(bTextoEncriptado);
            CryptoStream csCifrador = new CryptoStream(msCifrador, cifrador, CryptoStreamMode.Read);

            int contador = csCifrador.Read(bTextoPlano, 0, bTextoPlano.Length);

            msCifrador.Close();
            csCifrador.Close();

            return Encoding.UTF8.GetString(bTextoPlano, 0, contador);
        }
    }
}