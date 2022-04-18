namespace Logic.MappingProfile;
public class PaymentDestinationMappingProfile : Profile
{
    public PaymentDestinationMappingProfile()
    {
        CreateMap<PaymentDestination, PaymentDestinationDto>();
    }
}


