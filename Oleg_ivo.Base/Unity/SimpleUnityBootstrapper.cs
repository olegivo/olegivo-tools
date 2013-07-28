using System;
using System.Globalization;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Oleg_ivo.WAGO.Unity
{
    public abstract class SimpleUnityBootstrapper
    {
        private readonly ILoggerFacade _loggerFacade = new TraceLogger();
        private bool _useDefaultConfiguration = true;

        /// <summary>
        /// Gets the default <see cref="IUnityContainer"/> for the application.
        /// </summary>
        /// <value>The default <see cref="IUnityContainer"/> instance.</value>
        public IUnityContainer Container { get; private set; }

        /// <summary>
        /// Gets the default <see cref="ILoggerFacade"/> for the application.
        /// </summary>
        /// <value>A <see cref="ILoggerFacade"/> instance.</value>
        protected virtual ILoggerFacade LoggerFacade
        {
            get { return _loggerFacade; }
        }

        /// <summary>
        /// Runs the bootstrapper process.
        /// </summary>
        public void Run()
        {
            Run(true);
        }

        /// <summary>
        /// Run the bootstrapper process.
        /// </summary>
        /// <param name="useDefaultConfiguration">If <see langword="true"/>, registers default Composite Application Library services in the container. This is the default behavior.</param>
        public void Run(bool useDefaultConfiguration)
        {
            _useDefaultConfiguration = useDefaultConfiguration;
            ILoggerFacade logger = LoggerFacade;
            if (logger == null)
            {
                throw new InvalidOperationException(/*Resources.NullLoggerFacadeException*/);
            }

            logger.Log("Creating Unity container", Category.Debug, Priority.Low);
            Container = CreateContainer();
            if (Container == null)
            {
                throw new InvalidOperationException(/*Resources.NullUnityContainerException*/);
            }

            logger.Log("Configuring container", Category.Debug, Priority.Low);

            ConfigureContainer();

            logger.Log("Initializing modules", Category.Debug, Priority.Low);
            InitializeModules();

            logger.Log("Bootstrapper sequence completed", Category.Debug, Priority.Low);
        }

        /// <summary>
        /// Configures the <see cref="IUnityContainer"/>. May be overwritten in a derived class to add specific
        /// type mappings required by the application.
        /// </summary>
        protected virtual void ConfigureContainer()
        {
            Container.RegisterInstance<ILoggerFacade>(LoggerFacade);
            Container.RegisterInstance<IUnityContainer>(Container);
            Container.AddNewExtension<UnityBootstrapperExtension>();

            IModuleCatalog moduleCatalog = GetModuleCatalog();
            if (moduleCatalog != null)
            {
                Container.RegisterInstance<IModuleCatalog>(moduleCatalog);
            }
            if (_useDefaultConfiguration)
            {
                //TODO: будет ли работать без этого?//RegisterTypeIfMissing(typeof(IContainerFacade), typeof(UnityContainerAdapter), true);
                RegisterTypeIfMissing(typeof(IServiceLocator), typeof(UnityServiceLocatorAdapter), true);
                RegisterTypeIfMissing(typeof(IModuleInitializer), typeof(ModuleInitializer), true);
                RegisterTypeIfMissing(typeof(IModuleManager), typeof(ModuleManager), true);
                RegisterTypeIfMissing(typeof(IEventAggregator), typeof(EventAggregator), true);
                RegisterTypeIfMissing(typeof(IModuleManager), typeof(ModuleManager), true);
            }
        }

        /// <summary>
        /// Initializes the modules. May be overwritten in a derived class to use custom 
        /// module loading and avoid using an <seealso cref="IModuleManager"/> and 
        /// <seealso cref="IModuleCatalog"/>.
        /// </summary>
        protected virtual void InitializeModules()
        {
            IModuleCatalog moduleCatalog = Container.TryResolve<IModuleCatalog>();
            if (moduleCatalog == null)
            {
                throw new InvalidOperationException(/*Resources.NullModuleCatalogException*/);
            }

            IModuleManager moduleManager = Container.TryResolve<IModuleManager>();
            if (moduleManager == null)
            {
                throw new InvalidOperationException(/*Resources.NullModuleManagerException*/);
            }

            //ModuleInfo[] moduleInfo = moduleCatalog.GetStartupLoadedModules();
            //moduleManager.Initialize(moduleInfo);
            moduleCatalog.Initialize();
            foreach (var moduleInfo in moduleCatalog.Modules)
            {
                moduleManager.LoadModule(moduleInfo.ModuleName);
            }
        }

        /// <summary>
        /// Creates the <see cref="IUnityContainer"/> that will be used as the default container.
        /// </summary>
        /// <returns>A new instance of <see cref="IUnityContainer"/>.</returns>
        protected virtual IUnityContainer CreateContainer()
        {
            return new UnityContainer();
        }

        /// <summary>
        /// Returns the module enumerator that will be used to initialize the modules.
        /// </summary>
        /// <remarks>
        /// When using the default initialization behavior, this method must be overwritten by a derived class.
        /// </remarks>
        /// <returns>An instance of <see cref="IModuleCatalog"/> that will be used to initialize the modules.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        protected virtual IModuleCatalog GetModuleCatalog()
        {
            return null;
        }

        /// <summary>
        /// Registers a type in the container only if that type was not already registered.
        /// </summary>
        /// <param name="fromType">The interface type to register.</param>
        /// <param name="toType">The type implementing the interface.</param>
        /// <param name="registerAsSingleton">Registers the type as a singleton.</param>
        protected void RegisterTypeIfMissing(Type fromType, Type toType, bool registerAsSingleton)
        {
            ILoggerFacade logger = LoggerFacade;

            if (Container.IsTypeRegistered(fromType))
            {
                logger.Log(
                    String.Format(CultureInfo.CurrentCulture,
                                  /*Resources.TypeMappingAlreadyRegistered*/"{0}",
                                  fromType.Name), Category.Debug, Priority.Low);
            }
            else
            {
                if (registerAsSingleton)
                {
                    Container.RegisterType(fromType, toType, new ContainerControlledLifetimeManager());
                }
                else
                {
                    Container.RegisterType(fromType, toType);
                }
            }
        }
    }
}