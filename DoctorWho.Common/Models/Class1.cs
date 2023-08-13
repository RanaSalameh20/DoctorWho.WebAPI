using DoctorWho.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWho.Common.Models
{
    public class EpisodeAndCompanionDto
    {
        public EpisodeDto Episode { get; set; }
        public CompanionDto Companion { get; set; }
    }
}
