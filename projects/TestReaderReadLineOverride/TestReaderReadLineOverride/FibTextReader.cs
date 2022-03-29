using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReaderReadLineOverride
{
    public class FibTextReader : TextReader
    {
        public override string? ReadLine()
        {
            StringBuilder line = new StringBuilder();
            for (; this.Peek() is not '\r';)
                line.Append(this.Peek());

            return line.ToString();
        }
    }
}
