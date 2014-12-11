using ObjLoader.Loader.Data.DataStore;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.TypeParsers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjLoader.Loader.TypeParsers
{
    public class LineParser : TypeParserBase, ILineParser
    {
        private readonly ILineDataStore _lineDataStore;

        public LineParser(ILineDataStore lineDataStore)
        {
            _lineDataStore = lineDataStore;
        }

        protected override string Keyword
        {
            get 
            {
                return "l";
            }
        }

        public override void Parse (string parseLine)
        {
            var vertexIndices = parseLine.Split( new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
            var line = new Line();

            foreach(var indexString in vertexIndices)
            {
                int index;
                System.Int32.TryParse( indexString, out index);
                if ( index != 0 )
                {
                    line.AddIndex( index );
                }
            }

            _lineDataStore.AddLine( line );
        }
    }
}
