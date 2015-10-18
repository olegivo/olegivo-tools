using System;
using System.Linq;

namespace Oleg_ivo.Base.Extensions
{
    public abstract class TypeSwitchItem
    {
        public abstract bool IsSuitable<T>(T obj) where T : class;

        public abstract void Do<T>(T obj);
        public abstract T2 Get<T, T2>(T obj) where T2 : class;

        public static TypeSwitchItem Empty { get { return TypeSwitchActionItem<object>.Create(obj => { }); } }
    }

    public static class TypeSwitchExtensions
    {

        public static void TypeSwitch<T>(this T obj, TypeSwitchItem defaultSwitch, params TypeSwitchItem[] switches) where T:class
        {
            var switchItem = switches.FirstOrDefault(s => s.IsSuitable(obj)) ?? defaultSwitch;
            switchItem.Do(obj);
        }

        //public static TResult TypeSwitchReturn<T, TResult>(this T obj, TypeSwitchItem defaultSwitch, params TypeSwitchItem[] switches)
        //{
        //    var switchItem = switches.FirstOrDefault(s => s.IsSuitable<T>()) ?? defaultSwitch;
        //    switchItem.Do(obj);
        //}
    }

    public class TypeSwitchActionItem<TType> : TypeSwitchFunctionItem<TType, object> where TType : class
    {
        private TypeSwitchActionItem(Func<TType, object> func)
            : base(func)
        {
        }

        public static TypeSwitchActionItem<TType> Create(Action<TType> action)
        {
            return new TypeSwitchActionItem<TType>(obj => { action(obj); return null; });
        }

        public override void Do<T>(T obj)
        {
            Get<TType, object>(obj as TType);
        }

    }

    public class TypeSwitchFunctionItem<TType, TResult> : TypeSwitchItem
        where TType : class
        where TResult : class
    {
        private readonly Func<TType, TResult> func;

        protected TypeSwitchFunctionItem(Func<TType, TResult> func)
        {
            this.func = func;
        }

        public static TypeSwitchFunctionItem<TType, TResult> Create(Func<TType, TResult> func)
        {
            return new TypeSwitchFunctionItem<TType, TResult>(func);
        }

        public override bool IsSuitable<T>(T obj)
        {
            var isSuitable = obj is TType;
            return isSuitable;
        }

        public override void Do<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public override T2 Get<T, T2>(T obj)
        {
            return func(obj as TType) as T2;
        }
    }

}