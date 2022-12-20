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
    public class UserService : AuthenticationService, IAuthenticableEntityService, IUserService
    {

        // TODO IMPLEMENT PASSWORD CHECK

        private readonly IUserRepository _userRepository;

        private readonly ICryptographer _cryptographer;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, ICryptographer cryptographer, IMapper mapper)
        {
            _userRepository = userRepository;
            _cryptographer = cryptographer;
            _mapper = mapper;
        }
        
        public IEnumerable<ReadUserDTO?> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return _mapper.Map<IEnumerable<ReadUserDTO>>(users);
        }

        public async Task<ReadUserDTO?> GetUser(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(id, cancellationToken);
            return _mapper.Map<ReadUserDTO>(user);
        }

        public IEnumerable<ReadUserDTO?> GetUsersByUsername(string username, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUsersByUserName(username, cancellationToken);
            return _mapper.Map<IEnumerable<ReadUserDTO>>(user);
        }

        public async Task InsertUser(CreateUserDTO newUser, CancellationToken cancellationToken)
        {

            var existingUser =  _userRepository.GetUsersByUserName(newUser.Username, cancellationToken).Any();
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
