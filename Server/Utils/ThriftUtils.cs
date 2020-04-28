using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
namespace Thrift
{
    public class ThriftUtils
    {
        public static Domain.Cursa toModelCursa(Cursa cursa)
        {
            DateTime date;
            DateTime.TryParse(cursa.Date, out date);
            return new Domain.Cursa(cursa.Id, cursa.Destinatie, date, cursa.LocuriDisponibile);
        }
        public static Cursa fromNativeCursa(Domain.Cursa cursa)
        {
            Cursa c = new Cursa();
            c.Id = cursa.id;
            c.LocuriDisponibile = cursa.locuriDisponibile;
            c.Destinatie = cursa.destinatie;
            c.Date = cursa.date.ToString("dd-MM-yyyy HH:mm");
            return c;
        }
        public static List<Rezervare> fromNativeRezervari(List<Domain.Rezervare> rezervari)
        {
            var all = new List<Rezervare>();
            rezervari.ForEach(x =>
            {
                var r = new Rezervare();
                r.Id = x.id;
                r.Client = x.client;
                r.IdCursa = x.idCursa;
                r.NrLoc = x.nrLoc;
                all.Add(r);
            });
            return all;
        }
    }
}
