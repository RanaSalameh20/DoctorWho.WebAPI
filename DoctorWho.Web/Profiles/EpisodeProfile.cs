using AutoMapper;
using DoctorWho.Db;
using DoctorWho.Web.Models;

namespace DoctorWho.Web.Profiles
{
    public class EpisodeProfile : Profile
    {
        public EpisodeProfile()
        {
            CreateMap<Episode, EpisodeDto>();
            CreateMap<EpisodeDto, Episode>();
        }
    }
}
