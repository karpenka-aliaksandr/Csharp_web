using System;
using AutoMapper;
using ProductInStorageApp.Dto;
using ProductInStorageApp.Model;

public class MappingProfile:Profile
{
	public MappingProfile()
	{
        CreateMap<ProductInStorageDto, ProductInStorage>().ReverseMap();
    }
}


