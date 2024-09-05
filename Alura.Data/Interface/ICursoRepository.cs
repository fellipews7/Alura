using Alura.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Data.Interface
{
    public interface ICursoRepository
    {
        void Inserir(CursoDto curso);
    }
}
