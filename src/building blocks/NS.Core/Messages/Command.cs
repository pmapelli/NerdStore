using MediatR;
using FluentValidation.Results;

namespace NS.Core.Messages;

public abstract class Command : Message, IRequest<ValidationResult>
{
    public DateTime Timestamp { get; }
    public ValidationResult ValidationResult { get; set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
    }

    public virtual bool EhValido()
    {
        throw new NotImplementedException();
    }
}