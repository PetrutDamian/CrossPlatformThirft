using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Repositories;
using services;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport;

namespace server
{
    public class Service:IServices,ThriftService.ISync
    {
        private IUserRepo userRepo;
        private ICursaRepo cursaRepo;
        private IRezervareRepo rezervareRepo;
        private ConcurrentDictionary<int,ThriftObserver.Client> observers = new ConcurrentDictionary<int,ThriftObserver.Client>();

        public Service(IUserRepo userRepo, ICursaRepo cursaRepo, IRezervareRepo rezervareRepo)
        {
            this.userRepo = userRepo;
            this.cursaRepo = cursaRepo;
            this.rezervareRepo = rezervareRepo;
        }
        public void makeBooking(List<Domain.Rezervare> rezervari)
        {
            Domain.Cursa cursa = cursaRepo.FindOne(rezervari[0].idCursa);
            cursa.locuriDisponibile -= rezervari.Count;
            cursaRepo.Update(cursa);
            foreach(var rezervare in rezervari)
                rezervareRepo.Save(rezervare);
            notify(cursa, rezervari);
        }

        private void notify(Domain.Cursa cursa, List<Domain.Rezervare> rezervari)
        {

            foreach (var obs in observers.Values)
            {
                Task.Run(() =>
                {
                    obs.seatsDecremented(ThriftUtils.fromNativeCursa(cursa));
                    obs.newBookings(ThriftUtils.fromNativeRezervari(rezervari));
                });
            }
        }

        public Domain.Cursa findByDestinationAndDate(String destination, DateTime date)
        {
            Console.WriteLine("Enter service findByDestinationAndDate");
            var curse = cursaRepo.FindByDestinationAndDate(destination, date);
            if (curse.ToList<Domain.Cursa>().Count == 0)
                throw new ServiceException("No transport found!");
            return cursaRepo.FindByDestinationAndDate(destination, date).ElementAt(0);
        }
        public List<Domain.Rezervare> getAllBookings(int id)
        {
            return rezervareRepo.FindByIdCursa(id).ToList<Domain.Rezervare>();
        }
        public List<Domain.Cursa> getAllCurse()
        {
            return cursaRepo.FindAll().ToList<Domain.Cursa>();
        }

        public Domain.User getUserById(String id)
        {
            return userRepo.FindOne(id);
        }

        public void login(Domain.User user, int port)
        { 
        }

        public void logout(int port)
        {
            ThriftObserver.Client x;
            observers.TryRemove(port, out x);
        }

        public bool login(Thrift.User user, int port)
        {
            Domain.User usr = userRepo.FindOne(user.Id);
            if (usr == null || !usr.password.Equals(user.Password))
                return false;
            TTransport transport = new TSocket("localhost", port);
            transport.Open();
            TProtocol protocol = new TBinaryProtocol(transport);
            ThriftObserver.Client client = new ThriftObserver.Client(protocol);
            observers.TryAdd(port, client);
            return true;
        }

        List<Thrift.Cursa> ThriftService.ISync.getAllCurse()
        {

            List<Domain.Cursa> all = cursaRepo.FindAll().ToList<Domain.Cursa>();
            List<Thrift.Cursa> result = new List<Thrift.Cursa>();
            all.ForEach(x =>
            {
                result.Add(ThriftUtils.fromNativeCursa(x));
            });
            return result;
        }

        public Thrift.Cursa findByDestinationAndDate(string destination, string date)
        {

            DateTime d;
            DateTime.TryParse(date, out d);
            var curse = cursaRepo.FindByDestinationAndDate(destination, d);
            if (curse.ToList<Domain.Cursa>().Count == 0)
                return null;
            return ThriftUtils.fromNativeCursa(cursaRepo.FindByDestinationAndDate(destination, d).ElementAt(0));
        }

        List<Thrift.Rezervare> ThriftService.ISync.getAllBookings(int id)
        {
            List<Domain.Rezervare> all = rezervareRepo.FindByIdCursa(id).ToList<Domain.Rezervare>();
            List<Thrift.Rezervare> result = new List<Thrift.Rezervare>();
            all.ForEach(x =>
            {
                Thrift.Rezervare r = new Thrift.Rezervare();
                r.Client = x.client;
                r.Id = x.id;
                r.IdCursa = x.idCursa;
                r.NrLoc = x.nrLoc;
                result.Add(r);
            });
            return result;
        }

        public void makeBooking(List<Thrift.Rezervare> rezervari)
        {
            List<Domain.Rezervare> all = new List<Domain.Rezervare>();
            rezervari.ForEach(x =>
            {
                all.Add(new Domain.Rezervare(x.Id, x.IdCursa, x.NrLoc, x.Client));
            });
            makeBooking(all);
        }
    }
}
