using System;
using TronoBackup.Core.Enums;

namespace TronoBackup.Core.Entities;

public class Backup
{
    public Guid Id { get; private set; }
    public string SourceDirectory { get; }
    public string DestinationDirectory { get; }
    public DateTimeOffset CreationDate { get; private set; }
    public BackupStatus Status { get; private set; }
    public Backup(string sourceDirectory, string destinationDirectory)
    {
        if (string.IsNullOrWhiteSpace(sourceDirectory))
            throw new ArgumentException("El directorio de origen no puede estar vacío.", nameof(sourceDirectory));

        if (string.IsNullOrWhiteSpace(destinationDirectory))
            throw new ArgumentException("El directorio de destino no puede estar vacío.", nameof(destinationDirectory));
        Id = Guid.NewGuid();
        SourceDirectory = sourceDirectory;
        DestinationDirectory = destinationDirectory;
        CreationDate = DateTimeOffset.UtcNow;
        Status = BackupStatus.Pending;
    }

    public void Start()
    {
        if (Status != BackupStatus.Pending)
        {
            throw new InvalidOperationException("Solamente se pueden iniciar respaldos pendientes.");
        }
        Status = BackupStatus.InProgress;
    }

    public void Complete()
    {
        if (Status != BackupStatus.InProgress)
        {
            throw new InvalidOperationException("Solamente se pueden completar respaldos en progreso.");
        }
        Status = BackupStatus.Completed;
    }
    public void Fail()
    {
        if (Status != BackupStatus.InProgress)
        {
            throw new InvalidOperationException("Solamente se pueden marcar como fallidos respaldos en progreso.");
        }
        Status = BackupStatus.Failed;
    }
    public void Cancel()
    {
        if (Status != BackupStatus.Pending && Status != BackupStatus.InProgress)
        {
            throw new InvalidOperationException("Solamente se pueden cancelar respaldos pendientes o en progreso.");
        }
        Status = BackupStatus.Cancelled;
    }
}