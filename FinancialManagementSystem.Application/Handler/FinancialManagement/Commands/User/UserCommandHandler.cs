using ErrorOr;
using FinancialManagementSystem.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinancialManagementSystem.Application
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ErrorOr<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;


        public AddUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<User>> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {

            var exist = await _userRepository.UserGetDataByEmailAsync(command.user.Email);

            if (exist != null)
            {
                //throw new AlreadyExist("Email already exist");
            }

            command.user.PasswordHash = _passwordHasher.HashPassword(command.user, command.user.PasswordHash);

            var addedUser = await _userRepository.AddUserAsync(command.user);


            return addedUser;
        }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<User>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<User>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {

            var userId = command.user.Id;

         
       
            var existingUser = await _userRepository.UserGetDataAsync(command.user.Id);

            if (existingUser == null)
                throw new KeyNotFoundException($"User with ID {command.user.Id} not found.");

            var updatedUser = await _userRepository.UpdateUserAsync(command.user);

            //return _mapper.Map<Users>(updatedUser);
            return updatedUser;
        }
    }

    public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand, ErrorOr<bool>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.UserGetDataAsync(command.userId);
            if(user == null)
            {
                //return Errors.Common.DataNotFound;
            }
            return await _userRepository.DeleteUserAsync(user);
        }
    }
}
