using kewlnetcoreapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace kewlnetcoreapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConferenceController : Controller
    {
        public ConferenceController(IConferenceService conferenceService)
        {
            
        }
    }
}