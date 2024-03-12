using MediatR;
using PaymentAPI.Data;
using PaymentAPI.DTOs;
using PaymentAPI.Model;
using PaymentAPI.Service.Interfaces;

namespace PaymentAPI.Service.LoginServices.Query
{
    public class GetLoginRequestQuery : IRequest<int>
    {
        public LoginRequest LoginRequest { get; set; }

    }
    public class GetLoginRequestQueryHandler : IRequestHandler<GetLoginRequestQuery, int>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordService _passwordService;
        public GetLoginRequestQueryHandler(IUsersRepository usersRepository, IPasswordService passwordService)
        {
            _usersRepository = usersRepository;
            _passwordService = passwordService;
        }

        public async Task<int> Handle(GetLoginRequestQuery request, CancellationToken cancellationToken)
        {
            var login = request.LoginRequest;
            var users = await _usersRepository.GetAllAsync();
            Users user = users.FirstOrDefault(x => x.TC == login.TC);

            if (user == null || !_passwordService.VerifyPasswordHash(login.Password, user.Password))
            {
                return 0;
            }


            return user.Id;
        }
    }
}
