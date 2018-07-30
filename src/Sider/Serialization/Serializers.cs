
using System;
using System.Collections.Generic;

namespace Sider.Serialization
{
  // TODO: Make public + user configurable
  internal static class Serializers
  {
    public static Dictionary<Type, ISerializer> CustomSerializers { get; set; }
    
    public static ISerializer<T> For<T>()
    {
      Type type = typeof(T);
      if (CustomSerializers.ContainsKey(type))
        return (ISerializer<T>)CustomSerializers[type];
      if (type == typeof(string))
        return (ISerializer<T>)new StringSerializer();

      if (type == typeof(byte[]))
        return (ISerializer<T>)new BufferSerializer();

      return new ObjectSerializer<T>();
    }
  }
}
