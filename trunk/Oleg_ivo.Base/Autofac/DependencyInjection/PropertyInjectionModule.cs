using Autofac;

namespace Oleg_ivo.Base.Autofac.DependencyInjection
{

    /// <summary>
    ///  ������, ����������� � ��������� Autofac ���������, ��������������� ��������
    ///  ����������� �����������, ���������� ��������� <see cref="DependencyAttribute"/>,
    ///  �� ����� ��������� ���� �����������
    /// </summary>
    /// <remarks>
    ///  ��� ��������� ���������� ���������� ���� ������ ���������� �������� ����� ����������,
    ///  ���������� ��������� <see cref="DependencyAttribute"/>, �� ���� ���� ������� �������
    ///  � ���������� ������������������ ����������-�����������, � ������������� � �������� ���
    ///  ��������� ����������.
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
