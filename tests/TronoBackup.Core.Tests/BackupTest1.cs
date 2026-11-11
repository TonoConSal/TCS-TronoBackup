namespace TronoBackup.Core.Tests;

using Xunit;
using TronoBackup.Core.Entities;
using TronoBackup.Core.Enums;

public class BackupTests
{
    [Fact]
    public void NewBackup_ShouldHavePendingStatus()
    {
        // Arrange
        Backup backup = new Backup("Source", "Destination");

        // Act
        BackupStatus result = backup.Status;

        // Assert
        Assert.Equal(BackupStatus.Pending, result);
    }

    [Fact]
    public void Start_ShouldChangeStatusToInProgress()
    {
        // Arrange
        Backup backup = new Backup("Source", "Destination");

        // Act
        backup.Start();

        // Assert
        Assert.Equal(BackupStatus.InProgress, backup.Status);
    }

    [Fact]
    public void Complete_ShouldChangeStatusToCompleted()
    {
        // Arrange
        Backup backup = new Backup("Source", "Destination");

        // Act
        backup.Start();
        backup.Complete();

        // Assert
        Assert.Equal(BackupStatus.Completed, backup.Status);
    }

    [Fact]
    public void Complete_WhenPending_ShouldThrowException()
    {
        // Arrange
        Backup backup = new Backup("Source", "Destination");

        // Act + Assert
        Assert.Throws<InvalidOperationException>(
            () => backup.Complete()
        );
    }
    [Fact]
    public void Fail_ShouldChangeStatusToFailed()
    {
        // Arrange
        Backup backup = new Backup("Source", "Destination");
        // Act
        backup.Start();
        backup.Fail();
        // Assert
        Assert.Equal(BackupStatus.Failed, backup.Status);
    }
    [Fact]
    public void Cancel_ShouldChangeStatusToCancelled()
    {
        // Arrange
        Backup backup = new Backup("Source", "Destination");
        // Act
        backup.Start();
        backup.Cancel();
        // Assert
        Assert.Equal(BackupStatus.Cancelled, backup.Status);
    }
    [Fact]
    public void Start_WhenCompleted_ShouldThrowException()
    {
        // Arrange
        Backup backup = new Backup("Source", "Destination");
        backup.Start();
        backup.Complete();
        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => backup.Start());
    }
    [Fact]
    public void Cancel_WhenCompleted_ShouldThrowException()
    {
        // Arrange
        Backup backup = new Backup("Source", "Destination");
        backup.Start();
        backup.Complete();
        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => backup.Cancel());
    }
    [Fact]
    public void Cancel_WhenPending_ShouldChangeStatusToCancelled()
    {
        // Arrange
        Backup backup = new Backup("Source", "Destination");

        // Act
        backup.Cancel();

        // Assert
        Assert.Equal(BackupStatus.Cancelled, backup.Status);
    }
    [Fact]
    public void Fail_WhenPending_ShouldThrowException()
    {
        Backup backup = new Backup("Source", "Destination");

        Assert.Throws<InvalidOperationException>(
            () => backup.Fail()
        );
    }
}
