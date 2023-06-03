using System;
using System.Collections.Generic;
using PlayerIOClient;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
using Newtonsoft.Json.Linq;
using EELVL;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.ConstrainedExecution;
using System.Windows;
using SharpCompress.Writers;

namespace EEditor
{
    public class Frame
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int[,] Foreground { get; set; }
        public int[,] Background { get; set; }
        public int[,] BlockData { get; set; }
        public int[,] BlockData1 { get; set; }
        public int[,] BlockData2 { get; set; }
        public string[,] BlockData3 { get; set; }
        public string[,] BlockData4 { get; set; }
        public string[,] BlockData5 { get; set; }
        public string[,] BlockData6 { get; set; }
        public string nickname { get; set; }
        public string owner { get; set; }
        public string levelname { get; set; }
        public int totalblocks { get; set; }
        public bool toobig { get; set; }
        public uint backgroundColor { get; set; }
        public static byte[] xx;
        public static byte[] yy;
        public static byte[] xx1;
        public static byte[] yy1;
        public static string[] split1;


        public Frame(int width, int height)
        {
            Width = width;
            Height = height;
            Foreground = new int[Height, Width];
            Background = new int[Height, Width];
            BlockData = new int[Height, Width];
            BlockData1 = new int[Height, Width];
            BlockData2 = new int[Height, Width];
            BlockData3 = new string[height, width];
            BlockData4 = new string[height, width];
            BlockData5 = new string[height, width];
            BlockData6 = new string[height, width];
            nickname = null;
            owner = null;
            levelname = null;
        }



        public void Reset(bool frame)
        {
            for (int i = 0; i < Height; ++i)
            {
                for (int j = 0; j < Width; ++j)
                {
                    if (i == 0 || j == 0 || i == Height - 1 || j == Width - 1)
                    {
                        if (Width == 110 && Height == 110)
                        {
                            if (!frame) { Foreground[i, j] = 182; }
                            else { Foreground[i, j] = -1; }
                        }
                        else
                        {
                            if (!frame) { Foreground[i, j] = 9; }
                            else { Foreground[i, j] = -1; }
                        }
                    }
                    else
                    {
                        if (!frame) { Foreground[i, j] = 0; }
                        else { Foreground[i, j] = -1; }
                    }
                }
            }
        }


        public static Color UIntToColor(uint color)
        {
            byte a = (byte)(color >> 24);
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 0);
            return Color.FromArgb(255, r, g, b);
        }


        public void SaveLVL(FileStream file)
        {
            EELVL.Level savelvl = new Level(Width, Height, 0);
            savelvl.WorldName = MainForm.WOTitle;
            savelvl.OwnerName = MainForm.WONickname;
            savelvl.OwnerID = MainForm.WOMade;
            savelvl.Minimap = MainForm.WOMinimap;
            savelvl.CrewID = MainForm.WOCrewID;
            savelvl.CrewName = MainForm.WOCrewName;
            savelvl.Campaign = MainForm.WOCampaign;
            if (!string.IsNullOrEmpty(MainForm.WODescription)) savelvl.Description = MainForm.WODescription;
            if (MainForm.userdata.useColor) savelvl.BackgroundColor = ColorToUInt(MainForm.userdata.thisColor);
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    int fid = Foreground[y, x];
                    int bid = Background[y, x];
                    if (Blocks.IsType(fid, Blocks.BlockType.Normal))
                    {
                        savelvl[0, x, y] = new Blocks.Block(fid);
                    }
                    if (Blocks.IsType(fid, Blocks.BlockType.Number))
                    {
                        savelvl[0, x, y] = new Blocks.NumberBlock(fid, BlockData[y, x]);
                    }
                    if (Blocks.IsType(fid, Blocks.BlockType.NPC))
                    {
                        savelvl[0, x, y] = new Blocks.NPCBlock(fid, BlockData3[y, x], BlockData4[y, x], BlockData5[y, x], BlockData6[y, x]);
                    }
                    if (Blocks.IsType(fid, Blocks.BlockType.Morphable))
                    {
                        savelvl[0, x, y] = new Blocks.MorphableBlock(fid, BlockData[y, x]);
                    }
                    if (Blocks.IsType(fid, Blocks.BlockType.Enumerable))
                    {
                        savelvl[0, x, y] = new Blocks.EnumerableBlock(fid, BlockData[y, x]);
                    }
                    if (Blocks.IsType(fid, Blocks.BlockType.Sign))
                    {
                        savelvl[0, x, y] = new Blocks.SignBlock(fid, BlockData3[y, x], BlockData[y, x]);
                    }
                    if (Blocks.IsType(fid, Blocks.BlockType.Rotatable) || Blocks.IsType(fid, Blocks.BlockType.RotatableButNotReally))
                    {
                        int bdata = BlockData[y, x];
                        savelvl[0, x, y] = new Blocks.RotatableBlock(fid, bdata);
                    }
                    if (Blocks.IsType(fid, Blocks.BlockType.Portal))
                    {
                        savelvl[0, x, y] = new Blocks.PortalBlock(fid, BlockData[y, x], BlockData1[y, x], BlockData2[y, x]);
                    }
                    if (Blocks.IsType(fid, Blocks.BlockType.WorldPortal))
                    {
                        savelvl[0, x, y] = new Blocks.WorldPortalBlock(fid, BlockData3[y, x], BlockData[y, x]);
                    }
                    if (Blocks.IsType(fid, Blocks.BlockType.Music))
                    {
                        savelvl[0, x, y] = new Blocks.MusicBlock(fid, BlockData[y, x]);
                    }
                    if (Blocks.IsType(bid, Blocks.BlockType.Normal))
                    {
                        savelvl[1, x, y] = new Blocks.Block(bid);
                    }
                    if (Blocks.IsType(fid, Blocks.BlockType.Label))
                    {
                        savelvl[0, x, y] = new Blocks.LabelBlock(fid, BlockData3[y, x], BlockData4[y, x], BlockData[y, x]);
                    }
                }
            }
            savelvl.Save(file);

        }
        public void Save(System.IO.BinaryWriter writer)
        {

            writer.Write(Width);
            writer.Write(Height);
            writer.Write(MainForm.WOTitle);
            writer.Write(MainForm.WONickname);
            writer.Write(MainForm.userdata.useColor ? ColorToUInt(MainForm.userdata.thisColor) : 0);
            for (int y = 0; y < Height; ++y)
                for (int x = 0; x < Width; ++x)
                {
                    int t = Foreground[y, x];
                    writer.Write((short)t);
                    writer.Write((short)Background[y, x]);
                    if (bdata.goal.Contains(t) || bdata.rotate.Contains(t) || bdata.morphable.Contains(t) && t != 385 && t != 374)
                    {
                        writer.Write((short)BlockData[y, x]);
                    }
                    if (t == 385)
                    {
                        writer.Write((short)BlockData[y, x]);
                        writer.Write(BlockData3[y, x]);
                    }
                    if (t == 374)
                    {
                        writer.Write(BlockData3[y, x]);
                        writer.Write((short)BlockData[y, x]);
                    }
                    if (bdata.portals.Contains(t))
                    {
                        writer.Write(BlockData[y, x]);
                        writer.Write(BlockData1[y, x]);
                        writer.Write(BlockData2[y, x]);
                    }
                    if (bdata.isNPC(t))
                    {
                        writer.Write(BlockData3[y, x]);
                        writer.Write(BlockData4[y, x]);
                        writer.Write(BlockData5[y, x]);
                        writer.Write(BlockData6[y, x]);
                    }
                    if (t == 1000)
                    {
                        writer.Write(BlockData[y, x]);
                        writer.Write(BlockData3[y, x]);
                        writer.Write(BlockData4[y, x]);
                    }
                }
            writer.Close();
        }

        public static bool[] detectWorlds(string file)
        {
            bool[] corrects = new bool[10];
            using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
            {
                var width = reader.ReadInt32();
                var height = reader.ReadInt32();
                corrects[0] = width >= 25 && width <= 636 || height >= 25 && height <= 400;

                if (corrects[0])
                {
                    for (int y = 0; y < height; y++)
                    {
                        var fg = reader.ReadInt16();
                        corrects[1] = fg < 500 || fg >= 1001;

                        if (corrects[1])
                        {
                            var bg = reader.ReadInt16();
                            corrects[2] = bg >= 500 || bg <= 999;
                            if (bdata.goal.Contains(fg) || bdata.rotate.Contains(fg) || bdata.morphable.Contains(fg) && fg != 385 && fg != 374)
                            {
                                var rotation = reader.ReadInt16();
                                corrects[3] = rotation >= 0 || rotation <= 999;
                            }
                            if (fg == 385)
                            {
                                var rotation = reader.ReadInt16();
                                corrects[4] = rotation >= 0 || rotation <= 5;

                                var text = reader.ReadString();
                                corrects[5] = text.Length != 0;
                            }
                            if (fg == 374)
                            {
                                var text = reader.ReadString();
                                corrects[6] = text.Length != 0;
                            }
                            if (bdata.portals.Contains(fg))
                            {

                            }
                        }
                    }
                }
                reader.Close();
            }
            return corrects;
        }

        public static Frame LoadJSONDatabaseWorld(string input, bool isFilePath = true)
        {
            int width = 0, height = 0;
            var world = JObject.Parse(isFilePath ? File.ReadAllText(input) : input);

            width = world.TryGetValue("width", out var w) ? (int)world.GetValue("width") : 200;
            height = world.TryGetValue("height", out var h) ? (int)world.GetValue("height") : 200;

            if (world.TryGetValue("worlddata", out var wd))
            {
                var f = new Frame(width, height);
                var array = wd.Values().AsJEnumerable();
                var temp = new DatabaseArray();

                foreach (var block in array)
                {
                    var dbo = new DatabaseObject();

                    foreach (var token in block)
                    {
                        var property = (JProperty)token;
                        var value = property.Value;
                        Console.WriteLine(value);

                        switch (value.Type)
                        {
                            case JTokenType.Integer:
                                dbo.Set(property.Name, (uint)value);
                                break;
                            case JTokenType.Boolean:
                                dbo.Set(property.Name, (bool)value);
                                break;
                            case JTokenType.Float:
                                dbo.Set(property.Name, (double)value);
                                break;
                            default:
                                dbo.Set(property.Name, (string)value);
                                break;
                        }
                    }
                    temp.Add(dbo);
                }
                if (temp == null || temp.Count == 0) { f = null; }
                else
                {
                    for (int i = 0; i < temp.Count; i++)
                    {
                        if (temp.Contains(i) && temp.GetObject(i).Count != 0)
                        {
                            var obj = temp.GetObject(i);
                            byte[] x = TryGetBytes(obj, "x", new byte[0]), y = TryGetBytes(obj, "y", new byte[0]);
                            byte[] x1 = TryGetBytes(obj, "x1", new byte[0]), y1 = TryGetBytes(obj, "y1", new byte[0]);

                            for (int j = 0; j < x1.Length; j++)
                            {

                                if (y1[j] < height && x1[j] < width)
                                {
                                    try
                                    {
                                        if (Convert.ToInt32(obj["type"]) < 500 || Convert.ToInt32(obj["type"]) >= 1001)
                                        {
                                            f.Foreground[y1[j], x1[j]] = Convert.ToInt32(obj["type"]);
                                            object goal, signtype, text, rotation, id, target;
                                            if (obj.TryGetValue("goal", out goal)) f.BlockData[y1[j], x1[j]] = Convert.ToInt32(goal);
                                            if (obj.TryGetValue("signtype", out signtype)) f.BlockData[y1[j], x1[j]] = Convert.ToInt32(signtype);
                                            if (obj.TryGetValue("text", out text)) f.BlockData3[y1[j], x1[j]] = text.ToString();
                                            if (obj.TryGetValue("rotation", out rotation)) f.BlockData[y1[j], x1[j]] = Convert.ToInt32(rotation);
                                            if (obj.TryGetValue("id", out id))
                                            {
                                                if (bdata.sound.Contains(Convert.ToInt32(obj["type"])))
                                                {
                                                    f.BlockData[y1[j], x1[j]] = (int)Convert.ToUInt32(id);
                                                }
                                                else
                                                {
                                                    f.BlockData[y1[j], x1[j]] = Convert.ToInt32(id);
                                                }
                                            }
                                            if (obj.TryGetValue("target", out target) && !(target is string)) f.BlockData2[y1[j], x1[j]] = Convert.ToInt32(target);
                                            if (obj.TryGetValue("target", out target) && (target is string)) f.BlockData3[y1[j], x1[j]] = target.ToString();
                                        }
                                        else if (Convert.ToInt32(obj["type"]) >= 500 && Convert.ToInt32(obj["type"]) <= 999)
                                        {
                                            f.Background[y1[j], x1[j]] = Convert.ToInt32(obj["type"]);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.ToString());
                                    }

                                }

                            }

                            for (int k = 0; k < x.Length; k += 2)
                            {
                                if (k < x.Length && k < y.Length && k + 1 < x.Length && k + 1 < y.Length)
                                {
                                    int yy = (y[k] << 8) + y[k + 1];
                                    int xx = (x[k] << 8) + x[k + 1];

                                    if (yy < height && xx < width)
                                    {
                                        if (Convert.ToInt32(obj["type"]) < 500 || Convert.ToInt32(obj["type"]) >= 1001)
                                        {
                                            object goal, signtype, text, rotation, id, target;
                                            f.Foreground[yy, xx] = Convert.ToInt32(obj["type"]);
                                            if (obj.TryGetValue("goal", out goal)) f.BlockData[yy, xx] = Convert.ToInt32(obj["goal"]);
                                            if (obj.TryGetValue("signtype", out signtype)) f.BlockData[yy, xx] = Convert.ToInt32(obj["signtype"]);
                                            if (obj.TryGetValue("text", out text)) f.BlockData3[yy, xx] = text.ToString();
                                            if (obj.TryGetValue("rotation", out rotation)) f.BlockData[yy, xx] = Convert.ToInt32(obj["rotation"]);
                                            if (obj.TryGetValue("id", out id))
                                            {
                                                if (bdata.sound.Contains(Convert.ToInt32(obj["type"])))
                                                {
                                                    f.BlockData[yy, xx] = (int)Convert.ToUInt32(id);
                                                }
                                                else
                                                {
                                                    f.BlockData[yy, xx] = Convert.ToInt32(id);
                                                }
                                            }
                                            if (obj.TryGetValue("target", out target) && target.GetType().ToString() != "System.String") f.BlockData2[yy, xx] = Convert.ToInt32(target);
                                            if (obj.TryGetValue("target", out target) && target.GetType().ToString() == "System.String") f.BlockData3[yy, xx] = target.ToString();
                                        }
                                        else if (Convert.ToInt32(obj["type"]) >= 500 && Convert.ToInt32(obj["type"]) <= 999)
                                        {
                                            f.Background[yy, xx] = Convert.ToInt32(obj["type"]);
                                        }
                                    }
                                }





                            }
                        }
                    }

                }
                return f;
            }
            else if (world.TryGetValue("world", out var wd2))
            {
                var f = new Frame(width, height);
                try
                {
                    var xwd = Convert.FromBase64String(wd2.Value<string>());

                    for (var y = 0U; y < height; y++)
                    {
                        for (var x = 0U; x < width; x++)
                        {
                            try
                            {
                                f.Foreground[x, y] = xwd[y * width + x];
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                    }
                    return f;
                }
                catch { return null; }
            }

            return null;
        }

        public static byte[] TryGetBytes(DatabaseObject input, string key, byte[] defaultValue)
        {
            if (input.TryGetValue(key, out var obj))
            {
                return (obj is string) ? Convert.FromBase64String(obj as string) : (obj is byte[]) ? obj as byte[] : defaultValue;
            }

            return defaultValue;
        }

        public static void LoadEEBuilder(string file)
        {
            if (MainForm.editArea.Frames[0].Height >= 30 && MainForm.editArea.Frames[0].Width >= 41)
            {
                bool error = false;
                string[] lines = System.IO.File.ReadAllLines(file);
                int linee = 0;
                string[,] area = new string[29, 40];
                string[,] back = new string[29, 40];
                string[,] coins = new string[29, 40];
                string[,] id = new string[29, 40];
                string[,] target = new string[29, 40];
                string[,] text1 = new string[29, 40];
                string[,] text2 = new string[29, 40];
                string[,] text3 = new string[29, 40];
                string[,] text4 = new string[29, 40];

                foreach (string line in lines)
                {
                    linee += 1;
                    if (linee == 1)
                    {
                        if (Regex.IsMatch(line, @"[0-9]{1,3}\ [0-9]{1,3}\ [0-9]{1,3}\ [0-9]{1,3}\ [0-9]{1,3}\ [0-9]{1,3}\ [0-9]{1,3}\ [0-9]{1,3}\ [0-9]{1,3}\ [0-9]{1,3}"))
                        {
                            split1 = line.Split(' ');
                            error = false;
                        }
                        else
                        {
                            error = true;
                        }
                    }
                    else if (linee > 1)
                    {
                        if (!error)
                        {
                            string[] split = line.Split(new char[] { ' ' });
                            for (int m = 0; m < eebuilderData.Length / 2; m++)
                            {
                                int s1 = eebuilderData[m, 0], s2 = eebuilderData[m, 1];
                                int abc = Convert.ToInt32(split[0]) - 1;

                                if (Convert.ToInt32(split1[abc]) == s1)
                                {
                                    area[Convert.ToInt32(split[2]) - 1, Convert.ToInt32(split[1]) - 1] = s2.ToString();
                                }
                            }
                        }
                    }
                }
                if (!error)
                {
                    Clipboard.SetData("EEData", new string[][,] { area, back, coins, id, target, text1, text2, text3, text4 });
                    MainForm.editArea.Focus();
                    SendKeys.Send("^{v}");
                }
            }
            else
            {
                MessageBox.Show("The world is too small to handle EEBuilder files.\nWorlds should be larger or equal to width 30 and height 41", "World too small", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        public static Frame LoadFromEELVL(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open))
            {
                Level lvl = Level.Open(fs);
                int totalblock = 0;
                Frame f = new Frame(lvl.Width, lvl.Height);
                string owner = lvl.CrewName == "" ? lvl.OwnerName : lvl.CrewName;
                string ownerid = lvl.CrewName == "" ? lvl.OwnerID : lvl.CrewID;
                f.levelname = lvl.WorldName;
                f.nickname = owner;
                f.owner = ownerid;
                if (lvl.BackgroundColor != 0)
                {
                    MainForm.userdata.useColor = true;
                    MainForm.userdata.thisColor = UIntToColor(lvl.BackgroundColor);
                }
                else
                {
                    MainForm.userdata.useColor = false;
                    MainForm.userdata.thisColor = Color.Transparent;
                }
                MainForm.WONickname = lvl.OwnerName;
                MainForm.WOTitle = lvl.WorldName;
                MainForm.WOMade = lvl.OwnerID;
                MainForm.WODescription = lvl.Description;
                MainForm.WOMinimap = lvl.Minimap;
                MainForm.WOBackgroundColor = lvl.BackgroundColor;
                MainForm.WOCrewID = lvl.CrewID;
                MainForm.WOCrewName = lvl.CrewName;
                MainForm.WOCampaign = lvl.Campaign;
                if (lvl.Width <= 637 && lvl.Height <= 460 || lvl.Width <= 460 && lvl.Height <= 647)
                {
                    for (int x = 0; x < lvl.Width; ++x)
                    {
                        for (int y = 0; y < lvl.Height; ++y)
                        {
                            if (Blocks.IsType(lvl[0, x, y].BlockID, Blocks.BlockType.Normal))
                            {
                                f.Foreground[y, x] = lvl[0, x, y].BlockID;
                                totalblock += 1;

                            }
                            if (Blocks.IsType(lvl[0, x, y].BlockID, Blocks.BlockType.Rotatable) || Blocks.IsType(lvl[0, x, y].BlockID, Blocks.BlockType.RotatableButNotReally))
                            {
                                f.Foreground[y, x] = lvl[0, x, y].BlockID;
                                f.BlockData[y, x] = ((Blocks.RotatableBlock)lvl[0, x, y]).Rotation;
                                totalblock += 1;
                            }
                            if (Blocks.IsType(lvl[0, x, y].BlockID, Blocks.BlockType.NPC))
                            {

                                f.Foreground[y, x] = lvl[0, x, y].BlockID;
                                f.BlockData3[y, x] = ((Blocks.NPCBlock)lvl[0, x, y]).Name;
                                f.BlockData4[y, x] = ((Blocks.NPCBlock)lvl[0, x, y]).Message1;
                                f.BlockData5[y, x] = ((Blocks.NPCBlock)lvl[0, x, y]).Message2;
                                f.BlockData6[y, x] = ((Blocks.NPCBlock)lvl[0, x, y]).Message3;
                                totalblock += 1;
                            }
                            if (Blocks.IsType(lvl[0, x, y].BlockID, Blocks.BlockType.Sign))
                            {
                                f.Foreground[y, x] = lvl[0, x, y].BlockID;
                                f.BlockData3[y, x] = ((Blocks.SignBlock)lvl[0, x, y]).Text;
                                f.BlockData[y, x] = ((Blocks.SignBlock)lvl[0, x, y]).Morph;
                                totalblock += 1;
                            }
                            if (Blocks.IsType(lvl[0, x, y].BlockID, Blocks.BlockType.Portal))
                            {
                                f.Foreground[y, x] = lvl[0, x, y].BlockID;

                                f.BlockData[y, x] = ((Blocks.PortalBlock)lvl[0, x, y]).Rotation;
                                f.BlockData1[y, x] = ((Blocks.PortalBlock)lvl[0, x, y]).ID;
                                f.BlockData2[y, x] = ((Blocks.PortalBlock)lvl[0, x, y]).Target;
                                totalblock += 1;
                            }
                            if (Blocks.IsType(lvl[0, x, y].BlockID, Blocks.BlockType.Morphable))
                            {
                                f.Foreground[y, x] = lvl[0, x, y].BlockID;
                                f.BlockData[y, x] = ((Blocks.MorphableBlock)lvl[0, x, y]).Morph;
                                totalblock += 1;
                            }
                            if (Blocks.IsType(lvl[0, x, y].BlockID, Blocks.BlockType.Number))
                            {
                                f.Foreground[y, x] = lvl[0, x, y].BlockID;
                                f.BlockData[y, x] = ((Blocks.NumberBlock)lvl[0, x, y]).Number;
                                totalblock += 1;
                            }
                            if (Blocks.IsType(lvl[0, x, y].BlockID, Blocks.BlockType.Enumerable))
                            {
                                f.Foreground[y, x] = lvl[0, x, y].BlockID;
                                f.BlockData[y, x] = ((Blocks.EnumerableBlock)lvl[0, x, y]).Variant;
                                totalblock += 1;
                            }
                            if (Blocks.IsType(lvl[0, x, y].BlockID, Blocks.BlockType.WorldPortal))
                            {
                                f.Foreground[y, x] = lvl[0, x, y].BlockID;
                                f.BlockData[y, x] = ((Blocks.WorldPortalBlock)lvl[0, x, y]).Spawn;
                                f.BlockData3[y, x] = ((Blocks.WorldPortalBlock)lvl[0, x, y]).Target;
                                totalblock += 1;
                            }
                            if (Blocks.IsType(lvl[0, x, y].BlockID, Blocks.BlockType.Music))
                            {
                                f.Foreground[y, x] = lvl[0, x, y].BlockID;
                                int temp = ((Blocks.MusicBlock)lvl[0, x, y]).Note;
                                f.BlockData[y, x] = temp;
                                totalblock += 1;
                            }
                            if (Blocks.IsType(lvl[1, x, y].BlockID, Blocks.BlockType.Normal))
                            {
                                f.Background[y, x] = lvl[1, x, y].BlockID;
                                if (lvl[1, x, y].BlockID != 0) totalblock += 1;

                            }
                            if (Blocks.IsType(lvl[0, x, y].BlockID, Blocks.BlockType.Label))
                            {
                                f.Foreground[y, x] = lvl[0, x, y].BlockID;
                                f.BlockData[y, x] = ((Blocks.LabelBlock)lvl[0, x, y]).Wrap;
                                f.BlockData3[y, x] = ((Blocks.LabelBlock)lvl[0, x, y]).Text;
                                f.BlockData4[y, x] = ((Blocks.LabelBlock)lvl[0, x, y]).Color;
                                totalblock += 1;
                            }
                        }

                    }
                    f.toobig = false;
                }
                else
                {
                    f.toobig = true;
                }
                return f;
            }
        }

        private uint ColorToUInt(Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) |
                          (color.G << 8) | (color.B << 0));
        }
        public static Frame Load(System.IO.BinaryReader reader, int num)
        {

            //Version 4.0
            if (num == 8)
            {

                int width = reader.ReadInt32();
                int height = reader.ReadInt32();

                Frame f = new Frame(width, height);
                string title = reader.ReadString();
                string owner = reader.ReadString();
                uint bgcolor = reader.ReadUInt32();
                Console.WriteLine(bgcolor);
                MainForm.WOTitle = title;
                MainForm.WONickname = owner;
                MainForm.WOBackgroundColor = bgcolor;
                f.nickname = owner;
                f.levelname = title;
                f.backgroundColor = bgcolor;
                if (bgcolor != 0)
                {
                    MainForm.userdata.useColor = true;
                    MainForm.userdata.thisColor = UIntToColor(bgcolor);
                }
                else
                {
                    MainForm.userdata.useColor = false;
                    MainForm.userdata.thisColor = Color.Transparent;
                }
                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        int t = reader.ReadInt16();
                        f.Foreground[y, x] = t;
                        f.Background[y, x] = reader.ReadInt16();
                        if (bdata.goal.Contains(t) || bdata.rotate.Contains(t) || bdata.morphable.Contains(t) && t != 385 && t != 374)
                        {
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt16());
                        }
                        if (t == 385)
                        {
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt16());
                            f.BlockData3[y, x] = reader.ReadString();
                        }
                        if (t == 374)
                        {
                            f.BlockData3[y, x] = reader.ReadString();
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt16());
                        }
                        if (bdata.portals.Contains(t))
                        {
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt32());
                            f.BlockData1[y, x] = reader.ReadInt32();
                            f.BlockData2[y, x] = reader.ReadInt32();
                        }
                        if (bdata.isNPC(t))
                        {
                            f.BlockData3[y, x] = reader.ReadString();
                            f.BlockData4[y, x] = reader.ReadString();
                            f.BlockData5[y, x] = reader.ReadString();
                            f.BlockData6[y, x] = reader.ReadString();
                        }
                        if (t == 1000)
                        {
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt32());
                            f.BlockData3[y, x] = reader.ReadString();
                            f.BlockData4[y, x] = reader.ReadString();
                        }
                    }
                }
                Console.WriteLine(f.levelname);
                return f;

            }
            if (num == 7)
            {
                int width = reader.ReadInt32();
                int height = reader.ReadInt32();
                Frame f = new Frame(width, height);
                for (int y = 0; y < height; ++y)
                    for (int x = 0; x < width; ++x)
                    {
                        int t = reader.ReadByte();
                        f.Foreground[y, x] = t;
                        f.Background[y, x] = 0;
                        if (t == 43 || t == 77 || t == 83)
                        {
                            f.BlockData[y, x] = reader.ReadByte();
                        }
                        else if (t == 242)
                        {
                            // f.BlockData[y, x] = reader.ReadInt32();
                            try
                            {
                                var r = reader.ReadInt32();
                                var a = r >> 16;
                                var b = ((r >> 8) & 0xFF);
                                var c = (r & 0xFF);
                                f.BlockData[y, x] = Convert.ToInt32(a);
                                f.BlockData1[y, x] = b;
                                f.BlockData2[y, x] = c;
                            }
                            catch (Exception ex) { Console.WriteLine("didn't exists"); }
                        }
                    }
                return f;
            }
            if (num == 6)
            {
                int width = reader.ReadInt32();
                int height = reader.ReadInt32();
                Frame f = new Frame(width, height);
                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        int t = reader.ReadInt16();
                        f.Foreground[y, x] = t;
                        f.Background[y, x] = reader.ReadInt16();
                        if (bdata.goal.Contains(t) || bdata.rotate.Contains(t) || bdata.morphable.Contains(t) && t != 385 && t != 374)
                        {
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt16());
                        }
                        if (t == 385)
                        {
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt16());
                            f.BlockData3[y, x] = reader.ReadString();
                        }
                        if (t == 374)
                        {
                            f.BlockData3[y, x] = reader.ReadString();
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt16());
                        }
                        if (bdata.portals.Contains(t))
                        {
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt32());
                            f.BlockData1[y, x] = reader.ReadInt32();
                            f.BlockData2[y, x] = reader.ReadInt32();
                        }
                        if (bdata.isNPC(t))
                        {
                            f.BlockData3[y, x] = reader.ReadString();
                            f.BlockData4[y, x] = reader.ReadString();
                            f.BlockData5[y, x] = reader.ReadString();
                            f.BlockData6[y, x] = reader.ReadString();
                        }
                    }
                }
                return f;
            }
            if (num == 5)
            {
                int width = reader.ReadInt32();
                int height = reader.ReadInt32();
                Frame f = new Frame(width, height);
                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        int t = reader.ReadInt16();
                        f.Foreground[y, x] = t;
                        f.Background[y, x] = reader.ReadInt16();
                        if (bdata.goal.Contains(t) || bdata.rotate.Contains(t) || bdata.morphable.Contains(t) && t != 385 && t != 374)
                        {
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt16());
                        }
                        if (t == 385)
                        {
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt16());
                            f.BlockData3[y, x] = reader.ReadString();
                        }
                        if (t == 374)
                        {
                            f.BlockData3[y, x] = reader.ReadString();
                        }
                        if (bdata.portals.Contains(t))
                        {
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt32());
                            f.BlockData1[y, x] = reader.ReadInt32();
                            f.BlockData2[y, x] = reader.ReadInt32();
                        }
                        if (bdata.isNPC(t))
                        {
                            f.BlockData3[y, x] = reader.ReadString();
                            f.BlockData4[y, x] = reader.ReadString();
                            f.BlockData5[y, x] = reader.ReadString();
                            f.BlockData6[y, x] = reader.ReadString();
                        }
                    }
                }
                return f;
            }
            if (num == 4)
            {
                int width = reader.ReadInt32();
                int height = reader.ReadInt32();
                Frame f = new Frame(width, height);
                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        int t = reader.ReadInt16();
                        f.Foreground[y, x] = t;
                        f.Background[y, x] = reader.ReadInt16();
                        if (bdata.goal.Contains(t) || bdata.rotate.Contains(t) || bdata.morphable.Contains(t) && t != 385 && t != 374)
                        {
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt16());
                        }
                        if (t == 385)
                        {
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt16());
                            f.BlockData3[y, x] = reader.ReadString();
                        }
                        if (t == 374)
                        {
                            f.BlockData3[y, x] = reader.ReadString();
                        }
                        if (bdata.portals.Contains(t))
                        {
                            f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt32());
                            f.BlockData1[y, x] = reader.ReadInt32();
                            f.BlockData2[y, x] = reader.ReadInt32();
                        }
                    }
                }
                return f;
            }


            if (num >= 0 && num <= 2)
            {
                int width = reader.ReadInt32();
                int height = reader.ReadInt32();
                Frame f = new Frame(width, height);

                for (var y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        if (num == 0)
                        {
                            int t = reader.ReadByte();
                            f.Foreground[y, x] = t;
                            f.Background[y, x] = 0;
                            if (bdata.goal.Contains(t) || bdata.rotate.Contains(t) || bdata.morphable.Contains(t))
                            {
                                f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt16());
                            }
                            else if (bdata.portals.Contains(t))
                            {
                                var r = reader.ReadInt32();
                                var a = r >> 16;
                                var b = ((r >> 8) & 0xFF);
                                var c = (r & 0xFF);
                                f.BlockData[y, x] = Convert.ToInt32(a);
                                f.BlockData1[y, x] = b;
                                f.BlockData2[y, x] = c;
                            }
                        }
                        else if (num == 1)
                        {
                            int t = reader.ReadInt16();
                            f.Foreground[y, x] = t;
                            f.Background[y, x] = reader.ReadInt16();
                            if (bdata.goal.Contains(t) || bdata.rotate.Contains(t) || bdata.morphable.Contains(t))
                            {
                                f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt16());
                            }
                            else if (bdata.portals.Contains(t))
                            {
                                var r = reader.ReadInt32();
                                var a = r >> 16;
                                var b = ((r >> 8) & 0xFF);
                                var c = (r & 0xFF);
                                f.BlockData[y, x] = Convert.ToInt32(a);
                                f.BlockData1[y, x] = b;
                                f.BlockData2[y, x] = c;
                            }
                        }
                        else if (num == 2)
                        {
                            int t = reader.ReadInt16();
                            f.Foreground[y, x] = t;
                            f.Background[y, x] = reader.ReadInt16();
                            if (bdata.goal.Contains(t) || bdata.rotate.Contains(t) || bdata.morphable.Contains(t))
                            {
                                f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt16());
                            }
                            else if (t == 374)
                            {
                                f.BlockData[y, x] = 0;
                                f.BlockData3[y, x] = reader.ReadString();
                            }
                            else if (t == 385)
                            {
                                f.BlockData3[y, x] = reader.ReadString();
                            }
                            else if (bdata.portals.Contains(t))
                            {
                                f.BlockData[y, x] = Convert.ToInt32(reader.ReadInt32());
                                f.BlockData1[y, x] = reader.ReadInt32();
                                f.BlockData2[y, x] = reader.ReadInt32();
                            }
                        }
                    }
                }

                return f;
            }
            else
            {
                return null;
            }
        }

        public static Frame LoadSav(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryReader reader = new BinaryReader(fs);
            char[] filetype = reader.ReadChars(16);
            string version = new string(filetype);
            Console.WriteLine(version);
            if (version == "ANIMATOR SAV V05")
            {
                reader.ReadInt16();
                int LayerCount = Convert.ToInt16(reader.ReadInt16());
                int width = Convert.ToInt16(reader.ReadInt16());
                int height = Convert.ToInt16(reader.ReadInt16());
                Frame f = new Frame(width, height);
                for (int z = 1; z >= 0; z += -1)
                {
                    for (int y = 0; y <= height - 1; y++)
                    {
                        for (int x = 0; x <= width - 1; x++)
                        {
                            int bid = eeanimator2blocks(Convert.ToInt16(reader.ReadInt16()));
                            if (bid >= 500 && bid <= 900)
                            {
                                f.Background[y, x] = bid;
                            }
                            else
                            {
                                f.Foreground[y, x] = bid;
                            }
                        }
                    }
                }
                return f;
            }
            /*else if (version == "ANIMATOR SAV V01")
            {
                int jk = 0;
                int height = 200;
                int width = 200;
                int ind = 0;
                int innum = 0;
                width = reader.ReadInt32();
                height = reader.ReadInt32();
                int frame = reader.ReadInt32();
                Console.WriteLine(frame);
                int[,] world = new int[width, height];
                //for (jk = 0; jk < frame_cnt2; jk++)
                //{
                //world = new int[200, 200];
                for (int i = 1; i <= height - 2; i += 1)
                {
                    for (int ii = 1; ii <= width - 2; ii += 1)
                    {
                        if (innum == 0)
                        {
                            ind = reader.ReadByte();
                            if (ind == 171) ind = 172;
                            if (ind == 4) ind = reader.ReadByte() + 0x400;
                            if (ind == 3)
                            {
                                innum = reader.ReadByte() + (reader.ReadByte() << 8);
                                innum -= 1;
                                ind = reader.ReadByte();

                                if (ind == 4) ind = reader.ReadByte() + 0x400;
                                if (ind == 5) ind = reader.ReadByte() + reader.ReadByte() << 8 + 0x50000;
                                world[ii, i] = ind;
                            }
                            else
                            {
                                world[ii, i] = ind;
                            }

                        }
                        else
                        {
                            innum -= 1;
                            world[ii, i] = ind;
                        }
                    }

                }
                //}
                var f = new Frame(width, height);
                for (int y = 0; y < width; y++)
                {
                    for (int x = 0; x < height; x++)
                    {
                        f.Foreground[y, x] = eeanimator2blocks1(world[x, y]);
                        Console.WriteLine(world[x, y]);
                    }
                }
                return f;
            }*/
            else
            {
                int jk = 0;
                int height = 200;
                int width = 200;
                int ind = 0;
                int innum = 0;
                reader.Close();
                reader.Dispose();
                fs = new FileStream(filename, FileMode.Open);
                reader = new BinaryReader(fs);
                //var frame_cnt2 = reader.ReadInt16();
                int[,] world = new int[200, 200];
                //for (jk = 0; jk < frame_cnt2; jk++)
                //{
                //world = new int[200, 200];
                for (int i = 1; i <= height - 2; i += 1)
                {
                    for (int ii = 1; ii <= width - 2; ii += 1)
                    {
                        if (innum == 0)
                        {
                            ind = reader.ReadByte();
                            if (ind == 171) ind = 172;
                            if (ind == 4) ind = reader.ReadByte() + 0x400;
                            if (ind == 3)
                            {
                                innum = reader.ReadByte() + (reader.ReadByte() << 8);
                                innum -= 1;
                                ind = reader.ReadByte();

                                if (ind == 4) ind = reader.ReadByte() + 0x400;
                                if (ind == 5) ind = reader.ReadByte() + reader.ReadByte() << 8 + 0x50000;
                                world[ii, i] = ind;
                            }
                            else
                            {
                                world[ii, i] = ind;
                            }

                        }
                        else
                        {
                            innum -= 1;
                            world[ii, i] = ind;
                        }
                    }

                }
                //}
                var f = new Frame(200, 200);
                for (int y = 0; y < 200; y++)
                {
                    for (int x = 0; x < 200; x++)
                    {
                        f.Foreground[y, x] = eeanimator2blocks0(world[x, y]);
                    }
                }
                return f;
            }
        }
        public static Frame LoadFromSPT(string filename)
        {
            var packets = SPTReader.Deserialize(File.ReadAllBytes(filename));
            var binary_deserializer = new BinaryDeserializer();

            int width = 200;
            int height = 200;


            var messages_deserialized = 0;

            var blocks = new List<(int layer, int x, int y, int id)>();
            binary_deserializer.OnDeserializedMessage += (m) =>
            {
                messages_deserialized++;

                var layer = m.GetInt(0);
                var x = m.GetInt(1);
                var y = m.GetInt(2);
                var id = m.GetInt(3);

                blocks.Add((layer, x, y, id));
            };

            foreach (var packet in packets)
                binary_deserializer.AddBytes(packet.Data);

            while (messages_deserialized != packets.Count)
                Thread.Sleep(1);

            var world_width = 1 + blocks.OrderByDescending(t => t.x).First().x;
            var world_height = 1 + blocks.OrderByDescending(t => t.y).First().y;

            var f = new Frame(world_width, world_height);

            foreach (var block in blocks)
            {
                switch (block.layer)
                {
                    case 0:
                        f.Foreground[block.y, block.x] = block.id;
                        break;
                    case 1:
                        f.Background[block.y, block.x] = block.id;
                        break;
                }
            }

            return f;
        }

        static int eeanimator2blocks(int id)
        {
            if (id == 127)
            {
                return 0;
            }
            else if (id - 128 >= 0 && id - 128 <= 63)
            {
                return id - 128;
            }
            else if (id + 256 >= 500 && id + 256 <= 600)
            {
                return id + 256;
            }
            else
            {
                return id - 1024;
            }
        }

        static int eeanimator2blocks0(int id)
        {
            if (id == 127)
            {
                return 0;
            }
            else if (id - 128 >= 0 && id - 128 <= 63)
            {
                return id - 128;
            }
            else
            {
                return 0;
            }
        }

        static int eeanimator2blocks1(int id)
        {
            if (id == 127)
            {
                return 0;
            }
            else if (id - 128 >= 0 && id - 128 <= 63)
            {
                return id - 128;
            }
            else
            {
                if (id - 128 != -128)
                {
                    return id - 128;
                }
                else
                {
                    return 9;
                }
            }
        }

        static int[,] eebuilderData = new int[,]
        {
                { 1, 9 }, { 2, 10 }, { 3, 11 }, { 4, 12 }, { 5, 13 }, { 6, 14 }, { 7, 15 },
                { 8, 37 }, { 9, 38 }, { 10, 39 }, { 11, 40 }, { 12, 41 }, { 13, 42 },
                { 14, 16 }, { 15, 17 }, { 16, 18 }, { 17, 19 }, { 18, 20 }, { 19, 21 },
                { 20, 29 }, { 21, 30 }, { 22, 31 }, { 23, 34 }, { 24, 35 }, { 25, 36 },
                { 26, 22 }, { 27, 32 }, { 28, 33 }, { 29, 44 },
                { 30, 6 }, { 31, 7 }, { 32, 8 }, { 33, 23 }, { 34, 24 }, { 35, 25 },
                { 36, 0 }, { 37, 26 }, { 38, 27 }, { 39, 28 },
                { 40, 0 }, { 41, 1 }, { 42, 2 }, { 43, 3 }, { 44, 4 }, { 45, 100 }, { 46, 101 },
                { 47, 5 }, { 48, 255 },
                { 49, 0 }, { 50, 0 }, { 51, 0 }, { 52, 0 }, { 53, 0 }, { 54, 0 },
                { 55, 0 }, { 56, 0 }, { 57, 0 }, { 58, 0 }, { 59, 0 },
                { 60, 45 }, { 61, 46 }, { 62, 47 }, { 63, 48 }, { 64, 49 },
                { 65, 50 }, { 66, 243 },
                { 67, 51 }, { 68, 52 }, { 69, 53 }, { 70, 54 }, { 71, 55 }, { 72, 56 }, { 73, 57 }, { 74, 58 },
                { 75, 233 }, { 76, 234 }, { 77, 235 }, { 78, 236 }, { 79, 237 }, { 80, 238 }, { 81, 239 }, { 82, 240 },
        };
    }
    /*public class blockData : IEquatable<blockData>
    {
        public int X;
        public int Y;
        public int Bid;
        public int Layer;
        public object[] Param;
        public blockData(int X, int Y, int Bid, int Layer, object[] Param = null)
        {
            this.X = X;
            this.Y = Y;
            this.Bid = Bid;
            this.Param = Param;
        }
        public bool Equals(blockData other)
        {
            if (other != null)
            {
                return this.X == other.X &&
                       this.Y == other.Y &&
                       this.Bid == other.Bid &&
                       this.Layer == other.Layer;
            }
            else { return false; }
        }

    }*/
}
