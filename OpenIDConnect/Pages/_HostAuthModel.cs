using Microsoft.AspNetCore.Authentication; //
using Microsoft.AspNetCore.Mvc; //
using Microsoft.AspNetCore.Mvc.RazorPages;//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenIDConnect.Pages
{
    public class _HostAuthModel : PageModel
    {
        public IActionResult OnGetLogin()
        {
            return Challenge(AuthProps(), "oidc");
        }

        public async Task OnGetLogout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc", AuthProps());
        }

        private AuthenticationProperties AuthProps()
            => new AuthenticationProperties
            {
                RedirectUri = Url.Content("~/")
            };
    }
}
