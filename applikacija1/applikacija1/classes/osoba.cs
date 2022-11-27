using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace applikacija1.classes
{
    public class osoba
    {
        public string ime;
        public string prezime { get; set; }
        public string email { get; set; }
        public Dictionary<Guid, bool> prisutnost { get; private set; } = new Dictionary<Guid, bool>();
        public osoba()
        {
            
        }

    }
}
