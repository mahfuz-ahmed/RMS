using AutoMapper;
using ErrorOr;
using FinancialManagementSystem.Core;
using MediatR;

namespace FinancialManagementSystem.Application
{
    public class AddChartOfAccountCommandHandler: IRequestHandler<AddChartOfAccountCommand, ErrorOr<ChartOfAccountReadDto>>
    {
        private readonly IChartOfAccountRepository _chartOfAccountRepository;
        private readonly IMapper _mapper;

        public AddChartOfAccountCommandHandler(IChartOfAccountRepository chartOfAccountRepository,IMapper mapper)
        {
            _chartOfAccountRepository = chartOfAccountRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<ChartOfAccountReadDto>> Handle(AddChartOfAccountCommand command, CancellationToken cancellationToken)
        {
            // You may check if AccountCode already exists before adding

            var exists = await _chartOfAccountRepository.ExistsByAccountCodeAsync(command.chartOfAccount.AccountCode);
            if (exists)
            {
                return Error.Conflict(
                    code: "duplicate",
                    description: $"Account code '{command.chartOfAccount.AccountCode}' already exists."
                );
            }

            var entity = _mapper.Map<ChartOfAccount>(command.chartOfAccount);

            var addedEntity = await _chartOfAccountRepository.AddChartOfAccountAsync(entity);

            var dto = _mapper.Map<ChartOfAccountReadDto>(addedEntity);

            return dto;
        }
    }

    public class UpdateChartOfAccountCommandHandler: IRequestHandler<UpdateChartOfAccountCommand, ErrorOr<ChartOfAccountReadDto>>
    {
        private readonly IChartOfAccountRepository _chartOfAccountRepository;
        private readonly IMapper _mapper;

        public UpdateChartOfAccountCommandHandler(IChartOfAccountRepository chartOfAccountRepository, IMapper mapper)
        {
            _chartOfAccountRepository = chartOfAccountRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<ChartOfAccountReadDto>> Handle(UpdateChartOfAccountCommand command, CancellationToken cancellationToken)
        {
            var existing = await _chartOfAccountRepository.ChartOfAccountGetDataAsync(command.chartOfAccount.Id);

            if (existing == null)
                return Error.NotFound(description: $"ChartOfAccount with ID {command.chartOfAccount.Id} not found.");

            _mapper.Map(command.chartOfAccount, existing);

            var updatedEntity = await _chartOfAccountRepository.UpdateChartOfAccountAsync(existing);

            var updatedDto = _mapper.Map<ChartOfAccountReadDto>(updatedEntity);

            return updatedDto;
        }
    }

    public class DeleteChartOfAccountCommandHandler
        : IRequestHandler<DeleteChartOfAccountCommand, ErrorOr<bool>>
    {
        private readonly IChartOfAccountRepository _chartOfAccountRepository;

        public DeleteChartOfAccountCommandHandler(IChartOfAccountRepository chartOfAccountRepository)
        {
            _chartOfAccountRepository = chartOfAccountRepository;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteChartOfAccountCommand command, CancellationToken cancellationToken)
        {
            var existing = await _chartOfAccountRepository.ChartOfAccountGetDataAsync(command.chartOfAccountId);

            if (existing == null)
                return Error.NotFound(description: $"ChartOfAccount with ID {command.chartOfAccountId} not found.");

            var deleted = await _chartOfAccountRepository.DeleteUserAsync(existing);
            return deleted;
        }
    }
}
