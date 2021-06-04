using System;
using System.Collections.Generic;
using System.Xml;
using Xamarin.Forms;

namespace VStrip
{
    public partial class MainPage : ContentPage
    {
        public static List<Provider> fav_providers;
        public static List<Provider> all_providers;
        public static string home_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "//";
       
        public MainPage() {
            InitializeComponent();
            XmlElement xRoot = null;

            xRoot = LoadXml("all_providers.xml");
            if (xRoot != null) {
                all_providers = ExtractProviders(xRoot);
            }
            else {
                DisplayAlert("Error", "Invalid providers list", "Cansel");
            }
            
            xRoot = LoadXml("fav_providers.xml");
            if (xRoot != null) {
                fav_providers = ExtractProviders(xRoot);
            }
            else {
                DisplayAlert("Error", "Invalid favorite providers list", "Cansel");
            }
        }

        public static List<Provider> ExtractProviders(XmlElement xRoot) {
            List<Provider> providers = new List<Provider>();
            string title = null;
            string url = null;
            string content_mask = null;
            string pages_mask = null;
            string cover = null;
            foreach (XmlNode xnode in xRoot) {
                foreach (XmlNode childnode in xnode.ChildNodes) {
                    switch (childnode.Name) {
                        case "title":
                            title = xnode.Attributes?.GetNamedItem("title").InnerText;
                            break;
                        case "url":
                            url = xnode.Attributes?.GetNamedItem("url").InnerText;
                            break;
                        case "content_mask":
                            content_mask = xnode.Attributes?.GetNamedItem("content_mask").InnerText;
                            break;
                        case "pages_mask":
                            pages_mask = xnode.Attributes?.GetNamedItem("pages_mask").InnerText;
                            break;
                        case "cover":
                            cover = xnode.Attributes?.GetNamedItem("cover").InnerText;
                            break;
                    }
                }

                providers.Add(new Provider(title, url, content_mask, pages_mask, cover));
            }

            return providers;
        }

        public static XmlElement LoadXml(string x_file) {
            XmlDocument xProviders = new XmlDocument();
            try {
                xProviders.Load(home_path + x_file);
            }
            catch (Exception) {
                using (XmlWriter writer = XmlWriter.Create(home_path + x_file)) {
                    writer.WriteStartElement("providers");
                    writer.WriteEndElement();
                    writer.Flush();
                }

                xProviders.Load(home_path + x_file);
                throw;
            }

            return xProviders.DocumentElement;
        }
    }
}