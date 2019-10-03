using System;

namespace ShadowRunHelper.CharController
{
    public sealed class ShadowRunHelperControllerAttribute : Attribute
    {
        public bool SupportsEdit { get; set; } = true;
    }

}