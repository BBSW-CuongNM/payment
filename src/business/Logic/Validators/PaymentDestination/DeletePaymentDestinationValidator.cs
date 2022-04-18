namespace Logic.Validators;
public class DeletePaymentDestinationValidator : AbstractValidator<DeletePaymentDestinationCommand>
{
    public DeletePaymentDestinationValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
