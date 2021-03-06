﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
namespace Repositories
{
    public interface IRezervareRepo : ICrudRepository<int, Rezervare>
    {

        IEnumerable<Rezervare> FindByIdCursa(int idCursa);
    }
}
