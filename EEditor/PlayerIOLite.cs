using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EEditor.BinaryDeserializer;

namespace EEditor
{
    internal class BinaryDeserializer
    {
        internal enum EnumPattern
        {
            STRING_SHORT_PATTERN = 0xC0, STRING_PATTERN = 0x0C, BYTE_ARRAY_SHORT_PATTERN = 0x40, BYTE_ARRAY_PATTERN = 0x10,
            UNSIGNED_LONG_SHORT_PATTERN = 0x38, UNSIGNED_LONG_PATTERN = 0x3C, LONG_SHORT_PATTERN = 0x30, LONG_PATTERN = 0x34,
            UNSIGNED_INT_SHORT_PATTERN = 0x80, UNSIGNED_INT_PATTERN = 0x08, INT_PATTERN = 0x04, DOUBLE_PATTERN = 0x03,
            FLOAT_PATTERN = 0x02, BOOLEAN_TRUE_PATTERN = 0x01, BOOLEAN_FALSE_PATTERN = 0x00, DOES_NOT_EXIST = -1
        }

        internal enum EnumState
        {
            Init, Header, Data
        }

        internal delegate void MessageDeserializedEventHandler(Message e);
        internal delegate void ValueDeserializedEventHandler(object value);

        public event MessageDeserializedEventHandler OnDeserializedMessage;
        internal event ValueDeserializedEventHandler OnDeserializedValue;

        internal EnumState State = EnumState.Init;
        internal EnumPattern Pattern = EnumPattern.DOES_NOT_EXIST;

        internal MemoryStream _buffer;
        internal Message _message;

        internal int _length;
        internal int _partLength;

        public BinaryDeserializer()
        {
            _buffer = new MemoryStream();
            _message = null;
            _length = -1;
            _partLength = 0;

            OnDeserializedValue += (value) => {
                if (_length == -1)
                {
                    _length = (int)value;
                }
                else
                {
                    if (_message == null)
                        _message = new Message((string)value);
                    else
                        _message.Add(value);

                    if (_length == _message.Count)
                    {
                        OnDeserializedMessage?.Invoke(_message);

                        _message = null;
                        _length = -1;
                    }
                }

                State = EnumState.Init;
            };
        }

        public void AddBytes(byte[] input)
        {
            foreach (var value in input)
                DeserializeValue(value);
        }

        internal void DeserializeValue(byte value)
        {
            switch (State)
            {
                case EnumState.Init:
                    Pattern = value.RetrieveFlagPattern();

                    switch (Pattern)
                    {
                        case EnumPattern.STRING_SHORT_PATTERN:
                            _partLength = value.RetrievePartLength(Pattern);

                            if (_partLength > 0)
                                State = EnumState.Data;
                            else
                                OnDeserializedValue("");
                            break;
                        case EnumPattern.STRING_PATTERN:
                            _partLength = value.RetrievePartLength(Pattern) + 1;
                            State = EnumState.Header;
                            break;
                        case EnumPattern.BYTE_ARRAY_SHORT_PATTERN:
                            _partLength = value.RetrievePartLength(Pattern);

                            if (_partLength > 0)
                                State = EnumState.Data;
                            else
                                OnDeserializedValue(new byte[] { });
                            break;
                        case EnumPattern.BYTE_ARRAY_PATTERN:
                            _partLength = value.RetrievePartLength(Pattern) + 1;
                            State = EnumState.Header;
                            break;
                        case EnumPattern.UNSIGNED_INT_SHORT_PATTERN:
                            OnDeserializedValue(value.RetrievePartLength(Pattern));
                            break;
                        case EnumPattern.UNSIGNED_INT_PATTERN:
                            _partLength = value.RetrievePartLength(Pattern) + 1;
                            State = EnumState.Data;
                            break;
                        case EnumPattern.INT_PATTERN:
                            _partLength = value.RetrievePartLength(Pattern) + 1;
                            State = EnumState.Data;
                            break;
                        case EnumPattern.UNSIGNED_LONG_SHORT_PATTERN:
                            _partLength = 1;
                            State = EnumState.Data;
                            break;
                        case EnumPattern.UNSIGNED_LONG_PATTERN:
                            _partLength = 6;
                            State = EnumState.Data;
                            break;
                        case EnumPattern.LONG_SHORT_PATTERN:
                            _partLength = 1;
                            State = EnumState.Data;
                            break;
                        case EnumPattern.LONG_PATTERN:
                            _partLength = 6;
                            State = EnumState.Data;
                            break;
                        case EnumPattern.DOUBLE_PATTERN:
                            _partLength = 8;
                            State = EnumState.Data;
                            break;
                        case EnumPattern.FLOAT_PATTERN:
                            _partLength = 4;
                            State = EnumState.Data;
                            break;
                        case EnumPattern.BOOLEAN_TRUE_PATTERN:
                            OnDeserializedValue(true);
                            break;
                        case EnumPattern.BOOLEAN_FALSE_PATTERN:
                            OnDeserializedValue(false);
                            break;
                    }
                    break;

                case EnumState.Header:
                    _buffer.WriteByte(value);

                    if (_buffer.Position == _partLength)
                    {
                        var buffer = _buffer.ToArray().Reverse().ToArray();
                        var length = new List<byte>(4) { 0, 0, 0, 0 }.Select((b, index) => index <= _partLength - 1 ? buffer[index] : (byte)0);

                        _partLength = BitConverter.ToInt32(length.ToArray(), 0);
                        State = EnumState.Data;

                        _buffer.Position = 0;
                    }
                    break;
                case EnumState.Data:
                    _buffer.WriteByte(value);

                    if (_buffer.Position == _partLength)
                    {
                        var buffer = _buffer.ToArray();
                        var length = new List<byte>(8) { 0, 0, 0, 0, 0, 0, 0, 0 }.Select((v, index) => index <= _partLength - 1 ? buffer[index] : (byte)0);

                        Array.Reverse(buffer, 0, _partLength);

                        switch (Pattern)
                        {
                            case EnumPattern.STRING_SHORT_PATTERN:
                            case EnumPattern.STRING_PATTERN:
                                OnDeserializedValue(Encoding.UTF8.GetString(_buffer.ToArray()));
                                break;
                            case EnumPattern.UNSIGNED_INT_PATTERN:
                                OnDeserializedValue(BitConverter.ToUInt32(length.ToArray(), 0));
                                break;
                            case EnumPattern.INT_PATTERN:
                                OnDeserializedValue(BitConverter.ToInt32(length.ToArray(), 0));
                                break;
                            case EnumPattern.UNSIGNED_LONG_SHORT_PATTERN:
                            case EnumPattern.UNSIGNED_LONG_PATTERN:
                                OnDeserializedValue(BitConverter.ToUInt64(length.ToArray(), 0));
                                break;
                            case EnumPattern.LONG_SHORT_PATTERN:
                            case EnumPattern.LONG_PATTERN:
                                OnDeserializedValue(BitConverter.ToInt64(length.ToArray(), 0));
                                break;
                            case EnumPattern.DOUBLE_PATTERN:
                                OnDeserializedValue(BitConverter.ToDouble(length.ToArray(), 0));
                                break;
                            case EnumPattern.FLOAT_PATTERN:
                                OnDeserializedValue(BitConverter.ToSingle(length.ToArray(), 0));
                                break;
                            case EnumPattern.BYTE_ARRAY_SHORT_PATTERN:
                            case EnumPattern.BYTE_ARRAY_PATTERN:
                                OnDeserializedValue(_buffer.ToArray());
                                break;
                        }

                        _buffer = new MemoryStream();
                    }
                    break;
            }
        }
    }

    internal static class PatternHelper
    {
        internal static EnumPattern RetrieveFlagPattern(this byte input)
        {
            foreach (var pattern in ((EnumPattern[])Enum.GetValues(typeof(EnumPattern))).OrderByDescending(p => p))
                if ((input & (byte)pattern) == (byte)pattern)
                    return pattern;

            return EnumPattern.DOES_NOT_EXIST;
        }

        internal static int RetrievePartLength(this byte input, EnumPattern pattern) => input & ~(byte)pattern;
    }

    internal class BinarySerializer
    {
        internal static MemoryStream _buffer = new MemoryStream();

        public byte[] Serialize(Message message)
        {
            byte[] output;

            SerializeValue(message.Count);
            SerializeValue(message.Type);

            foreach (var value in message)
                SerializeValue(value);

            output = _buffer.ToArray();
            _buffer = new MemoryStream();

            return output;
        }

        private void SerializeValue(object value)
        {
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.String:
                    Writer.WriteTagWithLength(Encoding.UTF8.GetBytes(value as string).Length, EnumPattern.STRING_SHORT_PATTERN, EnumPattern.STRING_PATTERN);
                    Writer.Write(Encoding.UTF8.GetBytes(value as string));
                    break;
                case TypeCode.Int32:
                    Writer.WriteTagWithLength((int)value, EnumPattern.UNSIGNED_INT_SHORT_PATTERN, EnumPattern.INT_PATTERN);
                    break;
                case TypeCode.UInt32:
                    Writer.WriteBottomPatternAndBytes(EnumPattern.UNSIGNED_INT_PATTERN, LittleEndianToNetworkOrderBitConverter.GetBytes((uint)value));
                    break;
                case TypeCode.Int64:
                    Writer.WriteLongPattern(EnumPattern.LONG_SHORT_PATTERN, EnumPattern.LONG_PATTERN, LittleEndianToNetworkOrderBitConverter.GetBytes((long)value));
                    break;
                case TypeCode.UInt64:
                    Writer.WriteLongPattern(EnumPattern.UNSIGNED_LONG_SHORT_PATTERN, EnumPattern.UNSIGNED_LONG_PATTERN, LittleEndianToNetworkOrderBitConverter.GetBytes((ulong)value));
                    break;
                case TypeCode.Double:
                    Writer.Write(EnumPattern.DOUBLE_PATTERN);
                    Writer.Write(LittleEndianToNetworkOrderBitConverter.GetBytes((double)value));
                    break;
                case TypeCode.Single:
                    Writer.Write(EnumPattern.FLOAT_PATTERN);
                    Writer.Write(LittleEndianToNetworkOrderBitConverter.GetBytes((float)value));
                    break;
                case TypeCode.Boolean:
                    Writer.Write((bool)value ? EnumPattern.BOOLEAN_TRUE_PATTERN : EnumPattern.BOOLEAN_FALSE_PATTERN);
                    break;
                case TypeCode.Object:
                    if (!(value is byte[]))
                        break;

                    var array = (byte[])value;

                    Writer.WriteTagWithLength(array.Length, EnumPattern.BYTE_ARRAY_SHORT_PATTERN, EnumPattern.BYTE_ARRAY_PATTERN);
                    Writer.Write(array);
                    break;
            }
        }

        private static class Writer
        {
            internal static void WriteTagWithLength(int length, EnumPattern topPattern, EnumPattern bottomPattern)
            {
                if (length > 63 || length < 0)
                {
                    var bytes = LittleEndianToNetworkOrderBitConverter.GetBytes(length);
                    WriteBottomPatternAndBytes(bottomPattern, bytes);
                }
                else
                {
                    _buffer.WriteByte((byte)((int)topPattern | length));
                }
            }

            internal static void WriteBottomPatternAndBytes(EnumPattern pattern, byte[] bytes)
            {
                var counter = bytes[0] != 0 ? 3 : bytes[1] != 0 ? 2 : bytes[2] != 0 ? 1 : 0;

                _buffer.WriteByte((byte)((int)pattern | counter));
                _buffer.Write(bytes, bytes.Length - counter - 1, counter + 1);
            }

            internal static void WriteLongPattern(EnumPattern shortPattern, EnumPattern longPattern, byte[] bytes)
            {
                var counter = 0;
                for (var nc = 0; nc != 7; nc++)
                {
                    if (bytes[nc] != 0)
                    {
                        counter = 7 - nc;
                        break;
                    }
                }

                if (counter > 3)
                    _buffer.WriteByte((byte)((byte)longPattern | counter - 4));
                else
                    _buffer.WriteByte((byte)((byte)shortPattern | counter));

                _buffer.Write(bytes, bytes.Length - counter - 1, counter + 1);
            }

            internal static void Write(EnumPattern pattern) => Write((byte)pattern);

            internal static void Write(byte value)
            {
                _buffer.WriteByte(value);
            }

            internal static void Write(byte[] value)
            {
                _buffer.Write(value, 0, value.Length);
            }
        }
    }

    internal static class LittleEndianToNetworkOrderBitConverter
    {
        public static byte[] GetBytes(int value) => ReverseArray(BitConverter.GetBytes(value), 4, 0);
        public static byte[] GetBytes(uint value) => ReverseArray(BitConverter.GetBytes(value), 4, 0);
        public static byte[] GetBytes(long value) => ReverseArray(BitConverter.GetBytes(value), 8, 0);
        public static byte[] GetBytes(ushort value) => ReverseArray(BitConverter.GetBytes(value), 2, 0);
        public static byte[] GetBytes(ulong value) => ReverseArray(BitConverter.GetBytes(value), 8, 0);
        public static byte[] GetBytes(float value) => ReverseArray(BitConverter.GetBytes(value), 4, 0);
        public static byte[] GetBytes(double value) => ReverseArray(BitConverter.GetBytes(value), 8, 0);

        public static int ToInt32(byte[] value, int startIndex, int length) => BitConverter.ToInt32(ReverseArray(value, length, startIndex), startIndex);
        public static uint ToUInt32(byte[] value, int startIndex, int length) => BitConverter.ToUInt32(ReverseArray(value, length, startIndex), startIndex);
        public static long ToInt64(byte[] value, int startIndex, int length) => BitConverter.ToInt64(ReverseArray(value, length, startIndex), startIndex);
        public static ulong ToUInt64(byte[] value, int startIndex, int length) => BitConverter.ToUInt64(ReverseArray(value, length, startIndex), startIndex);
        public static float ToSingle(byte[] value, int startIndex, int length) => BitConverter.ToSingle(ReverseArray(value, length, startIndex), startIndex);
        public static double ToDouble(byte[] value, int startIndex, int length) => BitConverter.ToDouble(ReverseArray(value, length, startIndex), startIndex);

        private static byte[] ReverseArray(byte[] input, int length, int startIndex)
        {
            Array.Reverse(input, startIndex, length);
            return input;
        }
    }

    public class Message : IEnumerable<object>
    {
        public string Type { get; internal set; }
        public List<object> Values { get; internal set; } = new List<object>();
        public int Count => this.Values.Count;

        public Message(string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException("type");

            this.Type = type;
        }

        public Message(string type, params object[] parameters)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException("type");

            if (parameters == null)
                throw new ArgumentNullException("parameters");

            this.Type = type;
            this.Add(parameters);
        }

        public static Message Create(string type, params object[] parameters)
        {
            if (string.IsNullOrEmpty(type))
                throw new Exception("You must specify a type for the PlayerIO message.");

            var message = new Message(type);
            message.Add(parameters);

            return message;
        }

        public IEnumerator<object> GetEnumerator() => this.Values.GetEnumerator();

        public override string ToString()
        {
            var sb = new StringBuilder("");

            sb.AppendLine(string.Concat("  msg.Type= ", this.Type, ", ", this.Values.Count, " entries"));

            for (var i = 0; i < this.Values.Count; i++)
                sb.AppendLine(string.Concat("  msg[", i, "] = ", this.Values[i], "  (", this.Values[i].GetType().Name, ")"));

            return sb.ToString();
        }

        public object this[uint index] => this.Values[(int)index];

        public string GetString(uint index) => (string)this[index];
        public byte[] GetByteArray(uint index) => (byte[])this[index];
        public bool GetBoolean(uint index) => (bool)this[index];
        public double GetDouble(uint index) => (double)this[index];
        public float GetFloat(uint index) => (float)this[index];
        public int GetInteger(uint index) => (int)this[index];
        public int GetInt(uint index) => (int)this[index];
        public uint GetUInt(uint index) => (uint)this[index];
        public uint GetUnsignedInteger(uint index) => (uint)this[index];
        public long GetLong(uint index) => (long)this[index];
        public ulong GetULong(uint index) => (ulong)this[index];
        public ulong GetUnsignedLong(uint index) => (ulong)this[index];

        public void Add(string value) => Add(value);
        public void Add(int value) => Add(value);
        public void Add(uint value) => Add(value);
        public void Add(long value) => Add(value);
        public void Add(ulong value) => Add(value);
        public void Add(byte[] value) => Add(value);
        public void Add(float value) => Add(value);
        public void Add(double value) => Add(value);
        public void Add(bool value) => Add(value);

        public void Add(params object[] parameters)
        {
            if (parameters.Length == 0)
                return;

            var allowedTypes = new List<Type>() {
                typeof(string), typeof(int),    typeof(uint),
                typeof(long),   typeof(ulong),  typeof(float),
                typeof(double), typeof(bool),   typeof(byte[])
            };

            foreach (var value in parameters)
            {
                if (value == null)
                    throw new Exception("PlayerIO messages do not support null objects.");

                if (!allowedTypes.Contains(value.GetType()))
                    throw new Exception($"PlayerIO messages do not support objects of type '{value.GetType()}'");

                this.Values.Add(value);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)this.Values).GetEnumerator();
        }
    }
}
