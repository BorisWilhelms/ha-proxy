using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ha_proxy.Pages;

public class Index : PageModel
{
    private readonly HomeAssistant _homeAssistant;
    private readonly string[] _automations;

    [BindProperty(SupportsGet = true)]
    public string Automation { get; set; }
    public string? Name { get; set; }

    [TempData]
    public bool Run { get; set; }
    
    public Index(HomeAssistant homeAssistant, IOptions<HomeAssistantOptions> options)
    {
        _homeAssistant = homeAssistant;
        _automations = options.Value.Automations;
    }

    public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        if (string.IsNullOrWhiteSpace(Automation) || !_automations.Contains(Automation))
        {
            context.Result = BadRequest();
        }
    }

    public async Task<IActionResult> OnGet()
    {
        var entity = await _homeAssistant.Get(Automation);
        Name = entity?.Attributes["friendly_name"]?.ToString();

        return Page();
    }

    public async Task<IActionResult> OnPostRun()
    {
        var success = await _homeAssistant.Service("automation", "trigger", new{ entity_id = Automation });
        Run = true;
        return RedirectToPage();
    }
}