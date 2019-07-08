using System;
using System.Collections.Generic;
using System.Text;

namespace common.mdl
{
    public class KeyValuePair<TKey, TValue> : IModel
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}
