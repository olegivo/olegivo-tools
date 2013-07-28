using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Oleg_ivo.PrismExtensions.Extensions
{
    public static class DataContextExtensions
    {
        public static void RefreshAndNotify(this DataContext dataContext, RefreshMode mode, IEnumerable entities)
        {
            dataContext.Refresh(mode, entities);
            NotifyChanged(entities);
        }

        private static void NotifyChanged(IEnumerable entities)
        {
            var emptyPropName = new object[] { string.Empty };
            foreach (var entity in entities)
            {
                Type type = entity.GetType();
                var method = type.GetMethod("SendPropertyChanged", BindingFlags.Instance | BindingFlags.NonPublic);
                method.Invoke(entity, emptyPropName);
            }
        }



        public static string DumpToString(this IEnumerable<ObjectChangeConflict> objectConflicts)
        {
            return objectConflicts.JoinToString(Environment.NewLine, oc =>
                                                                     string.Join(Environment.NewLine,
                                                                                 new[]
                                                                                 {
                                                                                     string.Format("Сущность: {0}{1}", oc.Object.GetType().Name, (oc.IsDeleted ? ", удалена" : null)),
                                                                                     MemberConflictsToString(oc.MemberConflicts)
                                                                                 }));
        }

        public static string MemberConflictsToString(IEnumerable<MemberChangeConflict> coll)
        {
            return coll.JoinToString(Environment.NewLine, MemberChangeConflictToString);
        }

        public static string MemberChangeConflictToString(MemberChangeConflict mc)
        {
            return string.Format(
                "Член: {0}, current: '{1}', original: '{2}', database: '{3}'",
                mc.Member.Name, mc.CurrentValue, mc.OriginalValue, mc.DatabaseValue);
        }


        public static string DumpToString(this ChangeSet changeSet)
        {
            return new[] {
                            GetEntitiesList(changeSet.Inserts, "Inserts"),
                            GetEntitiesList(changeSet.Updates, "Updates"),
                            GetEntitiesList(changeSet.Deletes, "Deletes")
                         }.ExcludeNull().JoinToString("," + Environment.NewLine);
        }

        private static string GetEntitiesList(ICollection<object> entities, string updateType)
        {
            if (entities == null || entities.Count == 0) return null;
            var sb = new StringBuilder();
            sb.AppendFormat("'{0}': [", updateType);
            sb.AppendLine();

            foreach (var entity in entities)
            {
                sb.AppendFormat("\t{0} {{", entity.GetType().GetShortTypeName());
                var props = entity.GetType().GetProperties()
                        .Where(prop => prop.GetCustomAttributes<ColumnAttribute>(false).Any());

                var strProps = props.JoinToString(", ", p => string.Format("'{0}': {1}", p.Name, FormatValue(p.GetValue(entity, null))));

                sb.Append(strProps);
                sb.AppendLine("}},");
            }
            sb.AppendLine("]");
            return sb.ToString();
        }

        private static string FormatValue(object value)
        {
            if (value == null) return "null";
            return string.Format("'{0}'", value);
        }
    }
}
