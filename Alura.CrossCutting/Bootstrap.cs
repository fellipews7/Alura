using Alura.Application.Interface;
using Alura.Application.Service;
using Alura.Data.Interface;
using Alura.Data.Repository;
using SimpleInjector;

namespace ExtracaoDeInformacoesDoIPTU.Infra.Crosscutting
{
    public class Bootstrap
    {
        public static SimpleInjector.Container container;
        private readonly static Lifestyle lifestyle = Lifestyle.Singleton;
        public static void Start()
        {
            container = new SimpleInjector.Container();

            #region Application Service
            container.Register<IBuscarInformacoesService>(() => new BuscarInformacoesService(new CursoRepository()));
            container.Register<ICursoRepository>(() => new CursoRepository());
            #endregion
            container.Verify();
        }
    }
}
