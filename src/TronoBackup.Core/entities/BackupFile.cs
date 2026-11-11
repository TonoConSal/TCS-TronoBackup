using System;
using System.Security.Cryptography.X509Certificates;

public class BackupFile
{
    public string RelativePath { get; private set; }
    public long SizeInBytes { get; private set; }
    public string Sha256 { get; private set; }

    public static bool TryCreate(string relativePath, long sizeInBytes, string sha256, out BackupFile backupFile)
    {
        backupFile = null;
        if (string.IsNullOrWhiteSpace(relativePath))
            return false;
        if (sizeInBytes < 0)
            return false;
        if (string.IsNullOrWhiteSpace(sha256))
            return false;
        if (sha256.Length != 64 || !System.Text.RegularExpressions.Regex.IsMatch(sha256, "^[a-zA-Z0-9]+$"))
            return false;
        backupFile = new BackupFile
        {
            RelativePath = relativePath,
            SizeInBytes = sizeInBytes,
            Sha256 = sha256
        };
        return true;
    }

    public BackupFile()
	{
		
	}
}
