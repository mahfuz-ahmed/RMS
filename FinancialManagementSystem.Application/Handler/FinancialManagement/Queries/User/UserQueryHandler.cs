using ErrorOr;
using FinancialManagementSystem.Core;
using MediatR;
namespace FinancialManagementSystem.Application
{
    public class UserGetDataQueryHandler : IRequestHandler<UserGetDataQuery,ErrorOr<User>>
    {
        public readonly IUserRepository _userRepository;

        public UserGetDataQueryHandler(IUserRepository userRepository)  
        {
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<User>> Handle(UserGetDataQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.UserGetDataAsync(query.id);
            if (user == null) 
            {
                //throw new NotFoundException("User not found");
            }

            //var dto = _mapper.Map<QueryUserDto>(user);
            return user;
        }
    }

    public class UserGetAllDataQueryHandler : IRequestHandler<UserGetAllDataQuery, ErrorOr<IEnumerable<User>>>
    {
        public readonly IUserRepository _userRepository;

        public UserGetAllDataQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<IEnumerable<User>>> Handle(UserGetAllDataQuery query, CancellationToken cancellationToken)
        {

            var users = await _userRepository.UserGetAllDataAsync();

            //var usersMapping = _mapper.Map<IEnumerable<QueryUserDto>>(users);

            //if (!usersMapping.Any())
            //{
            //    throw new NotFoundException("Users not found");
            //}

            //return ErrorOrFactory.From(usersMapping);
            return users.ToList();
        }
    }

}
