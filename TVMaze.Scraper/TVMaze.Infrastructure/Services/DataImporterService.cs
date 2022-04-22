using AutoMapper;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TVMaze.Core.Contracts;
using TVMaze.Core.DTO;
using TVMaze.Core.Entities;
using TVMaze.Infrastructure.Services.Clients;


namespace TVMaze.Infrastructure.Services
{
    public class DataImporterService : IDataImporterService
    {
        private readonly ShowsClient _showsClient;
        private readonly CastClient _castClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //private readonly MapperConfiguration _showConfig;
        //private readonly MapperConfiguration _castConfig;
        private int _pageLimit = 1;

        public DataImporterService(ShowsClient showsClient, CastClient castClient,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _showsClient = showsClient;
            _castClient = castClient;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            //_showConfig = new MapperConfiguration(cfg =>
            //    cfg.CreateMap<TVMaze.Core.DTO.Show, TVMaze.Core.Entities.Show>()
            //    .ForMember(dest => dest.Genre, act => act.MapFrom(src => src.Genres.FirstOrDefault()))
            //    .ForMember(dest => dest.Network, act => act.MapFrom(src => src.Network.Name))
            //);

            //_castConfig = new MapperConfiguration(cfg =>
            //    cfg.CreateMap<TVMaze.Core.DTO.Cast, TVMaze.Core.Entities.Cast>()
            //    .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Person.Id))
            //    .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Person.Name))
            //    .ForMember(dest => dest.Url, act => act.MapFrom(src => src.Person.Url))
            //    .ForMember(dest => dest.Birthday, act => act.MapFrom(src => src.Person.Birthday))
            //    .ForMember(dest => dest.Deathday, act => act.MapFrom(src => src.Person.Deathday))
            //    .ForMember(dest => dest.Gender, act => act.MapFrom(src => src.Person.Gender))
            //    .ForMember(dest => dest.Character, act => act.MapFrom(src => src.Character.Name))
            //    .ForMember(dest => dest.CharacterUrl, act => act.MapFrom(src => src.Character.Url))              
            //);

        }

        public async Task Import()
        {
            
            try
            {
                //Get the last page to fetch
                var lastPageFetched = await _unitOfWork.GetLastPage();
                var lastPagetoFetch = lastPageFetched + _pageLimit;


                for (int i = lastPageFetched; i <= lastPagetoFetch; i++)
                {
                    var response = await _showsClient.GetShowsPerPage(i);

                    var shows = MapToEntities(response).Result;

                    //Add shows and casts
                    await _unitOfWork.AddShows(shows);
               
                }

                var lastTransaction = new ImportTransaction()
                {
                    LastRun = DateTime.Now,
                    NextPage = lastPagetoFetch + 1
                };

                //Add transaction and the next page to fetch
                await _unitOfWork.AddTransaction(lastTransaction);

            }
            catch (Exception ex)
            {

                throw ex;
            }     
            
        }

        private async Task<List<TVMaze.Core.Entities.Show>> MapToEntities(IList<Core.DTO.Show> shows)
        {
            var showList = new List<TVMaze.Core.Entities.Show>();
            //var mapper = new Mapper(_showConfig);

            foreach (var show in shows)
            {
                var showEntity = _mapper.Map<TVMaze.Core.Entities.Show>(show);

                var showId = show.Id;

                var response = await _castClient.GetCastByShowId(showId);

                var casts = MapToEntities(showId, response);

                showEntity.Casts = casts;

                showList.Add(showEntity);
            }

            return showList;

        }

        private List<TVMaze.Core.Entities.Cast> MapToEntities(int showId, IList<Core.DTO.Cast> casts)
        {
            var castList = new List<TVMaze.Core.Entities.Cast>();
            //var mapper = new Mapper(_castConfig);

            foreach (var cast in casts)
            {
                var castEntity = _mapper.Map<TVMaze.Core.Entities.Cast>(cast);
                castEntity.ShowId = showId;
                castList.Add(castEntity);
            }

            return castList;
        }
    }
}
