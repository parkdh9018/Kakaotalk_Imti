using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace kakaoImti
{
    [Serializable]
    class DataObject
    {
        public String imoticonName;
        public List<String> imageCodes;
        public List<String> kewordTexts;

        public DataObject()
        {
            imageCodes = new List<string>();
            kewordTexts = new List<string>();
        }
    }
    class DataSaveLoad
    {
        FileStream fs;
        BinaryFormatter bf;

        private String ImageToBase64(Image image)
        {
            string base64 = "";

            MemoryStream m = new MemoryStream();
            image.Save(m, ImageFormat.Bmp);
            byte[] imageBytes = m.ToArray();
            base64 = Convert.ToBase64String(imageBytes);

            return base64;
        }
        
        public Image Base64ToImage(String base64)
        {
            Image image = null;

            byte[] imageBytes = Convert.FromBase64String(base64);
            MemoryStream m = new MemoryStream(imageBytes,0,imageBytes.Length);
            m.Write(imageBytes, 0, imageBytes.Length);

            image = Image.FromStream(m, true);

            return image;
        }


        public void SaveData(String name, List<Image> images, List<string> texts)
        {
            fs = new FileStream(name+".dat", FileMode.Create);
            bf = new BinaryFormatter();

            DataObject obj = new DataObject();

            obj.imoticonName = name;

            for (int i = 0; i < images.Count; i++)
            {
                obj.imageCodes.Add(ImageToBase64(images[i]));
                obj.kewordTexts.Add(texts[i]);
            }

            bf.Serialize(fs, obj);
            fs.Close();
        }

        public DataObject LoadData(String name)
        {
            fs = new FileStream(name + ".dat", FileMode.Open);
            bf = new BinaryFormatter();

            DataObject obj = (DataObject)bf.Deserialize(fs);

            fs.Close();

            return obj;
        }
    }
}
