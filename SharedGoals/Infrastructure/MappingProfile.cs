using SharedGoals.Data.Models;
using SharedGoals.Models.Goals;
using SharedGoals.Services.Users;
using SharedGoals.Services.GoalWorks;
using SharedGoals.Services.Goals.Models;
using AutoMapper;

namespace SharedGoals.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Goal, GoalServiceModel>()
                .ForMember(g => g.Creator, cfg => cfg.MapFrom(g => g.Creator.Name))
                .ForMember(g => g.Tag, cfg => cfg.MapFrom(g => g.Tag.Name))
                .ForMember(g => g.DueDate, cfg => cfg.MapFrom(g => g.DueDate.ToString("dd/MM/yyyy")));
            this.CreateMap<Goal, GoalDetailsServiceModel>()
                .ForMember(g => g.Tag, cfg => cfg.MapFrom(g => g.Tag.Name))
                .ForMember(g => g.Creator, cfg => cfg.MapFrom(g => g.Creator.Name))
                .ForMember(g => g.DueDate, cfg => cfg.MapFrom(g => g.DueDate.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(g => g.CreatedOn, cfg => cfg.MapFrom(g => g.CreatedOn.ToString("dd/MM/yyyy HH:mm")));

            this.CreateMap<GoalDetailsServiceModel, GoalDetailsViewModel>();
            this.CreateMap<GoalDetailsServiceModel, GoalFormModel>();

            this.CreateMap<GoalWork, GoalWorkServiceModel>()
                .ForMember(g => g.User, cfg => cfg.MapFrom(g => g.User.UserName))
                .ForMember(g => g.Goal, cfg => cfg.MapFrom(g => g.Goal.Name));
            this.CreateMap<GoalWorkServiceModel, GoalWorkViewModel>();

            this.CreateMap<Tag, GoalTagServiceModel>();

            this.CreateMap<User, UserServiceModel>();
            
        }
    }
}
