using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace $safeprojectname$.Common;

public class Entity<TId> where TId : struct
{
    private readonly List<INotification> _domainEvents = [];

    protected Entity()
    {
    }

    public TId Id { get; protected set; }

    public int CreatedBy { get; }

    public DateTime CreatedOn { get; }

    public int? ModifiedBy { get; }

    public DateTime? ModifiedOn { get; }

    [NotMapped]
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    public List<INotification> GetDomainEvents()
    {
        return [.. _domainEvents];
    }

    public void AddDomainEvent(INotification domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(INotification domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
