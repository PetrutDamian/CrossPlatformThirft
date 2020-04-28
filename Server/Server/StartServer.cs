using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using services;
using Thrift.Transport;
using Thrift.Protocol;
using Thrift.Server;
using Thrift;

namespace server
{
    class StartServer
    {
        public static void Main(string[] args)
        {
            Service serv = getService();
            ThriftService.Processor processor = new ThriftService.Processor(serv);
            TServerTransport st = new TServerSocket(9090);
            TServer server = new TThreadedServer(processor, st);
            Console.WriteLine("Starting server : listening on port 9090 ....");
            server.Serve();
        }
        private static Service getService()
        {
            return new Service(new UserDbRepo(), new CursaDbRepo(), new RezervareDbRepo());
        }
    }
}
