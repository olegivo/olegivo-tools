using System.Windows.Forms;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Oleg_ivo.WAGO.Unity;

namespace Oleg_ivo.PrismExtensions.Unity
{
    public abstract class WinFormsBootstrapper<TForm> : SimpleUnityBootstrapper where TForm : Form
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Shell = Container.Resolve<TForm>();
        }

        protected override IModuleCatalog GetModuleCatalog()
        {
            return new ModuleCatalog();
        }

        public TForm Shell { get; protected set; }

    }
}
