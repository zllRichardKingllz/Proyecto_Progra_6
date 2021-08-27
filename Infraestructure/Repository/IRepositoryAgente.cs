﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryAgente
    {
        IEnumerable<Agente> GetAgentes();
        Agente GetAgentesByID(int id);
        Agente guardarAgente(Agente agente);
    }
}
