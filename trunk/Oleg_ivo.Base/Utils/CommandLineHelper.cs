using System.Collections.Generic;
using System.Linq;
using Args;
using NLog;

namespace Oleg_ivo.Base.Utils
{
    /// <summary>
    /// Класс для работы с параметрами командной строки
    /// </summary>
    /// <typeparam name="TCommandLineOptionsStorage">Класс для хранения параметров командной строки</typeparam>
    public sealed class CommandLineHelper<TCommandLineOptionsStorage>
    {
        private readonly Logger Log = LogManager.GetCurrentClassLogger();
        public IEnumerable<string> Args { get; private set; }
        private readonly IModelBindingDefinition<TCommandLineOptionsStorage> modelBindingDefinition;
        public TCommandLineOptionsStorage CommandLineOptions { get; private set; }

        public CommandLineHelper()
        {
            modelBindingDefinition = Configuration.Configure<TCommandLineOptionsStorage>();
        }

        public void InitArgs(IEnumerable<string> args)
        {
            var list = args as IList<string> ?? args.ToList();
            Log.Debug("{0}: Инициализация хранилища опций командной строки \n(параметры командной строки:'{1}')", typeof(TCommandLineOptionsStorage).Name, string.Join(" ", list));
            Args = list;
            CommandLineOptions = modelBindingDefinition.CreateAndBind(list);
        }
    }
}