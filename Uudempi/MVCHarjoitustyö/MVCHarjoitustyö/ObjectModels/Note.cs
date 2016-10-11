using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace MVCHarjoitustyö.ObjectModels
{
    public class Note
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public byte[] ImageContent { get; set; }
        public string CreationTimeString { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public Image GetImage()
        {
            if (this.ImageContent == null)
                return null;
            using (MemoryStream ms = new MemoryStream(this.ImageContent))
            {
                return Image.FromStream(ms);
            }
        }
    }
}