using AutoMapper;
using Domain.DTOs.UserDTOs;
using Domain.Models;
using Domain.Utils.Cryptography;
using FluentResults;
using Repository.Repositories.Abstract;
using Service.Services.Abstract;
using Service.Utils.Responses;

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

        public async Task<Result> InsertUser(CreateUserDTO newUser, CancellationToken cancellationToken)
        {

            var existingUser = _userRepository.GetUsers(name: newUser.Username).Any();
            if (existingUser)
            {
                return Result.Fail(new BadRequestError($"There's already an user with the username \"{newUser.Username}\"."));
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
            return Result.Ok();
        }

        public async Task<Result> RemoveUser(int id, CancellationToken cancellationToken)
        {
            var userToBeDeleted = await _userRepository.Get(id, cancellationToken);
            if (userToBeDeleted == null)
            {
                return Result.Fail(new BadRequestError($"The user with Id {id} doesn't exist."));
            }
            await _userRepository.Remove(userToBeDeleted, cancellationToken);
            return Result.Ok();
        }

        public async Task<Result> ToggleUserActivation(int id, CancellationToken cancellationToken)
        {
            var userToBeToggled = await _userRepository.Get(id, cancellationToken);
            if (userToBeToggled == null)
            {
                return Result.Fail(new BadRequestError($"The user with Id {id} doesn't exist."));
            }
            userToBeToggled.IsActive = !userToBeToggled.IsActive;
            await _userRepository.Update(userToBeToggled, cancellationToken);
            return Result.Ok();
        }

        public async Task<Result> UpdateUser(int id, UpdateUserDTO updatedUser, CancellationToken cancellationToken)
        {
            var userToBeUpdated = await _userRepository.Get(id, cancellationToken);
            if (userToBeUpdated is null)
            {
                return Result.Fail(new BadRequestError($"The user with Id {id} doesn't exist."));
            }
            if (userToBeUpdated.Username != updatedUser.Username)
            {
                var existingUser = _userRepository.GetUsers(name: updatedUser.Username).Any();
                if (existingUser)
                {
                    return Result.Fail(new BadRequestError($"There's already an user with the username \"{updatedUser.Username}\"."));
                }
            }
            var user = _mapper.Map(updatedUser, userToBeUpdated);
            await _userRepository.Update(user, cancellationToken);
            return Result.Ok();
        }
    }
}