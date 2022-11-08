using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamNote.Model
{
    public class Settings
    {
        public enum EncoderType
        {
            APP,
            ENTRY,
            NONE
        }

        public EncoderType Encoder { get; set; }

        //public bool HasPassowrd => Encoder != EncoderType.NONE;
        public bool HasPassowrd  {get; set;}
    }
}
