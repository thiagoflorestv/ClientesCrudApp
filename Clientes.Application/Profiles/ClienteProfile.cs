using AutoMapper;
using Clientes.Application.DTOs;
using Clientes.Domain.Entities;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        
        CreateMap<Cliente, ClienteDto>()
            .ForMember(dest => dest.Telefones, opt => opt.MapFrom(src => src.Telefones));

        
        CreateMap<ClienteDto, Cliente>()
            .ForMember(dest => dest.Telefones, opt => opt.MapFrom(src => src.Telefones));

        
        CreateMap<Telefone, TelefoneDto>()
            .ForMember(dest => dest.TipoTelefone, opt => opt.MapFrom(src => src.TipoTelefone));

        
        CreateMap<TelefoneDto, Telefone>()
            .ForMember(dest => dest.TipoTelefone, opt => opt.Ignore());

        
        CreateMap<TipoTelefone, TipoTelefoneDto>().ReverseMap();
    }
}
