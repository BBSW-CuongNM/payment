namespace Data.Configurations;

public class PaymentDestinationConfiguration : IEntityTypeConfiguration<PaymentDestination>
{
    public void Configure(EntityTypeBuilder<PaymentDestination> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
