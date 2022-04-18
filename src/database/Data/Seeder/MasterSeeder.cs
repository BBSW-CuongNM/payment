namespace Data.Seeder;

public class MasterSeeder : BaseSeeder
{
    public override void Seed(ModelBuilder modelBuilder)
    {
        new PaymentDestinationSeeder().Seed(modelBuilder);
        new PartnerSeeder().Seed(modelBuilder);
    }
}

