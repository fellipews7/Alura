using Alura.Data.Interface;
using Alura.Domain.Dto;
using Dapper;

namespace Alura.Data.Repository
{
    public class CursoRepository : ICursoRepository
    {
        private DatabaseContext _dbContext;

        public CursoRepository() 
        { 
            _dbContext = new DatabaseContext();
        }

        public void Inserir(CursoDto curso)
        {
            var sql = "INSERT INTO Cursos (Nome, Professor, Carga, Descricao, Url) VALUES (@Nome, @Professor, @Carga, @Descricao, @Url)";
            using (var connection = _dbContext.CreateConnection())
            {
                connection.Open();
                connection.Execute(sql, curso);
            }
        }
    }
}
