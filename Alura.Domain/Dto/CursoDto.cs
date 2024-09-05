namespace Alura.Domain.Dto
{
    public record CursoDto
    {
        public string Nome { get; set; }
        public string Professor { get; set; }
        public int Carga { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }

        public CursoDto(CursoResumidoDto resumido, string professor, int carga)
        {
            Nome = resumido.Nome;
            Descricao = resumido.Descricao;
            Url = resumido.Url;
            Professor = professor;
            Carga = carga;
        }
    }

    public record CursoResumidoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }
    }
}
