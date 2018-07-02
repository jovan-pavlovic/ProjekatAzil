using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjekatAzil.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UploadTimeStamp { get; set; }

        public virtual ICollection<Dog> Dogs { get; set; }
        //[Key, ForeignKey("Image")]
        //public string Image_Id { get; set; }
        public virtual Image Image { get; set; }


        public string NameOfImage
        {
            get
            {
                return Image == null ? "event_default.jpeg" : Image.NameOfPicture;
            }
        }

    }
}