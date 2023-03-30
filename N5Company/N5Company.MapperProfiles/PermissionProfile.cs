using AutoMapper;
using N5Company.Entities.DTOS;
using N5Company.Entities.Models;
using System;

namespace N5Company.MapperProfiles
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionDTO>();
            CreateMap<PermissionDTO, Permission>();
        }
    }
}
