using AutoMapper;
using Domain.DTOs.PerformanceDTOs;
using Domain.Models;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;

namespace Service.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IPerformanceRepository _performanceRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly IMapper _mapper;

        public PerformanceService(IPerformanceRepository performanceRepository, IMovieRepository movieRepository, IParticipantRepository participantRepository, IMapper mapper)
        {
            _performanceRepository = performanceRepository;
            _movieRepository = movieRepository;
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        public async Task<ReadPerformanceDTO?> Get(int movieId, int participantId, CancellationToken cancellationToken)
        {
            var performance = await _performanceRepository.GetComposite(movieId, participantId, cancellationToken);
            return _mapper.Map<ReadPerformanceDTO>(performance);
        }

        public IEnumerable<ReadPerformanceDTO?> GetAll()
        {
            return _mapper.Map<IEnumerable<ReadPerformanceDTO>>(_performanceRepository.GetAll());
        }

        public IEnumerable<ReadPerformanceDTO?> GetByCharacterName(string characterName, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ReadPerformanceDTO>>(_performanceRepository.GetByCharacterName(characterName, cancellationToken));
        }

        public IEnumerable<ReadPerformanceDTO?> GetByMovieId(int movieId, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ReadPerformanceDTO>>(_performanceRepository.GetPerformancesByMovie(movieId, cancellationToken));
        }

        public IEnumerable<ReadPerformanceDTO?> GetByParticipantId(int participantId, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ReadPerformanceDTO>>(_performanceRepository.GetPerformancesByParticipant(participantId, cancellationToken));
        }
        
        public Task Insert(int movieId, int participantId, CreatePerformanceDTO newPerformance, CancellationToken cancellationToken)
        {
            var movie =  _movieRepository.Get(movieId, cancellationToken);
            if (movie == null)
            {
                throw new ApplicationException("Invalid movie ID");
            }
            var participant =  _participantRepository.Get(participantId, cancellationToken);
            if (participant == null)
            {
                throw new ApplicationException("Invalid participant ID");
            }
            newPerformance.MovieId = movieId;
            newPerformance.ParticipantId = participantId;
            return _performanceRepository.Insert(_mapper.Map<Performance>(newPerformance), cancellationToken);
        }

        public Task Update(int movieId, int participantId, UpdatePerformanceDTO performance, CancellationToken cancellationToken)
        {
            var performanceToBeUpdated = _performanceRepository.GetComposite(movieId, participantId, cancellationToken).Result;
            if (performanceToBeUpdated == null)
            {
                throw new ApplicationException("Performance not found");
            }
            else
            {
                performanceToBeUpdated.CharacterName = performance.CharacterName;
                return _performanceRepository.Update(performanceToBeUpdated, cancellationToken);
            }
        }

        public Task Remove(int movieId, int participantId, CancellationToken cancellationToken)
        {
            var performanceToBeDeleted = _performanceRepository.GetComposite(movieId, participantId, cancellationToken).Result;
            if (performanceToBeDeleted == null)
            {
                throw new ApplicationException("Performance not found");
            } 
            else
            {
                return _performanceRepository.Remove(performanceToBeDeleted, cancellationToken);
            }
        }
        
    }
}
