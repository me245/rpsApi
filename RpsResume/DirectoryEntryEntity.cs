using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpsResume
{
    public enum DirectoryEntryType { Folder, File}
    class DirectoryEntryEntity
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public DirectoryEntryType TypeDescription { get; set; }
        public string Name { get; set; }
    }
}
