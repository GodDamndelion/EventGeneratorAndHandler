namespace EGAH.EventGenerator.Pages;

//using EGAH.EventGenerator.Settings;
using EGAH.Common;
//using EGAH.Services.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

public class IndexModel : PageModel
{
    [BindProperty]
    public bool OpenApiEnabled => true; // settings.Enabled;

    [BindProperty]
    public string Version => Assembly.GetExecutingAssembly().GetAssemblyVersion();

    [BindProperty]
    public string HelloMessage => null; // apiSettings.HelloMessage;


    //private readonly SwaggerSettings settings;
    //private readonly ApiSpecialSettings apiSettings;

    //public IndexModel(SwaggerSettings settings, ApiSpecialSettings apiSettings)
    //{
    //    this.settings = settings;
    //    this.apiSettings = apiSettings;
    //}

    public void OnGet()
    {
    }
}
