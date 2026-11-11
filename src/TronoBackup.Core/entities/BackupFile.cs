using System;

namespace TronoBackup.Core.Entities
{
    public class BackupFile
    {
        public string RelativePath { get; }
        public long SizeInBytes { get; }
        public string Sha256 { get; }

        private static bool IsValidHash(string sha256)
        {
            if (string.IsNullOrWhiteSpace(sha256))
                return false;
            if (sha256.Length != 64)
                return false;
            foreach (char c in sha256)
            {
                if (!Uri.IsHexDigit(c))
                    return false;
            }
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
                throw new ArgumentException("La ruta no puede estar vacía.", nameof(relativePath));
            }
            else if (sizeInBytes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sizeInBytes));
            }
            else if (!IsValidHash(sha256))
            {
                throw new ArgumentException("El hash SHA-256 no es válido.", nameof(sha256));
            }
        }
    }
}