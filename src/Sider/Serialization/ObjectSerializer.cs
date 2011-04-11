﻿
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sider.Serialization
{
  public class ObjectSerializer : SerializerBase<object>
  {
    private BinaryFormatter _formatter;
    private MemoryStream _mem;

    public ObjectSerializer(int bufferSize = RedisSettings.DefaultStringBufferSize)
    {
      SAssert.ArgumentPositive(() => bufferSize);

      _formatter = new BinaryFormatter();
      _mem = new MemoryStream(bufferSize);
    }


    public override object Read(Stream src, int length)
    {
      using (var limiter = new LimitingStream(src, length))
        return _formatter.Deserialize(limiter);
    }


    public override int GetBytesNeeded(object obj)
    {
      _mem.SetLength(0);
      _formatter.Serialize(_mem, obj);

      _mem.Seek(0, SeekOrigin.Begin);
      return (int)_mem.Length;
    }

    public override void Write(object obj, Stream dest, int bytesNeeded)
    {
      _mem.CopyTo(dest);
    }
  }
}