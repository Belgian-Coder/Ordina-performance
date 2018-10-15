using System.Collections.Generic;
using System.Linq;
using DotNetPerformance.Entities;
using DotNetPerformance.Shared.Constants;

namespace DotNetPerformance.Data.Seeders
{
    public static class ImageSeeder
    {
        private static List<Image> _images;

        static ImageSeeder()
        {
            _images = new List<Image>();
        }

        public static IList<Image> GetSeedList()
        {
            Build();
            return _images;
        }

        public static int Count()
        {
            Build();
            return _images.Count();
        }

        private static void Build()
        {
            if (_images.Any()) return;
            List<Image> images = new List<Image>();
            for (int i = 1; i <= 1800; i++)
            {
                images.Add(BuildItem(i, $"Description image {i}", new byte[1024]));
            }
            _images = images;
        }

        private static Image BuildItem(int id, string description, byte[] blob)
        {
            var item = new Image();
            if (Startup.Startup.DbMode == DbModeEnum.Unconfigured) item.Id = id;
            item.Blob = blob;
            item.Description = description;
            return item;
        }
    }
}
