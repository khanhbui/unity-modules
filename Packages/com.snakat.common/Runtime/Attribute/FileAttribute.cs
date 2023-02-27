using System;

namespace Snakat.Common.Attribute
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class FileAttribute : System.Attribute
    {
        private readonly string _path;

        public string Path => _path;

        public FileAttribute(string path)
        {
            _path = path;
        }
    }
}
