using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
namespace services
{
    public interface IServices
    {
        void login(User user, int port);
        void logout(int port);
        List<Cursa> getAllCurse();
        Cursa findByDestinationAndDate(string destination, DateTime date);
        List<Rezervare> getAllBookings(int id);
        void makeBooking(List<Rezervare> rezervari);
    }
}
