using NLog;

namespace Oleg_ivo.Base.WPF.LogBinding
{
    /// <summary>
    /// Interaction logic for DefaultEventViewBox.xaml
    /// </summary>
    public partial class DefaultEventViewBox
    {
        public DefaultEventViewBox()
        {
            InitializeComponent();
        }

        public ObservableLogTarget LogTargetDefaultInstance
        {
            get { return LogManager.Configuration.FindTargetByName("uiLog") as ObservableLogTarget; }
        }

    }
}
