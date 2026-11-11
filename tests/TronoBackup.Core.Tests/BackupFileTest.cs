using System;
using Xunit;
using TronoBackup.Core.Entities;

namespace TronoBackup.Core.Tests
{
    public class BackupFileTest
    {
        [Fact]
        public void Create_ShouldCreate_ValidBackupFile()
        {
            // Arrange
            string path = "test/file.txt";
            long sizeInBytes = 1024;
            string sha256 = "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad";
            // Act
            BackupFile backupFile = new BackupFile(path, sizeInBytes, sha256);
            // Assert
            Assert.Equal(path, backupFile.RelativePath);
            Assert.Equal(sizeInBytes, backupFile.SizeInBytes);
            Assert.Equal(sha256, backupFile.Sha256);
        }
        [Fact]
        public void Create_ShouldThrowException_WhenSizeIsInvalid()
        {
            // Arrange
            string path = "test/file.txt";
            long sizeInBytes = -1;
            string sha256 = "b94d27b9934d3e08a52e52d7da7dabfac484efe37a5380ee9088f7ace2efcde9";
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new BackupFile(path, sizeInBytes, sha256));
        }
        [Fact]
        public void Create_ShouldThrowException_WhenSha256HasInvalidLength()
        {
            // Arrange
            string relativePath = "test/file.txt";
            long sizeInBytes = 1024;
            string sha256 = "aaaaaaaaaaaaaaaaaa";
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new BackupFile(relativePath, sizeInBytes, sha256));
        }
        [Fact]
        public void Create_ShouldThrowException_WhenHashContainsInvalidCharacters()
        {
            // Arrange
            string relativePath = "test/file.txt";
            long sizeInBytes = 1024;
            string sha256 = "a" + new string('z', 63);
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new BackupFile(relativePath, sizeInBytes, sha256));
        }
    }
}
