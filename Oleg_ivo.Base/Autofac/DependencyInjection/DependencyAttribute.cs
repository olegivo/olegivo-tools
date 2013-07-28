using System;
using Autofac.Core;

namespace Oleg_ivo.Base.Autofac.DependencyInjection
{

    /// <summary>
    ///  �������, ���������� �������� ��� ��������� �������� �� ����������.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DependencyAttribute: Attribute
    {
        /// <summary>
        ///  ��� ������, ��� ���������� �� �����.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        ///  ����������, ����������� �� ���������� ������� ��������
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        ///  ���� ���������, �� ������� ����� ������������� �������� � ���������� ��������� ��������
        /// </summary>
        /// <remarks>
        ///  ����� ����� ���� ����� ���������� ������ �� �������
        ///  <see cref="AtActivating"/> ���
        ///  <see cref="AtActivated"/>
        /// </remarks>
        public InjectionStages Stage { get; set; }

        /// <summary>
        ///  ����������, ��� �������� ��������������� �� ����� Activating
        /// </summary>
        public bool AtActivating
        {
            get { return Stage == InjectionStages.Activating; }
            set { if (value) Stage = InjectionStages.Activating; }
        }

        /// <summary>
        ///  ����������, ��� �������� ��������������� �� ����� Activated.
        ///  ����� �������������� ��� ���������� ����������� ������.
        /// </summary>
        public bool AtActivated
        {
            get { return Stage == InjectionStages.Activated; }
            set { if (value) Stage = InjectionStages.Activated; }
        }

        /// <summary>
        ///  � ������ ���� ����������� �� ������� � ����������, ������������ ��� �� ���������� ������ ���
        /// </summary>
        public Type DefaultType { get; set; }
    }


    /// <summary>
    ///  ����� ��������� �������, �� ������� ����� ��������������� ��� ��������
    /// </summary>
    public enum InjectionStages
    {
        /// <seealso cref="IComponentRegistration.Activating"/>
        Activating = 0,
        /// <seealso cref="IComponentRegistration.Activated" />
        Activated
    }

}
