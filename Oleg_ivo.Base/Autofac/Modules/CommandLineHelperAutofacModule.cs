using System.Collections.Generic;
using Autofac;
using Oleg_ivo.Base.Utils;

namespace Oleg_ivo.Base.Autofac.Modules
{
    /// <summary>
    /// Модуль для поддержки работы с параметрами командной строки 
    /// </summary>
    /// <typeparam name="T">Класс для хранения параметров командной строки</typeparam>
    public sealed class CommandLineHelperAutofacModule<T> : Module
    {
        private readonly IEnumerable<string> args;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        public CommandLineHelperAutofacModule(IEnumerable<string> args)
        {
            this.args = args;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            var commandLineHelper = new CommandLineHelper<T>();
            builder.RegisterInstance(commandLineHelper)
                   .OnActivating(e => e.Instance.InitArgs(args));
            builder.Register(context => context.Resolve<CommandLineHelper<T>>().CommandLineOptions);
        }
    }
}