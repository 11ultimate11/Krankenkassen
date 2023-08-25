using Krankenkassen.PlatformInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krankenkassen
{
    public partial class PrintProcessor : IPrintProcessor
    {
        public partial void Print(Stream stream);
        public partial void Print(string path);
    }
}
