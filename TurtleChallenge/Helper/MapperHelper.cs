using AutoMapper;
using TurtleChallenge.Assets.Boards;
using TurtleChallenge.Assets.Implementation.Settings;

namespace TurtleChallenge.Helper
{
    public static class MapperHelper
    {
        private static IMapper mapper = null;

        public static IMapper Mapper
        {
            get
            {
                if (mapper == null)
                {
                    var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<BoardSettings, DefaultBoard>());
                    mapper = mapperConfiguration.CreateMapper();
                }
                return mapper;
            }
        }
    }
}
