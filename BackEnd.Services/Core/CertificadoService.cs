using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Services.Core
{
    public  class CertificadoService
    {
        public static string GenerarClavePrivada()
        {
            using (RSA rsa = RSA.Create(2048)) // Generar clave privada de 2048 bits
            {
                Console.WriteLine("Generando clave privada RSA de 2048 bits...");
                return ExportarClavePrivadaPEM(rsa); // Retorna la clave privada en formato PEM
            }
        }

        // Método 2: Generar CSR (retorna en formato PEM)
        public static string GenerarCSR(string privateKeyPem, string ORGANIZACION, string COMMON_NAME, string SERIAL_NUMBER)
        {
            // Convertir la clave privada PEM a un objeto RSA
            RSA rsa = ImportarClavePrivadaDesdePEM(privateKeyPem);

            // Crear la solicitud de firma del certificado (CSR)
            var request = new CertificateRequest(
                new X500DistinguishedName($"C=AR, O={ORGANIZACION}, CN={COMMON_NAME}, SERIALNUMBER={SERIAL_NUMBER}"),
                rsa,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);

            // Exportar el CSR en formato PEM
            var csr = request.CreateSigningRequest();
            var csrPem = $"-----BEGIN CERTIFICATE REQUEST-----\n{Convert.ToBase64String(csr)}\n-----END CERTIFICATE REQUEST-----";
            return csrPem;  // Retorna el CSR en formato PEM
        }

       

        // Método 4: Generar archivo .p12 usando clave privada y certificado (retorna en bytes)
        public static byte[] GenerarP12(string privateKeyPem, byte[] certificadoBytes, string password)
        {
            // Convertir la clave privada PEM a un objeto RSA
            RSA rsa = ImportarClavePrivadaDesdePEM(privateKeyPem);

            // Leer el certificado desde los bytes
            X509Certificate2 certificado = new X509Certificate2(certificadoBytes);

            // Crear un archivo PKCS#12 (.p12) que contenga la clave privada y el certificado
            X509Certificate2 certificadoConClave = certificado.CopyWithPrivateKey(rsa);
            byte[] p12Bytes = certificadoConClave.Export(X509ContentType.Pfx, password);

            return p12Bytes;  // Retorna el archivo .p12 en formato de bytes
        }

        // Método auxiliar para exportar la clave privada en formato PEM
        private static string ExportarClavePrivadaPEM(RSA rsa)
        {
            var builder = new System.Text.StringBuilder();
            var privateKey = rsa.ExportRSAPrivateKey();
            builder.AppendLine("-----BEGIN PRIVATE KEY-----");
            builder.AppendLine(Convert.ToBase64String(privateKey, Base64FormattingOptions.InsertLineBreaks));
            builder.AppendLine("-----END PRIVATE KEY-----");
            return builder.ToString();
        }

        // Método auxiliar para importar la clave privada desde un archivo PEM
        private static RSA ImportarClavePrivadaDesdePEM(string pem)
        {
            // Eliminar las etiquetas de inicio y fin de la clave privada
            var pemLines = pem.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var base64 = string.Join("", pemLines, 1, pemLines.Length - 2);
            byte[] privateKeyBytes = Convert.FromBase64String(base64);

            // Importar la clave privada
            RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
            return rsa;
        }
    }
}
