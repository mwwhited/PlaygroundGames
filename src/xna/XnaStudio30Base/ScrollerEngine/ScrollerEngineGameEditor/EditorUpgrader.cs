using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrollerEngineGameEditor
{
    public static class EditorUpgrader
    {
        public static T Upgrade<T>(this object item)
            where T : new()
        {
            var newitem = new T();

            var upgradeToType = newitem.GetType();
            var upgradeFromType = item.GetType();

            var toFields = upgradeToType.GetFields();
            var fromFields = upgradeFromType.GetFields();

            var joinedFields = from f in fromFields
                               let v = f.GetValue(item)
                               join t in toFields on f.Name equals t.Name
                               where f.FieldType == t.FieldType
                               select new
                               {
                                   From = v is ICloneable ? (v as ICloneable).Clone() : v,
                                   To = t
                               };

            foreach (var joined in joinedFields)
                joined.To.SetValue(newitem, joined.From);

            return newitem;
        }
    }
}
