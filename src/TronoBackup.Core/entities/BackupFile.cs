using System;
using System.Runtime.CompilerServices;

namespace TronoBackup.Core.Entities
{
    public class BackupFile
    {
        public string RelativePath { get; }
        public long SizeInBytes { get; }
        public string Sha256 { get; }
        public bool IsValid { get; private set; }

        public bool IsValidHash(string sha256)
        {
            if (string.IsNullOrWhiteSpace(sha256))
                return false;
            if (sha256.Length != 64)
                return false;
            if (!Uri.IsHexDigit(sha256[0]))
                return false;
            return true;
        }

        public BackupFile(string relativePath, long sizeInBytes, string sha256)
        {
            RelativePath = relativePath;
            SizeInBytes = sizeInBytes;
            Sha256 = sha256;

            //Validations
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                IsValid = false;
                throw new ArgumentException("La ruta no puede estar vacía.", nameof(relativePath));
            }
            else if (sizeInBytes < 0)
            {
                IsValid = false;
                throw new ArgumentOutOfRangeException(nameof(sizeInBytes));
            }
            else if (!IsValidHash(sha256))
            {
                IsValid = false;
                throw new ArgumentException("El hash SHA-256 no es válido.", nameof(sha256));
            }
            else
            {
                IsValid = true;
            }

            if (IsValid == false)
            {
                throw new ArgumentException("El archivo de respaldo no es válido.");
            }
        }
    }
}