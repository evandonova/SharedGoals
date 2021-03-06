using AutoMapper;
using SharedGoals.Data.Models;
using SharedGoals.Services.Comments;
using SharedGoals.Services.Goals.Models;
using SharedGoals.Services.GoalWorks;
using SharedGoals.Services.Users;

namespace SharedGoals.Services.Infrastructure
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
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

            this.CreateMap<GoalWork, GoalWorkServiceModel>()
                .ForMember(g => g.User, cfg => cfg.MapFrom(g => g.User.UserName))
                .ForMember(g => g.Goal, cfg => cfg.MapFrom(g => g.Goal.Name));

            this.CreateMap<Tag, GoalTagServiceModel>();

            this.CreateMap<User, UserServiceModel>();

            this.CreateMap<Comment, CommentServiceModel>()
                .ForMember(g => g.User, cfg => cfg.MapFrom(g => g.User.UserName))
                .ForMember(g => g.Goal, cfg => cfg.MapFrom(g => g.Goal.Name));
        }
    }
}
