using AutoMapper;
using Common.DataModels.DatabaseModels;
using Common.DataModels.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DataModels.MappingProfiles
{
    public class ClassEventProfile : Profile
    {
        public ClassEventProfile()
        {
            CreateMap<ClassEvent, ClassEventDto>();
        }
    }
}
