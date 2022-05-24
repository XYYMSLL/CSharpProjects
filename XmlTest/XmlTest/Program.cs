using System;
using System.Xml;

namespace XmlTest
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            ReadXml();
        }

        public static void ReadXml()
        {
            XmlDocument doc = new XmlDocument();

            try { doc.Load("../../zhenghuo.xml"); }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }

            XmlNode root = doc.SelectSingleNode("root");

            XmlNodeList list = root.ChildNodes;

            foreach (XmlNode node in list)
            {
                XmlElement element = node as XmlElement;
                Console.WriteLine(element.GetAttribute("name") + " " + element.GetAttribute("status"));
            }
        }
    }


}
