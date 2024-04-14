using DCXAirAPI.Application.DTOs.Currency;
using DCXAirAPI.Application.Interfaces.Currency;
using MediatR;

namespace DCXAirAPI.Application.Cqrs.Currency.Queries
{
    public class GetCurrencyQuery : IRequest<List<CurrencyDTO>>
    {
    }

    public class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery, List<CurrencyDTO>>
    {
        private readonly ICurrencyService _currencyService;
        public GetCurrencyQueryHandler(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public async Task<List<CurrencyDTO>> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
        {
            return await _currencyService.GetAllowedCurrencies();
        }
    }
}
