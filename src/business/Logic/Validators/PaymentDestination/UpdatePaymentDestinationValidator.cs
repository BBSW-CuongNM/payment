namespace Logic.Validators;
public class UpdatePaymentDestinationValidator : AbstractValidator<UpdatePaymentDestinationCommand>
{
    public UpdatePaymentDestinationValidator()
    {
        RuleFor(x => x.ExternalId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Image).NotEmpty();
        RuleFor(x => x.SortIndex).NotEmpty();
    }
}
