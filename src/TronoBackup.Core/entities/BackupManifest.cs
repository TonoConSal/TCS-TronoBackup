using System;
using System.Collections.Generic;
using TronoBackup.Core.Entities;

public class BackupManifest
{
    public int Version { get; }
    public Guid BackupId { get; }
    public DateTimeOffset CreatedAt { get; }
    public string Source { get; }
    private readonly List<BackupFile> _files = new();
    public IReadOnlyList<BackupFile> Files => _files.AsReadOnly();

    public BackupManifest(int version, Guid backupId, DateTimeOffset createdAt, string source, IEnumerable<BackupFile> files)
	{
        //Validations
        if (backupId == Guid.Empty)
        {
            throw new ArgumentException("Backup ID cannot be empty.", nameof(backupId));
        }
        if (string.IsNullOrWhiteSpace(source))
        {
            throw new ArgumentException("Source cannot be null or empty.", nameof(source));
        }
        if (version <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(version), "Version must be a positive integer.");
        }
        ArgumentNullException.ThrowIfNull(files);
        foreach (var file in files)
        {
            if (file == null)
            { 
                throw new ArgumentException(
                    "Files collection cannot contain null elements.",
                    nameof(files));
            }
        }


        Version = version;
        BackupId = backupId;
        CreatedAt = createdAt;
        Source = source;

        _files.AddRange(files);
    }
}
