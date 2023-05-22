using System;

namespace ProfileSample.Models
{
    public class ImageModel
    {
        public string Name { get; set; }

        public byte[] Data { get; set; }
        public string Img
        {
            get;
            //{
            //    var converted = Convert.ToBase64String(Data);
            //    var imgSrc = string.Format("data:image/jpg;base64,{0}", converted);
            //    return imgSrc;
            //}
            set;
        }
    }
}