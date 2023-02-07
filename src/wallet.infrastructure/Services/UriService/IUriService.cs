using wallet.core.EntitiesFilters;

namespace wallet.infrastructure.Services.UriService;

public interface IUriService
{
    Uri GetPageUri(PaginationFilter filter, string route);
}
