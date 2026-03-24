namespace Shared;

public abstract class BaseEntity
{
    private readonly List<object> _domainEvents = new();

    public IReadOnlyList<object> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(object eventItem)
    {
        _domainEvents.Add(eventItem);
    }


    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

}
