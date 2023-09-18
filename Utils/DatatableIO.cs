using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaikoSoundEditor.Data;

namespace TaikoSoundEditor.Utils
{
    public class DatatableIO
    {
        public bool IsEncrypted { get; set; }        

        public T Deserialize<T>(string path)
        {
            if (!IsEncrypted)
            {
                var str = GZ.DecompressString(path);
                Debug.WriteLine("----------------------------------------------------------------------");
                Debug.WriteLine(str);
                return Json.Deserialize<T>(GZ.DecompressString(path));
            }
            else
            {
                var bytes = SSL.DecryptDatatable(File.ReadAllBytes(path));
                File.WriteAllBytes("res.bin", bytes);

                return Json.Deserialize<T>(GZ.DecompressBytes(SSL.DecryptDatatable(File.ReadAllBytes(path))));
            }
        }

    }
}
