using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrollerEngineData
{
    public interface ICloneable<T> : ICloneable
    {
        T GetClone();
    }

    public abstract class Cloneable<T> : ICloneable <T> 
    {
        public T GetClone()
        {
            var returnVal = Clone();
            if (returnVal is T)
                return (T)Clone();
            return default(T);
        }

        #region ICloneable Members

        public abstract object Clone();

        #endregion
    }
}
