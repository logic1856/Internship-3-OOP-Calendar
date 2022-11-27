using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using applikacija1.classes;
namespace applikacija1
{
    internal class Program
    {
        public static List<Event> eventi = new List<Event>();
        public static List<osoba> Osobe = new List<osoba>();
        
        static float enddate(int k)
        {
            
            return (float)(eventi[k].Datumkraja - DateTime.Now).TotalDays;
        }
        public static float zakolikopocinje(int k)
        {
            return (float)(eventi[k].Datumpocetka - DateTime.Now).TotalDays;
        }
        public static void aktivni()
        {
            Console.Clear();
            int p = 0;
            for(int k=0;k<eventi.Count();k++)
            {
                if (eventi[k].jelipoceo()==true && !eventi[k].jelizavrsio()==true)
                {
                    p = 1;
                    Console.WriteLine(eventi[k].Id + "\n" + eventi[k].Naziv + "-" + eventi[k].Lokacija + "-" + enddate(k) + "\n\n\n");
                }
            }
            if (p == 1)
            {
                Console.WriteLine("Unesite id eventa");
                var x = Guid.Parse(Console.ReadLine());
                Console.WriteLine("unesite emailove osoba koje nisu prisustvovali");
                var y = Console.ReadLine();
                var popisodsutnih = new List<string>(y.Split(' '));
                foreach (var k in Osobe)
                {
                    for (int i = 0; i < popisodsutnih.Count; i++)
                    {
                        if (popisodsutnih[i] == k.email)
                        {
                            k.prisutnost[x] = false;
                        }
                    }
                }
            }
            Console.WriteLine("1-izlaz na pocetni izbornik");
            var c = Console.ReadLine();
            if (c == "1")
            {
                pocetniIzbornik();
            }

        }
        public static float trajanje(int k)
        {
            return (float)(eventi[k].Datumkraja - eventi[k].Datumpocetka).TotalHours;
        }
        public static void izbrisievent()
        {
            Console.WriteLine("unesi id eventa kojeg zelis obrisati:");
            var x=Guid.Parse(Console.ReadLine());
            foreach(var k in Osobe)
            {
                k.prisutnost[x] = false;
            }
            for(int i=0;i<eventi.Count; i++)
            {
                if (x == eventi[i].Id)
                {
                    eventi.Remove(eventi[i]);
                }
            }
            Console.WriteLine("1-izlaz na pocetni izbornik");
            var c = Console.ReadLine();
            if (c == "1")
            {
                pocetniIzbornik();
            }

        }
        public static void ukloni()
        {
            Console.WriteLine("unesi id eventa sa kojeg zelis ukloniti osobe");
            var s=Guid.Parse(Console.ReadLine());
            Console.WriteLine("unesi mailove osoba koje nisu vise sudionici");
            var x = Console.ReadLine();
            var y = new List<string>(x.Split(' '));
            int p = 0;
            foreach(var k in eventi)
            {
                if (k.Id == s)
                {
                   
                    for(int i = 0; i < y.Count; i++)
                    {
                        if (k.emails.Contains(y[i]) == false)
                        {
                            p = 1;
                        }
                        
                        else {
                            k.emails.Remove(y[i]);
                        }
                    }
                }
            }
            if (p == 1)
            {
                Console.WriteLine("unijeli ste neke osobe koje nisu sudionici eventa");
            }
            foreach(var k in Osobe) 
            {
                if (k.prisutnost[s] == true && y.Contains(k.email)==true)
                {
                    k.prisutnost[s] = false;
                }
            }
            y.Clear();
            Console.WriteLine("1-izlaz na pocetni izbornik");
            var c = Console.ReadLine();
            if (c == "1")
            {
                pocetniIzbornik();
            }
        }
        public static void nadolazeci()
        {
            Console.Clear();
            int p = 0;
            for(int k = 0; k < eventi.Count(); k++)
            {

                if (!eventi[k].jelipoceo() == true)
                {
                    p = 1;
                    Console.WriteLine(eventi[k].Id + "\n" + eventi[k].Naziv + "-" + eventi[k].Lokacija + "-" + zakolikopocinje(k) +"----"+ trajanje(k)+"\n");
                    for(int i = 0; i < Osobe.Count; i++)
                    {
                        if (Osobe[i].prisutnost[eventi[k].Id] == true)
                        {
                            Console.Write(Osobe[i].ime + " " + Osobe[i].prezime + "\t");
                        }
                    }
                    Console.WriteLine("\n\n");
                }
            }
            Console.WriteLine("1-izbrisi event\n2-ukloni osobe s eventa\n3-povratak na glavni meni");
            var x = Console.ReadLine();
            switch (x)
            {
                case "1":
                    izbrisievent();
                    break;
                case "2":
                    ukloni();
                    break;
                case"3":
                    pocetniIzbornik();
                    break;

            }
            
            
        }
        public static float zavrsio(int k)
        {
            float x = (float)(DateTime.Now - eventi[k].Datumkraja).TotalDays*100;
            int y = (int)x;
            float z=(float) y;
            return z/100;
        }
        public static void eventikojisuzavrsili()
        {
            Console.Clear();
            for (int k = 0; k < eventi.Count(); k++)
            {

                if (eventi[k].jelizavrsio() == true)
                {
                    
                    Console.WriteLine(eventi[k].Id + "\n" + eventi[k].Naziv + "-" + eventi[k].Lokacija + "-" + zavrsio(k) + "----" + trajanje(k) + "\n\n\n");
                    Console.WriteLine("popis prisutnih:");
                    foreach (var z in Osobe)
                    {
                        if (z.prisutnost[eventi[k].Id] == true)
                        {
                            Console.WriteLine(z.ime + " " + z.prezime);
                        }
                    }
                    Console.WriteLine("popis neprisutnih:");
                    foreach (var z in Osobe)
                    {
                        if (z.prisutnost[eventi[k].Id] == false)
                        {
                            Console.WriteLine(z.ime + " " + z.prezime);
                        }
                    }
                    Console.WriteLine("\n\n");
                }
            }
            Console.WriteLine("1-izlaz na pocetni izbornik");
            var c = Console.ReadLine();
            if (c == "1")
            {
                pocetniIzbornik();
            }

        }
        public static void kreirajevent()
        {
            Console.Clear();
            Console.WriteLine("unesi naziv,lokaciju,datum pocetka,datum kraja te emailove pozvanih osoba");
            var x = new Event();
            
            x.Naziv = Console.ReadLine();
            x.Lokacija = Console.ReadLine();
            x.Datumpocetka = DateTime.Parse(Console.ReadLine());
            x.Datumkraja = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("unesite mailove osoba koje zelite dodat u event");
            var y = Console.ReadLine();
            var e=new List<string>(y.Split(' '));
            int p = 0;
            for (int i = 0; i < e.Count; i++)
            {
                foreach(var k in eventi)
                {
                    for(int j = 0; j < k.emails.Count; j++)
                    {
                        if(k.emails[j] == e[i] && ((k.Datumpocetka<x.Datumpocetka && k.Datumkraja>x.Datumpocetka) || (k.Datumpocetka < x.Datumkraja && k.Datumkraja > x.Datumkraja)))
                        {
                            p = 1;
                            e.Remove(e[i]);
                            
                        }
                    }
                }
            }
            if (p == 1)
            {
                Console.WriteLine("unijeli ste neke osobe koje vec imaju event u vremenu novog eventa");

            }
            foreach(var k in Osobe)
            {
                k.prisutnost.Add(x.Id, false);
                for(int i = 0; i < e.Count; i++)
                {
                    if (e[i] == k.email)
                    {
                        k.prisutnost[x.Id] = true;
                    }
                }
            }
            for(int i = 0; i < e.Count; i++)
            {
                x.emails.Add(e[i]);
            }
            e.Clear();
            eventi.Add(x);
            Console.WriteLine("1-izlaz na pocetni izbornik");
            var c = Console.ReadLine();
            if (c == "1")
            {
                pocetniIzbornik();
            }

        }
        public static void exit()
        {

        }
          public static void pocetniIzbornik()
        {
            Console.Clear();
            Console.WriteLine("1-Aktivni eventi\n2-Nadolazeci eventi\n3-Eventi koji su zavrsili\n4-kreiraj event");
            var x=Console.ReadLine();
            switch (x)
            {
                case "1":
                    aktivni();
                    break;
                case "2":
                    nadolazeci();
                    break;
                case "3":
                    eventikojisuzavrsili();
                    break;
                case "4":
                    kreirajevent();
                    break;
                case "5":
                    exit();
                    break;
               
            }
            
        }
         public static void Main(string[] args)
        {
            var x = new Event();
            x.emails.Add("lukapuco@gmail.com");
            x.emails.Add("leopuco@gmail.com");
            x.emails.Add("marin@gmail.com");
            x.emails.Add("jurejuric@gmail.com");
            x.emails.Add("markomarkic@gmail.com");
            x.Naziv = "Hajduk-Dinamo";
            x.Lokacija = "Split";
            x.Datumpocetka = DateTime.Parse("22/10/2022");
            x.Datumkraja = DateTime.Parse("23/10/2022");
            eventi.Add(x);
            var x2 = new Event();
            x2.emails.Add("leopuco@gmail.com");
            x2.emails.Add("marin@gmail.com");
            x2.emails.Add("jurejuric@gmail.com");
            x2.emails.Add("markomarkic@gmail.com");
            x2.Naziv = "HNK predstave";
            x2.Lokacija = "Split";
            x2.Datumpocetka = DateTime.Parse("14/12/2022");
            x2.Datumkraja = DateTime.Parse("15/10/2023");
            eventi.Add(x2);
            var x3 = new Event();
            x3.emails.Add("leonlekic@gmail.com");
            x3.emails.Add("lanamatic@gmail.com");
            x3.emails.Add("markorakic@gmail.com");
            x3.emails.Add("jurejuric@gmail.com");
            x3.emails.Add("markomarkic@gmail.com");
            x3.Naziv = "Hajduk-Dinamo";
            x3.Lokacija = "Split";
            x3.Datumpocetka = DateTime.Parse("22/10/2022");
            x3.Datumkraja = DateTime.Parse("23/10/2022");
            eventi.Add(x3);
            var x4=new Event();
            x4.emails.Add("markorakic@gmail.com");
            x4.emails.Add("lanamatic@gmail.com");
            x4.emails.Add("jurejuric@gmail.com");
            x4.emails.Add("markomarkic@gmail.com");
            x4.Naziv = "Hajduk-Dinamo";
            x4.Lokacija = "Split";
            x4.Datumpocetka = DateTime.Parse("22/10/2022");
            x4.Datumkraja = DateTime.Parse("23/10/2022");
            eventi.Add(x4);
            var x5=new Event();
            x5.emails.Add("lukapuco@gmail.com");
            x5.emails.Add("leopuco@gmail.com");
            x5.emails.Add("marin@gmail.com");
            x5.emails.Add("jurejuric@gmail.com");
            x5.emails.Add("markomarkic@gmail.com");
            x5.Naziv = "Hajduk-Dinamo";
            x5.Lokacija = "Split";
            x5.Datumpocetka = DateTime.Parse("22/10/2022");
            x5.Datumkraja = DateTime.Parse("23/10/2022");
            eventi.Add(x5);
            var y = new osoba();
            y.ime = "Luka";
            y.prezime = "Puco";
            y.email = "lukapuco@gmail.com";
            y.prisutnost.Add(x.Id, true);
            y.prisutnost.Add(x2.Id, true);
            y.prisutnost.Add(x3.Id, true);
            y.prisutnost.Add(x4.Id, false);
            y.prisutnost.Add(x5.Id, true);
            Osobe.Add(y);
            var y2 = new osoba();
            y2.ime = "Leo";
            y2.prezime = "Puco";
            y2.email = "leopuco@gmail.com";
            y2.prisutnost.Add(x.Id, true);
            y2.prisutnost.Add(x2.Id, false);
            y2.prisutnost.Add(x3.Id, false);
            y2.prisutnost.Add(x4.Id, false);
            y2.prisutnost.Add(x5.Id, true);
            Osobe.Add(y2);
            var y3 = new osoba();
            y3.ime = "Marin";
            y3.prezime = "Puco";
            y3.email = "marin@gmail.com";
            y3.prisutnost.Add(x.Id, true);
            y3.prisutnost.Add(x2.Id, true);
            y3.prisutnost.Add(x3.Id, true);
            y3.prisutnost.Add(x4.Id, false);
            y3.prisutnost.Add(x5.Id, true);
            Osobe.Add(y3);
            var y4 = new osoba();
            y4.ime = "Jure";
            y4.prezime = "Juric";
            y4.email = "jurejuric@gmail.com";
            y4.prisutnost.Add(x.Id, true);
            y4.prisutnost.Add(x2.Id, true);
            y4.prisutnost.Add(x3.Id, true);
            y4.prisutnost.Add(x4.Id, true);
            y4.prisutnost.Add(x5.Id, true);
            Osobe.Add(y4);
            var y5 = new osoba();
            y5.ime = "Marko";
            y5.prezime = "Markic";
            y5.email = "markomarkic@gmail.com";
            y5.prisutnost.Add(x.Id, true);
            y5.prisutnost.Add(x2.Id, true);
            y5.prisutnost.Add(x3.Id, true);
            y5.prisutnost.Add(x4.Id, true);
            y5.prisutnost.Add(x5.Id, true);
            Osobe.Add(y5);
            var y6 = new osoba();
            y6.ime = "Lana";
            y6.prezime = "Matic";
            y6.email = "jurejuric@gmail.com";
            y6.prisutnost.Add(x.Id, false);
            y6.prisutnost.Add(x2.Id, false);
            y6.prisutnost.Add(x3.Id, true);
            y6.prisutnost.Add(x4.Id, true);
            y6.prisutnost.Add(x5.Id, false);
            Osobe.Add(y6);
            var y7 = new osoba();
            y7.ime = "Marko";
            y7.prezime = "Rakic";
            y7.email = "markorakic@gmail.com";
            y7.prisutnost.Add(x.Id, false);
            y7.prisutnost.Add(x2.Id, false);
            y7.prisutnost.Add(x3.Id, true);
            y7.prisutnost.Add(x4.Id, false);
            y7.prisutnost.Add(x5.Id, false);
            Osobe.Add(y7);
            pocetniIzbornik();
        }
    }
}
