using AutoMapper;
using Domain.DTOs.UserDTOs;
using Domain.Models;
using Domain.Utils.Cryptography;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly ICryptographer _cryptographer;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, ICryptographer cryptographer, IMapper mapper)
        {
            _userRepository = userRepository;
            _cryptographer = cryptographer;
            _mapper = mapper;
        }
        
        public IEnumerable<ReadUserReferencelessDTO?> GetUsers(
            bool sortedByName = false,
            string? name = null,
            int? pageNumber = null,
            int? pageSize = null
            )
        {
            var users = _userRepository.GetUsers(sortedByName, name, pageNumber, pageSize);
            return _mapper.Map<IEnumerable<ReadUserReferencelessDTO>>(users);
        }

        public async Task<ReadUserDTO?> GetUser(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(id, cancellationToken);
            return _mapper.Map<ReadUserDTO>(user);
        }

        public async Task<ReadUserDTO?> GetUserByName(string username, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUserName(username, cancellationToken);
            return _mapper.Map<ReadUserDTO>(user);
        }

        public async Task InsertUser(CreateUserDTO newUser, CancellationToken cancellationToken)
        {
            
            var existingUser = _userRepository.GetUsers(name: newUser.Username).Any();
            if (existingUser)
            {
                throw new ApplicationException("User already exists");
            }

            var salt = _cryptographer.GenerateSalt();

            var user = new User
            {
                Username = newUser.Username,
                Salt = salt,
                Password = _cryptographer.Hash(newUser.Password, salt),
                IsActive = true
            };

            await _userRepository.Insert(user, cancellationToken);
        }
        
        public async Task RemoveUser(int id, CancellationToken cancellationToken)
        {
            var userToBeDeleted = await _userRepository.Get(id, cancellationToken);
            if (userToBeDeleted == null)
            {
                throw new ApplicationException("User not found");
            }
            await _userRepository.Remove(userToBeDeleted, cancellationToken);
        }

        public async Task ToggleUserActivation(int id, CancellationToken cancellationToken)
        {
            var userToBeToggled = await _userRepository.Get(id, cancellationToken);
            if (userToBeToggled == null)
            {
                throw new ApplicationException("User not found");
            }
            userToBeToggled.IsActive = !userToBeToggled.IsActive;
            await _userRepository.Update(userToBeToggled, cancellationToken);
        }

        public async Task UpdateUser(int id, UpdateUserDTO updatedUser, CancellationToken cancellationToken)
        {
            var userToBeUpdated = await _userRepository.Get(id, cancellationToken);
            if (userToBeUpdated == null)
            {
                throw new ApplicationException("User not found");
            }
            var user = _mapper.Map(updatedUser, userToBeUpdated);
            await _userRepository.Update(user, cancellationToken);
        }
    }
}
