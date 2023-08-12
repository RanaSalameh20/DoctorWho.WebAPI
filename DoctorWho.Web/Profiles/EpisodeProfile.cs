using AutoMapper;
using DoctorWho.Db.Entities;
using DoctorWho.Db.ViewsModels;
using DoctorWho.Web.Models;

namespace DoctorWho.Web.Profiles
{
    public class EpisodeProfile : Profile
    {
        public EpisodeProfile()
        {
            CreateMap<Episode, EpisodeDto>();
            CreateMap<EpisodeDto, Episode>();
            CreateMap<EpisodeView, EpisodeDto>();
        }
    }
}
