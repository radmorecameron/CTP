using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.CompilerClient.Helpers {
    public class ZipHelper {
        public static byte[] GetZip(InMemoryFile inMemoryFile) {
            byte[] zippedfile;
            using (MemoryStream stream = new()) {
                using (ZipArchive archive = new(stream, ZipArchiveMode.Create, true)) {
                    ZipArchiveEntry zipEntry = archive.CreateEntry(inMemoryFile.FileName, CompressionLevel.Fastest);
                    using Stream zipStream = zipEntry.Open();
                    zipStream.Write(inMemoryFile.Content, 0, inMemoryFile.Content.Length);
                }
                zippedfile = stream.ToArray();
            }

            return zippedfile;
        }
    }
}
