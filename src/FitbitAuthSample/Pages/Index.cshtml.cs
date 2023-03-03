using FitbitAuthSample.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitbitAuthSample.Pages;

public class IndexModel : PageModel
{
    private readonly FitbitClient fitbitClient;

    public IndexModel(
        FitbitClient fitbitClient)
    {
        this.fitbitClient = fitbitClient;
    }

    public ActivityList Activities { get; set; }
        = new();

    public async Task OnGetAsync(
        CancellationToken cancellationToken)
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            Activities
                = await fitbitClient
                    .GetActivityList(
                        cancellationToken);
        }
    }
}
