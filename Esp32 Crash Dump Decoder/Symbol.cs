using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esp32_Crash_Dump_Decoder
{
    class Symbol
    {
        public string _name;
        public int _address;
        public int _length;

        public Symbol(string name, int address, int length)
        {
            _name = name;
            _address = address;
            _length = length;
        }

        public bool IsMatch(int address)
        {
            return address >= _address && address < _address + _length;
        }

        public string FixedName
        {
            get
            {
                string fixedName = String.Empty;

                int index = 0;

                while (index < _name.Length)
                {
                    char c = _name[index];

                    if (c >= '0' && c <= '9')
                    {
                        break;
                    }

                    fixedName += _name[index];

                    index++;
                }

                if (index == _name.Length)
                {
                    return fixedName;
                }

                int numberStart = index;

                while (index < _name.Length)
                {
                    char c = _name[index];

                    if (c >= '0' && c <= '9')
                    {
                    }
                    else
                    {
                        break;
                    }
                    index++;
                }

                string numberString = _name.Substring(numberStart, index - numberStart);
                int functionLength = Int32.Parse(numberString);

                string functionName = _name.Substring(index, functionLength);

                return fixedName + "::" + functionName;

            }
        }
    }
}
