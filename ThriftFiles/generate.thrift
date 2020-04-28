namespace java Thrift
namespace csharp Thrift

struct User{
1: string id
2: string password
}

struct Rezervare{
1: i32 id
2: i32 idCursa
3: i32 nrLoc
4: string client
}



struct Cursa{
1:i32 id
2: string destinatie
3: string date
4: i32 locuriDisponibile
}
service ThriftService
    {
        bool login(1:User user, 2:i32 port);
        void logout(1:i32 port);
        list<Cursa> getAllCurse();
        Cursa findByDestinationAndDate(1:string destination,2: string date);
        list<Rezervare> getAllBookings(1:i32 id);
        void makeBooking(1:list<Rezervare> rezervari);
    }

service ThriftObserver{
    void seatsDecremented(1:Cursa cursa)
    void newBookings(1:list<Rezervare> rezervari)
}


