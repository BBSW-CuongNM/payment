namespace Logic.Validators;
public class CreatePaymentDestinationValidator : AbstractValidator<CreatePaymentDestinationCommand>
{
    public CreatePaymentDestinationValidator()
    {
        RuleFor(x => x.ExternalId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Image).NotEmpty();
        RuleFor(x => x.SortIndex).NotEmpty();
    }
}
