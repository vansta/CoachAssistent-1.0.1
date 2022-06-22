using AutoMapper;
using CoachAssistent.AutomapperBootstrapper.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.AutomapperBootstrapper
{
    public class MapperService
    {
        public static IEnumerable<Profile> Profiles
        {
            get
            {
                return new[] { new ExerciseProfile() };
            }
        }
    }
}
