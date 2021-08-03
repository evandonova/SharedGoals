using AutoMapper;
using SharedGoals.Data.Models;
using SharedGoals.Models.Goals;
using SharedGoals.Services.Creators;
using SharedGoals.Services.Goals.Models;
using SharedGoals.Services.GoalWorks;
using SharedGoals.Services.Users;
using System.Collections.Generic;
using System.Linq;

namespace SharedGoals.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<GoalExtendedServiceModel, GoalFormModel>();
            this.CreateMap<GoalExtendedServiceModel, GoalDetailsViewModel>();
            this.CreateMap<Goal, GoalServiceModel>()
                .ForMember(g => g.Tag, cfg => cfg.MapFrom(g => g.Tag.Name))
                .ForMember(g => g.DueDate, cfg => cfg.MapFrom(g => g.DueDate.ToString("dd/MM/yyyy")));
            this.CreateMap<Goal, GoalExtendedServiceModel>()
                .ForMember(g => g.CreatorName, cfg => cfg.MapFrom(g => g.Creator.Name))
                .ForMember(g => g.Tag, cfg => cfg.MapFrom(g => g.Tag.Name))
                .ForMember(g => g.UserId, cfg => cfg.MapFrom(g => g.Creator.UserId));
            this.CreateMap<Tag, GoalTagServiceModel>();
            this.CreateMap<GoalWork, GoalWorkServiceModel>()
                .ForMember(g => g.User, cfg => cfg.MapFrom(g => g.User.UserName))
                .ForMember(g => g.Goal, cfg => cfg.MapFrom(g => g.Goal.Name));
            this.CreateMap<User, UserServiceModel>();
            this.CreateMap<Goal, GoalDetailsServiceModel>()
                .ForMember(g => g.DueDate, cfg => cfg.MapFrom(g => g.DueDate.ToString("dd/MM/yyyy")))
                .ForMember(g => g.Tag, cfg => cfg.MapFrom(g => g.Tag.Name))
                .ForMember(g => g.CreatedOn, cfg => cfg.MapFrom(g => g.CreatedOn.ToString("dd/MM/yyyy hh:mm")));   
        }
    }
}
