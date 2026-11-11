using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TronoBackup.Core.Entities;
using TronoBackup.Core.Enums;

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
            Assert.True(backupFile.IsValid);
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
        public void Create_ShouldThrowException_WhenSha256IsInvalid()
        {
            // Arrange
            string path = "test/file.txt";
            long sizeInBytes = 1024;
            string sha256 = new string('z', 64);
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new BackupFile(path, sizeInBytes, sha256));
        }
        [Fact]
        public void Create_ShouldThrowException_WhenHashContainsInvalidCharacters()
        {
            // Arrange
            string path = "test/file.txt";
            long sizeInBytes = 1024;
            string sha256 = new string('z', 64);
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new BackupFile(path, sizeInBytes, sha256));
        }
    }
}
