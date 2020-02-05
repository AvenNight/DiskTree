using System;
using System.Collections.Generic;
using System.Linq;

namespace DiskTree
{
    public class Folder
    {
        public string Name { get; }
        public Folder Root { get; }
        public HashSet<Folder> Folders;

        public Folder(string name, Folder root)
        {
            Name = name;
            this.Root = root;
            Folders = new HashSet<Folder>();
        }
    }

    public static class FolderExtensions
    {
        public static List<string> GetPaths(this Folder folder, List<string> result = null, string prefix = "")
        {
            if (result == null) result = new List<string>();
            if (folder.Root != null) prefix += " ";
            foreach (var f in folder.Folders.OrderBy(e => e.Name, StringComparer.Ordinal))
            {
                result.Add($"{prefix}{f.Name}");
                GetPaths(f, result, prefix);
            }
            return result;
        }
    }

    public class DiskTreeTask
    {
        public static List<string> Solve(List<string> input)
        {
            var root = new Folder("", null);
            foreach (var path in input)
            {
                var prev = root;
                foreach (var f in path.Split('\\'))
                {
                    if (!prev.Folders.Any(e => e.Name == f))
                    {
                        var newFolder = new Folder(f, prev);
                        prev.Folders.Add(newFolder);
                        prev = newFolder;
                    }
                    else
                        prev = prev.Folders.FirstOrDefault(e => e.Name == f);
                }
            }
            return root.GetPaths().ToList();
        }
    }
}