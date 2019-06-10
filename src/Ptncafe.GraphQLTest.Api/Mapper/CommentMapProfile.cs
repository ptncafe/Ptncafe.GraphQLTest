using AutoMapper;
using Ptncafe.GraphQLTest.Proxy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ptncafe.GraphQLTest.Api.Mapper
{
    public class CommentMapProfile : Profile
    {
        public CommentMapProfile()
        {
            CreateMap<Comment, Comment>().ForAllMembers(opt => opt.Condition((source, destination, sourceMember, destMember) => IsNullOrDefault(sourceMember) == false ));

        }
        public bool IsNullOrDefault(object obj)
        {
            return obj == null || (obj.GetType() is var type && type.IsValueType && obj.Equals(Activator.CreateInstance(type)));
        }
    }
}
