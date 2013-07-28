using System;
using Autofac.Core;

namespace Oleg_ivo.Base.Autofac.DependencyInjection
{

    /// <summary>
    ///  Атрибут, помечающий свойство для установки значения из контейнера.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DependencyAttribute: Attribute
    {
        /// <summary>
        ///  Имя службы, для разрешения по имени.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        ///  Показывает, обязательно ли разрешение данного свойства
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        ///  Этап активации, на котором нужно устанавливать значение в помеченное атрибутом свойство
        /// </summary>
        /// <remarks>
        ///  Также может быть задан установкой одного из свойств
        ///  <see cref="AtActivating"/> или
        ///  <see cref="AtActivated"/>
        /// </remarks>
        public InjectionStages Stage { get; set; }

        /// <summary>
        ///  Показывает, что свойство устанавливается на этапе Activating
        /// </summary>
        public bool AtActivating
        {
            get { return Stage == InjectionStages.Activating; }
            set { if (value) Stage = InjectionStages.Activating; }
        }

        /// <summary>
        ///  Показывает, что свойство устанавливается на этапе Activated.
        ///  Может использоваться для разрешения циклических ссылок.
        /// </summary>
        public bool AtActivated
        {
            get { return Stage == InjectionStages.Activated; }
            set { if (value) Stage = InjectionStages.Activated; }
        }

        /// <summary>
        ///  В случае если зависимость не найдена в контейнере, использовать для ее разрешения данный тип
        /// </summary>
        public Type DefaultType { get; set; }
    }


    /// <summary>
    ///  Этапы активации объекта, на которых могут устанавливаться его свойства
    /// </summary>
    public enum InjectionStages
    {
        /// <seealso cref="IComponentRegistration.Activating"/>
        Activating = 0,
        /// <seealso cref="IComponentRegistration.Activated" />
        Activated
    }

}
