using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Esp32_Crash_Dump_Decoder
{
    class MapFile
    {
        List<Symbol> _symbols = new List<Symbol>();

        string GetNextHexNumber(string s)
        {
            while (true)
            {
                if (s.StartsWith("0x") || s.Length == 0)
                {
                    return s;
                }

                s = s.Substring(1);
            }
        }

        int GetValue(string s)
        {
            return Convert.ToInt32(s.Substring(0, s.IndexOf(' ')), 16);
        }

        public MapFile(string filename)
        {
            StreamReader streamReader = File.OpenText(filename);

            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();

                if (line.StartsWith(" .text."))
                {
                    line = line + streamReader.ReadLine();
                    line = line.Replace(" .text.", "");

                    if (line.Contains("HandleBuiltInAnimate"))
                    {
                        int j = 12;
                    }

                    string name = line.Substring(0, line.IndexOf(' '));
                    name = name.Replace("_ZN", "");

                    while (name[0] >= '0' && name[0] <= '9')
                    {
                        name = name.Substring(1);
                    }

                    string parsed = GetNextHexNumber(line);
                    if (parsed.StartsWith("0x"))
                    {
                        int address = GetValue(parsed);

                        parsed = GetNextHexNumber(parsed.Substring(1));

                        if (parsed.StartsWith("0x"))
                        {
                            int size = GetValue(parsed);

                            _symbols.Add(new Symbol(name, address, size));
                            //Console.WriteLine(name + " " + address + " " + size);
                        }
                    }

                }
            }
            streamReader.Close();
        }

        public Symbol FindSymbol(int address)
        {
            Console.WriteLine(address);

            foreach (Symbol symbol in _symbols)
            {
#if fred
                Console.Write(address.ToString("X"));
                Console.Write(" ");
                Console.Write(symbol._address.ToString("X"));
                Console.Write(" ");
                Console.Write((symbol._address + symbol._length).ToString("X"));
                Console.WriteLine(" ");
                //Console.WriteLine(symbol._name);
#endif
                if (address > symbol._address && address < symbol._address + symbol._length)
                {
                    return symbol;
                }
            }

            return null;
        }
    }
}
