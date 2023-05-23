using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utilities.SerializableData.SerializableFunc
{
    [System.Serializable]
    public class SerializableFunc<TReturn>
    {
        [SerializeField] protected Object targetObject;
        [SerializeField] protected string methodName;

        private Func<TReturn> func;

        public Func<TReturn> Func { get { return GetReturnedFunc(); } }

        public TReturn Invoke()
        {
            Func<TReturn> func = GetReturnedFunc();

            if (func != null) return func();
            return default;
        }

        #region Protected Interface

        protected Func<TReturn> GetReturnedFunc()
        {
            if (func == null)
            {
                if (targetObject == null) return null;
                if (string.IsNullOrWhiteSpace(methodName)) return null;

                MethodInfo info = targetObject
                    .GetType()
                    .GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(x => IsTargetMethodInfo(x));

                if (info == null) return null;

                func = (Func<TReturn>)Delegate.CreateDelegate(typeof(Func<TReturn>), targetObject, methodName);
            }

            return func;
        }

        #endregion

        #region Utility Functions

        private bool IsTargetMethodInfo(MethodInfo x)
        {
            return string.Equals(x.Name, methodName, StringComparison.InvariantCultureIgnoreCase)
                                && x.ReturnType == typeof(TReturn)
                                && x.GetParameters().Length == 0;
        }

        #endregion

        #region Operators

        public static implicit operator Func<TReturn>(SerializableFunc<TReturn> serializableFunc)
        {
            if (serializableFunc == null) return null;

            return serializableFunc.GetReturnedFunc();
        }

        #endregion
    }
}