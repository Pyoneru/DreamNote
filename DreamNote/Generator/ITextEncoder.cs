using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamNote.Generator
{
    public interface ITextEncoder
    {

        string Encode(string content, string password);
        string Decode(string content, string password);
    }
}
