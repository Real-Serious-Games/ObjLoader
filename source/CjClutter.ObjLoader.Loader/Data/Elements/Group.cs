﻿using System.Collections.Generic;
using ObjLoader.Loader.Data.DataStore;

namespace ObjLoader.Loader.Data.Elements
{
    public class Group : IFaceGroup
    {
        private readonly List<Face> _faces = new List<Face>();
        private readonly List<Line> _lines = new List<Line>();

        
        public Group(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public Material Material { get; set; }

        public IList<Face> Faces { get { return _faces; } }
        public IList<Line> Lines { get { return _lines; } }

        public void AddFace(Face face)
        {
            _faces.Add(face);
        }

        public void AddLine(Line line)
        {
            _lines.Add(line);
        }
    }
}