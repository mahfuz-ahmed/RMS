using AutoMapper;
using ErrorOr;
using FinancialManagementSystem.Core;
using MediatR;

namespace FinancialManagementSystem.Application.ChartOfAccounts
{
    public class ChartOfAccountGetDataQueryHandler: IRequestHandler<ChartOfAccountGetDataQuery, ErrorOr<ChartOfAccountReadDto>>
    {
        private readonly IChartOfAccountRepository _coaRepository;
        private readonly IMapper _mapper;

        public ChartOfAccountGetDataQueryHandler(IChartOfAccountRepository coaRepository, IMapper mapper)
        {
            _coaRepository = coaRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<ChartOfAccountReadDto>> Handle(ChartOfAccountGetDataQuery query,CancellationToken cancellationToken)
        {
            var coa = await _coaRepository.ChartOfAccountGetDataAsync(query.Id);

            if (coa == null)
            {
                return Error.NotFound(
                    code: "ChartOfAccount.NotFound",
                    description: $"Chart of Account with ID {query.Id} not found.");
            }

            var dto = _mapper.Map<ChartOfAccountReadDto>(coa);
            return dto;
        }
    }

    public class ChartOfAccountGetAllDataQueryHandler: IRequestHandler<ChartOfAccountGetAllDataQuery, ErrorOr<List<ChartOfAccountReadDto>>>
    {
        private readonly IChartOfAccountRepository _coaRepository;
        private readonly IMapper _mapper;

        public ChartOfAccountGetAllDataQueryHandler(IChartOfAccountRepository coaRepository, IMapper mapper)
        {
            _coaRepository = coaRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<ChartOfAccountReadDto>>> Handle(ChartOfAccountGetAllDataQuery query, CancellationToken cancellationToken)
        {
            var coas = await _coaRepository.ChartOfAccountGetAllDataAsync();

            if (!coas.Any())
            {
                return Error.NotFound(
                    code: "ChartOfAccount.Empty",
                    description: "No chart of accounts found."
                );
            }

            var dtos = _mapper.Map<List<ChartOfAccountReadDto>>(coas);
            return dtos;

        }
    }
}
