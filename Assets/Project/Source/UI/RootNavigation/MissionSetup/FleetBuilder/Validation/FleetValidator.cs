using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exa.Ships;
using Exa.Validation;

namespace Exa.UI
{
    public class FleetValidator : Validator<Fleet>
    {
        protected override void AddErrors(ValidationResult errors, Fleet args)
        {
            
        }
    }
}
