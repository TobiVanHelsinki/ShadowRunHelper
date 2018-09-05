using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowRunHelper
{
    public interface IInstanceHandling
    {
        string InstanceKey { get; }
        void CreateInstance();
    }
}
