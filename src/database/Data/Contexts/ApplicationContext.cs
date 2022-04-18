namespace Data.Contexts;

public class ApplicationContext : DbContext
{
    public ApplicationContext() { }
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql();

    public virtual DbSet<User> Users => Set<User>();
    public virtual DbSet<UserToken> UserTokens => Set<UserToken>();
    public virtual DbSet<Order> Orders => Set<Order>();
    public virtual DbSet<Otp> Otps => Set<Otp>();
    public virtual DbSet<Partner> Partners => Set<Partner>();
    public virtual DbSet<Payment> Payments => Set<Payment>();
    public virtual DbSet<PaymentDestination> PaymentDestionations => Set<PaymentDestination>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentDestinationConfiguration());
        modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());

        //seed fake data
        new MasterSeeder().Seed(modelBuilder);
    }
}

