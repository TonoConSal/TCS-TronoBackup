using System;
using System.Collections.Generic;
using TronoBackup.Core.Entities;

public class BackupManifestTest
{
	[Fact]
	public void Create_ShouldCreate_ValidBackupManifest()
    {
        // Arrange
        int version = 1;
        Guid backupId = Guid.NewGuid();
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        string source = "test/source";
        var files = new List<BackupFile>
        {
            new BackupFile("file1.txt", 1024, "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad"),
            new BackupFile("file2.txt", 2048, "b94d27b9934d3e08a52e52d7da7dabfac484efe37a5380ee9088f7ace2efcde9")
        };
        // Act
        var backupManifest = new BackupManifest(version, backupId, createdAt, source, files);
        // Assert
        Assert.Equal(version, backupManifest.Version);
        Assert.Equal(backupId, backupManifest.BackupId);
        Assert.Equal(createdAt, backupManifest.CreatedAt);
        Assert.Equal(source, backupManifest.Source);
        Assert.Equal(files.Count, backupManifest.Files.Count);
        for (int i = 0; i < files.Count; i++)
        {
            Assert.Equal(files[i].RelativePath, backupManifest.Files[i].RelativePath);
            Assert.Equal(files[i].SizeInBytes, backupManifest.Files[i].SizeInBytes);
            Assert.Equal(files[i].Sha256, backupManifest.Files[i].Sha256);
        }
    }
    [Fact]
    public void Create_ShouldThrowException_WhenGuidIsEmpty()
    {
        // Arrange
        int version = 1;
        Guid backupId = Guid.Empty;
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        string source = "test/source";
        var files = new List<BackupFile>();
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BackupManifest(version, backupId, createdAt, source, files));
    }
    [Fact]
    public void Create_ShouldThrowException_WhenSourceIsNull()
    {
        // Arrange
        int version = 1;
        Guid backupId = Guid.NewGuid();
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        string source = null!;
        var files = new List<BackupFile>();
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BackupManifest(version, backupId, createdAt, source, files));
    }
    [Fact]
    public void Create_ShouldThrowException_WhenSourceIsEmpty()
    {
        string source = "";

        Assert.Throws<ArgumentException>(
            () => new BackupManifest(
                1, Guid.NewGuid(), DateTimeOffset.UtcNow,
                source, new List<BackupFile>()));
    }
    [Fact]
    public void Create_ShouldThrowException_WhenVersionIsNotPositive()
    {
        // Arrange
        int version = 0; // or any negative number
        Guid backupId = Guid.NewGuid();
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        string source = "test/source";
        var files = new List<BackupFile>();
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new BackupManifest(version, backupId, createdAt, source, files));
    }
    [Fact]
    public void Create_ShouldThrowException_WhenFilesContainsNull()
    {
        // Arrange
        int version = 1;
        Guid backupId = Guid.NewGuid();
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        string source = "test/source";
        var files = new List<BackupFile>
        {
            new BackupFile("file1.txt", 1024, "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad"),
            null! // This should trigger the exception
        };
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BackupManifest(version, backupId, createdAt, source, files));
    }
    [Fact]
    public void Create_ShouldThrowException_WhenFilesIsNull()
    {
        // Arrange
        int version = 1;
        Guid backupId = Guid.NewGuid();
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        string source = "test/source";
        List<BackupFile> files = null!; // This should trigger the exception
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new BackupManifest(version, backupId, createdAt, source, files));
    }
    [Fact]
    public void Manifest_ShouldBeImmutable()
    {
        // Arrange
        int version = 1;
        Guid backupId = Guid.NewGuid();
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        string source = "test/source";
        var files = new List<BackupFile>
        {
            new BackupFile("file1.txt", 1024, "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad"),
            new BackupFile("file2.txt", 2048, "b94d27b9934d3e08a52e52d7da7dabfac484efe37a5380ee9088f7ace2efcde9")
        };
        var backupManifest = new BackupManifest(version, backupId, createdAt, source, files);
        // Act
        var retrievedFiles = backupManifest.Files;
        // Assert
        Assert.Throws<NotSupportedException>(() => ((ICollection<BackupFile>)retrievedFiles).Add(new BackupFile("file3.txt", 512, "b94d27b9934d3e08a52e52d7da7dabfac484efe37a5380ee9088f7ace2efcde9")));
    }
}
