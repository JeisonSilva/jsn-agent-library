namespace jsn.agent.library.domain.SharedKernel;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}
