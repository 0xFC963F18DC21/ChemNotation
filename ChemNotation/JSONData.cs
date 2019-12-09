using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace ChemNotation
{
    class JSONData
    {
        private static List<DataBlock> _Data = null;
        public static List<DataBlock> Data
        {
            get
            {
                if (_Data is null)
                {
                    _Data = new List<DataBlock>();

                    using (StreamReader reader = File.OpenText(@"Resource/DefaultValues.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        _Data = (List<DataBlock>)serializer.Deserialize(reader, typeof(List<DataBlock>));
                    }
                }
                return _Data;
            }
        }

        public static DataBlock? QueryName(string name)
        {
            IEnumerable<DataBlock> results =
                from atom in _Data
                where atom.Name.ToLower() == name.ToLower()
                select atom;

            try {
                return results.First();
            }
            catch
            {
                return null;
            }
        }

        public static DataBlock? QuerySymbol(string symbol)
        {
            IEnumerable<DataBlock> results =
                from atom in _Data
                where atom.Symbol.ToLower() == symbol.ToLower()
                select atom;

            try
            {
                return results.First();
            }
            catch
            {
                return null;
            }
        }

        public struct DataBlock
        {
            public string Name { get; set; }
            public string Symbol { get; set; }
            public int Red { get; set; }
            public int Green { get; set; }
            public int Blue { get; set; }

            public DataBlock(string name = "", string symbol = "", int red = 0, int green = 0, int blue = 0)
            {
                Name = name;
                Symbol = symbol;
                Red = red;
                Green = green;
                Blue = blue;
            }
        }
    }
}
