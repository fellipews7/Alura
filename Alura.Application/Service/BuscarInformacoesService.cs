using Alura.Application.Interface;
using Alura.Data.Interface;
using Alura.Domain.Dto;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;

namespace Alura.Application.Service
{
    public class BuscarInformacoesService : IBuscarInformacoesService
    {
        private readonly string _url = "https://www.alura.com.br/busca?query=#SUBSTITUIR#";
        private IWebDriver? _driver;
        private readonly ICursoRepository _cursoRepository;

        public BuscarInformacoesService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public async Task Executar(string termo)
        {
            _driver = new ChromeDriver();

            try
            {
                var cursosResumidos = await BuscarDadosIniciais(termo);

                var cursos = BuscarDadosCompleto(cursosResumidos);

                cursos.ForEach(_cursoRepository.Inserir);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }

        private async Task<List<CursoResumidoDto>> BuscarDadosIniciais(string termo)
        {
            List<CursoResumidoDto> cursos = new();

            try
            {
                _driver.Navigate().GoToUrl(_url.Replace("#SUBSTITUIR#", termo.Replace(" ", "+")));

                var elementos = _driver.FindElements(By.CssSelector("#busca-resultados > ul > li"));

                foreach (var elemento in elementos)
                {
                    var nome = elemento.FindElement(By.ClassName("busca-resultado-nome")).Text;
                    var descricao = elemento.FindElement(By.ClassName("busca-resultado-descricao")).Text;
                    var link = elemento.FindElement(By.ClassName("busca-resultado-link")).GetAttribute("href");

                    if (link.Contains("/artigos/") || link.Contains("/podcast/") || link.Contains("/cursos-online-programacao/"))
                        continue;

                    cursos.Add(new CursoResumidoDto
                    {
                        Nome = nome,
                        Descricao = descricao,
                        Url = link
                    });
                }

                return cursos;
            }
            catch
            {
                throw;
            }
        }

        private List<CursoDto> BuscarDadosCompleto(List<CursoResumidoDto> cursos)
        {
            List<CursoDto> response = new();
            
            foreach (var curso in cursos)
            {
                try
                {
                    _driver.Navigate().GoToUrl(curso.Url);

                    for(var i = 0; i < 3 && _driver.FindElements(By.Id("login-email")).Count > 0; i++)
                    {
                        _driver.Navigate().Refresh();
                    }

                    if (_driver.FindElements(By.Id("login-email")).Count > 0)
                        throw new Exception($"Dados não encontrados para o curso {curso.Nome}");

                    var nomeInstrutor = string.Empty;
                    var carga = string.Empty;

                    if (_driver.FindElements(By.ClassName("instructor-title--name")).Count > 0)
                    {
                        nomeInstrutor = string.Join(",", _driver.FindElements(By.ClassName("instructor-title--name")).Select(x => x.GetAttribute("innerText")).Distinct());
                        carga = _driver.FindElement(By.CssSelector("body > section.course-header__wrapper > div > div.course-container-flex--desktop > div.course-container__co-branded_icon > div > div:nth-child(1) > div > p.courseInfo-card-wrapper-infos")).Text;
                    }
                    else if (_driver.FindElements(By.ClassName("formacao-instrutor-nome")).Count > 0)
                    {
                        nomeInstrutor = string.Join(",", _driver.FindElements(By.ClassName("formacao-instrutor-nome")).Select(x => x.GetAttribute("innerText")).Distinct());
                        carga = _driver.FindElement(By.CssSelector("body > main > section.formacao-container-color > div > div.formacao__info-conclusao > div.formacao__info-content > div")).Text;
                    }

                    response.Add(new CursoDto(curso,
                        nomeInstrutor,
                        int.Parse(Regex.Matches(carga, @"\d+").FirstOrDefault()?.Value ?? throw new Exception($"Dados não encontrados para o curso {curso.Nome}"))));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return response;
        }
    }
}