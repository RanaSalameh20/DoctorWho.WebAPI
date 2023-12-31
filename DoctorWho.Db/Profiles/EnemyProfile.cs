﻿using AutoMapper;
using DoctorWho.Db.Entities;
using DoctorWho.Web.Models;

namespace DoctorWho.Web.Profiles
{
    public class EnemyProfile : Profile
    {
        public EnemyProfile()
        {
            CreateMap<Enemy, EnemyDto>();
            CreateMap<EnemyDto, Enemy>();
        }
    }
}
