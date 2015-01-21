using System.Collections.Generic;
using System.IO;
using ObjLoader.Loader.Data.DataStore;
using ObjLoader.Loader.TypeParsers.Interfaces;

namespace ObjLoader.Loader.Loaders
{
    public class ObjLoader : LoaderBase, IObjLoader
    {
        private readonly IDataStore _dataStore;
        private readonly List<ITypeParser> _typeParsers = new List<ITypeParser>();

        private readonly List<string> _unrecognizedLines = new List<string>();

        public ObjLoader(
            IDataStore dataStore, 
            IFaceParser faceParser, 
            IGroupParser groupParser,
            INormalParser normalParser, 
            ITextureParser textureParser, 
            IVertexParser vertexParser,
            ILineParser lineParser,
            IMaterialLibraryParser materialLibraryParser, 
            IUseMaterialParser useMaterialParser)
        {
            _dataStore = dataStore;
            SetupTypeParsers(
                vertexParser,
                faceParser,
                normalParser,
                textureParser,
                groupParser,
                lineParser,
                materialLibraryParser,
                useMaterialParser);
        }

        private void SetupTypeParsers(params ITypeParser[] parsers)
        {
            foreach (var parser in parsers)
            {
                _typeParsers.Add(parser);
            }
        }

        protected override void ParseLine(string keyword, string data)
        {
            foreach (var typeParser in _typeParsers)
            {
                if (typeParser.CanParse(keyword))
                {
                    typeParser.Parse(data);
                    return;
                }
            }

            _unrecognizedLines.Add(keyword + " " + data);
        }

        public LoadResult Load(Stream lineStream)
        {
            StartLoad(lineStream);

            return CreateResult();
        }

        public LoadResult Load (string filepath)
        {
            if ( string.IsNullOrEmpty( filepath ) )
            {
                throw new FileNotFoundException( "the filepath provided to ObjLoader is null or empty" );
            }
            if ( !File.Exists( filepath ) )
            {
                throw new FileNotFoundException( string.Format( "the filepath: '{0}', could not be found", filepath ) );
            }

            File.SetAttributes( filepath, FileAttributes.Normal );

            using ( var filestream = new FileStream( filepath, FileMode.Open ) )
            {
                return this.Load( filestream );
            }
        }

        private LoadResult CreateResult()
        {
            var result = new LoadResult
                             {
                                 Vertices = _dataStore.Vertices,
                                 Textures = _dataStore.Textures,
                                 Normals = _dataStore.Normals,
                                 Groups = _dataStore.Groups,
                                 Materials = _dataStore.Materials
                             };
            return result;
        }
    }
}