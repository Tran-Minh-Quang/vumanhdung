using AutoMapper;
using Team12EUP.DTO;
using Team12EUP.Entity;
using static Team12EUP.Controllers.VideoController;

namespace Team12EUP.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account,AccountDTO>().ReverseMap();
            CreateMap<Video, VideoDTO>().ReverseMap();
            CreateMap<Test,TestDTO>().ReverseMap();
            CreateMap<Question,QuestionDTO>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<CreateAdvertísementDTO, Advertisement>().ReverseMap();
        }
    }
}
