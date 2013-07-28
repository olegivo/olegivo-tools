using Autofac;

namespace Oleg_ivo.Base.Autofac.DependencyInjection
{

    /// <summary>
    ///  ћодуль, добавл€ющий в контейнер Autofac поведение, устанавливающее свойства
    ///  экземпл€ров компонентов, помеченные атрибутом <see cref="DependencyAttribute"/>,
    ///  во врем€ активации этих экземпл€ров
    /// </summary>
    /// <remarks>
    ///  ѕри активации экземпл€ра компонента этот модуль определ€ет свойства этого экземпл€ра,
    ///  помеченные атрибутом <see cref="DependencyAttribute"/>, по типу этих свойств находит
    ///  в контейнере зарегистрированные компоненты-зависимости, и устанавливает в свойства эти
    ///  найденные компоненты.
    /// </remarks>
    public class PropertyInjectionModule : ComponentRegistrationEventExtenderModule
    {
        readonly AttributedPropertyInjector injector = new AttributedPropertyInjector();

        internal AttributedPropertyInjector Injector { get { return injector; } }

        public PropertyInjectionModule()
        {

            ActivatingHandler = (s, e) =>
            {
                injector.InjectProperties(e.Context, e.Instance, InjectionStages.Activating);
            };
            ActivatedHandler = (s, e) =>
            {
                injector.InjectProperties(e.Context, e.Instance, InjectionStages.Activated);
            };
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterInstance(this);
        }
    }
}
