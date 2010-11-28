using System;
using System.Collections.Generic;
using System.Text;

namespace AppInterface
{
    public interface IScript
    {
        void Initialize(IApplication Host);
        void Main(); // The entry of the script.
    }
}
