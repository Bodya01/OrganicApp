using Microsoft.AspNetCore.Mvc;
using OrganicApp.Context;

namespace OrganicApp.Controllers.Base
{
    public class OrganicController : Controller
    {
        protected readonly OrganicContext _context;

        public OrganicController(OrganicContext context)
        {
            _context = context;
        }
    }
}