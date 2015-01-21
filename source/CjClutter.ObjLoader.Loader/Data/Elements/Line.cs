using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjLoader.Loader.Data.Elements
{
    public class Line
    {
        private readonly List<int> _vertexIndices = new List<int>();

        public void AddIndex(int vertexIndex)
        {
            _vertexIndices.Add( vertexIndex );
        }

        public IList<int> VertexIndicies { get { return _vertexIndices; } }

        public int this[int i]
        {
            get
            {
                return _vertexIndices[i];
            }
        }

        public int Count
        {
            get
            {
                return _vertexIndices.Count;
            }
        }
    }
}
