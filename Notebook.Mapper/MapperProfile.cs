﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Notebook.Common.Dto;
using Notebook.Models;

namespace Notebook.Mapper
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<UserCreateDto, User>().ReverseMap();
			CreateMap<UserDto, User>().ReverseMap();
			CreateMap<User, UserDto>().ReverseMap();
			CreateMap<UserAuthDto, User>().ReverseMap();
			CreateMap<UserCreateDto, CheckUser>().ReverseMap();
			CreateMap<Record, RecordDto>().ReverseMap();

        }
	}
}
