using ErrorOr;
using FinancialManagementSystem.Application;
using FinancialManagementSystem.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartOfAccountController : ControllerBase
    {
        private readonly ISender _sender;

        public ChartOfAccountController(ISender sender)
        {
            _sender = sender;
        }

        //[Authorize]
        [HttpPost("AddChartOfAccount")]
        public async Task<IActionResult> AddChartOfAccountAsync([FromBody] ChartOfAccountCreateDto request)
        {
            var command = new AddChartOfAccountCommand(request);

            ErrorOr<ChartOfAccountReadDto> response = await _sender.Send(command);

            return response.Match(
                success => Ok(success),
                error =>
                {
                    var firstError = error.First();
                    return Problem(
                        detail: firstError.Description,
                        title: firstError.Code
                    );
                }
            );
        }

        //[Authorize]
        [HttpPut("UpdateChartOfAccount")]
        public async Task<IActionResult> UpdateChartOfAccountAsync([FromBody] ChartOfAccountUpdateDto request)
        {
            var command = new UpdateChartOfAccountCommand(request);

            ErrorOr<ChartOfAccountReadDto> response = await _sender.Send(command);

            return response.Match(
                success => Ok(success),
                error =>
                {
                    var firstError = error.First();
                    return Problem(
                        detail: firstError.Description,
                        title: firstError.Code
                    );
                }
            );
        }

        //[Authorize]
        [HttpDelete("DeleteChartOfAccount/{coaId}")]
        public async Task<IActionResult> DeleteChartOfAccountAsync([FromRoute] int coaId)
        {
            var command = new DeleteChartOfAccountCommand(coaId);

            ErrorOr<bool> response = await _sender.Send(command);

            return response.Match(
                success => Ok(success),
                error =>
                {
                    var firstError = error.First();
                    return Problem(
                        detail: firstError.Description,
                        title: firstError.Code
                    );
                }
            );
        }

        //[Authorize]
        [HttpGet("GetAllChartOfAccounts")]
        public async Task<IActionResult> GetAllChartOfAccountsAsync()
        {
            ErrorOr<List<ChartOfAccountReadDto>> response = await _sender.Send(new ChartOfAccountGetAllDataQuery());

            return response.Match(
                success => Ok(success),
                error =>
                {
                    var firstError = error.First();
                    return Problem(
                        detail: firstError.Description,
                        title: firstError.Code
                    );
                }
            );
        }

        [HttpGet("GetChartOfAccountById/{coaId}")]
        public async Task<IActionResult> GetChartOfAccountByIdAsync([FromRoute] int coaId)
        {
            ErrorOr<ChartOfAccountReadDto> response = await _sender.Send(new ChartOfAccountGetDataQuery(coaId));

            return response.Match(
                success => Ok(success),
                error =>
                {
                    var firstError = error.First();
                    return Problem(
                        detail: firstError.Description,
                        title: firstError.Code
                    );
                }
            );
        }
    }
}
