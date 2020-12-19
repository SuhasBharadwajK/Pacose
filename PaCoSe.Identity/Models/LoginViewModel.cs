using System.Collections.Generic;
using System.Linq;

namespace PaCoSe.Identity.Models
{
    public class LoginViewModel : LoginInputModel
    {
        public bool AllowRememberLogin { get; set; } = true;

        public bool EnableLocalLogin { get; set; } = true;

        public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();

        public IEnumerable<ExternalProvider> VisibleExternalProviders => this.ExternalProviders.Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));

        public bool IsExternalLoginOnly => !this.EnableLocalLogin && this.ExternalProviders?.Count() == 1;

        public string ExternalLoginScheme => this.IsExternalLoginOnly ? this.ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
    }
}
