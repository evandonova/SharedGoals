using SharedGoals.Data.Models;
using SharedGoals.Services.Users;
using SharedGoals.Services.GoalWorks;
using SharedGoals.Services.Goals.Models;
using AutoMapper;
using SharedGoals.Services.Comments;
using SharedGoals.Controllers.Models.Goals;

namespace SharedGoals.Controllers.Infrastructure
{
    public class ControllerMappingProfile : Profile
    {
        public ControllerMappingProfile()
        {
            this.CreateMap<GoalDetailsServiceModel, GoalDetailsViewModel>();
            this.CreateMap<GoalDetailsServiceModel, GoalFormModel>();

            this.CreateMap<GoalWorkServiceModel, GoalWorkViewModel>();
        }
    }
}
