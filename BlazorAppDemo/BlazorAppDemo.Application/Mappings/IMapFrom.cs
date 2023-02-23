using AutoMapper;

namespace BlazorAppDemo.Application.Mappings;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(GetType(), GetType());
}