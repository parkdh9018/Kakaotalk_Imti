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
        public int listIndex;
        public String imoticonName;
        public List<String> imageCodes;
        public List<String> kewordTexts;

        [NonSerialized]
        public List<Image> imageList;

        public DataObject(String imoticonName, int listIndex, List<Image> imageList, List<String> kewordTexts)
        {
            this.imoticonName = imoticonName;
            this.listIndex = listIndex;
            this.imageList = imageList;
            this.kewordTexts = kewordTexts;

            imageCodes = this.imageList.Select(image => ImageToBase64(image)).ToList();
        }

        public void getImage()
        {
            imageList = this.imageCodes.Select(code => Base64ToImage(code)).ToList();
        }

        private String ImageToBase64(Image image)
        {
            string base64 = "";

            MemoryStream m = new MemoryStream();
            image.Save(m, ImageFormat.Bmp);
            byte[] imageBytes = m.ToArray();
            base64 = Convert.ToBase64String(imageBytes);

            return base64;
        }

        private Image Base64ToImage(String base64)
        {
            Image image = null;

            byte[] imageBytes = Convert.FromBase64String(base64);
            MemoryStream m = new MemoryStream(imageBytes, 0, imageBytes.Length);
            m.Write(imageBytes, 0, imageBytes.Length);

            image = Image.FromStream(m, true);

            return image;
        }
    }
    class DataSaveLoad
    {
        FileStream fs;
        BinaryFormatter bf; 

        public void SaveData(DataObject dataobject, String DataName)
        {
            var configure = Properties.Settings.Default;

            if (!configure.MyCollection.Contains(DataName))
            {
                configure.MyCollection.Add(DataName);
                configure.Save();
            }
                
            fs = new FileStream(dataobject.imoticonName+".dat", FileMode.Create);
            bf = new BinaryFormatter();

            bf.Serialize(fs, dataobject);
            fs.Close();

        }

        public List<DataObject> LoadData()
        {
            List<DataObject> result = new List<DataObject>();

            foreach (String name in Properties.Settings.Default.MyCollection)
            {
                Console.WriteLine("dataName : {0}", name);
                fs = new FileStream(name + ".dat", FileMode.Open);
                bf = new BinaryFormatter();

                DataObject obj = (DataObject)bf.Deserialize(fs);
                obj.getImage();

                result.Add(obj);
                fs.Close();
            }


            return result;
        }
    }
}
