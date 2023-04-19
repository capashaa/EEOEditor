using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEditor
{
    public static class SPTReader
    {
        public static List<Packet> Deserialize(byte[] input)
        {
            var state = State.HEADER;
            var packets = new List<Packet>();

            using (var reader = new BinaryReader(new StreamReader(new MemoryStream(input)).BaseStream))
            {
                Packet packet = null;
                bool finished = false;

                while (!finished)
                {
                    switch (state)
                    {
                        case State.HEADER:
                            var capacity = reader.ReadInt32();

                            packets = new List<Packet>(capacity);
                            state = State.NAME;
                            break;
                        case State.NAME:
                        {
                            var length = reader.ReadInt32();
                            var name = reader.ReadChars(length);

                            if (packet == null)
                            {
                                packet = new Packet()
                                {
                                    Name = new string(name)
                                };

                                state = State.DATA;
                            }
                            else
                            {
                                packet.Name = new string(name);
                                state = State.DATA;
                            }
                        }
                        break;
                        case State.DATA:
                        {
                            var length = reader.ReadInt32();
                            var data = reader.ReadBytes(length);

                            if (packet != null)
                            {
                                packet.Data = data;
                                packets.Add(packet);

                                state = State.NAME;
                                packet = new Packet();
                            }

                            if (packets.Count >= packets.Capacity)
                            {
                                finished = true;
                            }
                        }
                        break;
                    }
                }
            }

            return packets;
        }

        public static byte[] Serialize(List<Packet> packets)
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);

            writer.Write((int)packets.Count);

            foreach (var packet in packets)
            {
                writer.Write((int)packet.Name.Length);
                writer.Write(packet.Name.ToCharArray());

                writer.Write(packet.Data.Length);
                writer.Write(packet.Data);
            }

            return stream.ToArray();
        }

        public enum State { HEADER, NAME, DATA }
    }

    public class Packet
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
