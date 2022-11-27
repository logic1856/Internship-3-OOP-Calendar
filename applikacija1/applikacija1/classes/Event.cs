using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace applikacija1.classes
{
    public class Event
    {
        public Guid Id { get;}
        public string Naziv;
        public string Lokacija;
        public DateTime Datumpocetka;
        public DateTime Datumkraja;
        public List<string> emails { get; private set; }=new List<string>();
        public Event()
        {
            Id = Guid.NewGuid();
        }
        public bool jelipoceo()
        {
            if (Datumpocetka < DateTime.Now)
            {
                return true;
            }
            return false;
        }
        public bool jelizavrsio()
        {
            if (Datumkraja < DateTime.Now)
            {
                return true ;
            }
            return false;
        }
    }
}
