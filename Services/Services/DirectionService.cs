using AutoMapper;
using Domain.DTOs.DirectionDTOs;
using Domain.Models;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class DirectionService : IDirectionService
    {
        private readonly IMapper _mapper;
        private readonly IDirectionRepository _directionRepository;

        public DirectionService(IMapper mapper, IDirectionRepository directionRepository)
        {
            _mapper = mapper;
            _directionRepository = directionRepository;
        }

        public async Task<ReadDirectionDTO?> Get(int movieId, int participantId, CancellationToken cancellationToken)
        {
            var direction = await _directionRepository.GetComposite(movieId, participantId, cancellationToken);
            return _mapper.Map<ReadDirectionDTO>(direction);
        }

        public IEnumerable<ReadDirectionDTO?> GetDirectionsByMovie(int movieId, CancellationToken cancellationToken)
        {
            var directions =  _directionRepository.GetDirectionsByMovie(movieId, cancellationToken);
            return _mapper.Map<IEnumerable<ReadDirectionDTO>>(directions);
        }

        public IEnumerable<ReadDirectionDTO?> GetDirectionsByParticipant(int participantId, CancellationToken cancellationToken)
        {
            var directions =  _directionRepository.GetDirectionsByParticipant(participantId, cancellationToken);
            return _mapper.Map<IEnumerable<ReadDirectionDTO>>(directions);
        }

        public IEnumerable<ReadDirectionDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<ReadDirectionDTO>>(_directionRepository.GetAll());
        }

        public async Task Insert(CreateDirectionDTO newDirection, CancellationToken cancellationToken)
        {
            var existingDirection = await _directionRepository.GetComposite(newDirection.MovieId, newDirection.ParticipantId, cancellationToken);
            if (existingDirection != null)
            {
                throw new ApplicationException("This direction already exists");
            }
            else
            {
                await _directionRepository.Insert(_mapper.Map<Direction>(newDirection), cancellationToken);
            }
        }

        public async Task Remove(int movieId, int participantId, CancellationToken cancellationToken)
        {
            var direction = await _directionRepository.GetComposite(movieId, participantId, cancellationToken);
            if (direction == null)
            {
                throw new ApplicationException("Direction not found");
            }
            else
            {
                await _directionRepository.Remove(direction, cancellationToken);
            }
        }
    }
}
