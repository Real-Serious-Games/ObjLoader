﻿using System.Collections.Generic;
using System.Linq;
using ObjLoader.Loader.Common;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;

namespace ObjLoader.Loader.Data.DataStore
{
    public class DataStore : IDataStore, IGroupDataStore, IVertexDataStore, ITextureDataStore, INormalDataStore,
                             IFaceGroup, IMaterialLibrary, IElementGroup, ILineDataStore
    {
        private Group _currentGroup;

        private readonly List<Group> _groups = new List<Group>();
        private readonly List<Material> _materials = new List<Material>();

        private readonly List<Vertex> _vertices = new List<Vertex>();
        private readonly List<Texture> _textures = new List<Texture>();
        private readonly List<Normal> _normals = new List<Normal>();

        private readonly List<Line> _lines = new List<Line>();

        public IList<Vertex> Vertices
        {
            get { return _vertices; }
        }

        public IList<Texture> Textures
        {
            get { return _textures; }
        }

        public IList<Normal> Normals
        {
            get { return _normals; }
        }

        public IList<Material> Materials
        {
            get { return _materials; }
        }

        public IList<Group> Groups
        {
            get { return _groups; }
        }

        public IList<Line> Lines
        {
            get { return _lines; }
        }

        public void AddFace(Face face)
        {
            PushGroupIfNeeded();

            _currentGroup.AddFace(face);
        }

        public void PushGroup(string groupName)
        {
            _currentGroup = new Group(groupName);
            _groups.Add(_currentGroup);
        }

        private void PushGroupIfNeeded()
        {
            if (_currentGroup == null)
            {
                PushGroup("default");
            }
        }

        public void AddVertex(Vertex vertex)
        {
            _vertices.Add(vertex);
        }

        public void AddTexture(Texture texture)
        {
            _textures.Add(texture);
        }

        public void AddNormal(Normal normal)
        {
            _normals.Add(normal);
        }

        public void Push(Material material)
        {
            _materials.Add(material);
        }

        public void AddLine (Line line)
        {
            _lines.Add( line );
        }

        public void SetMaterial(string materialName)
        {
            var material = _materials.SingleOrDefault(x => x.Name.EqualsInvariantCultureIgnoreCase(materialName));
            PushGroupIfNeeded();
            _currentGroup.Material = material;
        }
    }
}