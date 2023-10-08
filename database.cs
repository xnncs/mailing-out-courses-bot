using Telegram.Bot.Types;
using System.Xml;

namespace project
{
    class DataBase
    {
        private static Update update;
        private const string path = @"D:\code\codes\project\data\users.xml";

        private static long Id;
        private static string Username;
        private static bool Admin;
        private static bool Follow;
        

        private static XmlNode GetXmlNode(long Id, XmlElement xmlDocumentRoot)
        {
            XmlNode node = null;

            XmlNodeList userNodes = xmlDocumentRoot.SelectNodes("user");
            if (userNodes != null)
            {
                foreach (XmlNode thisNode in userNodes)
                {
                    if (Convert.ToInt64(thisNode.SelectSingleNode("@id").Value) == Id)
                    {
                        node = thisNode;
                    }
                }
            }
            return node;

        }
        private static bool ContainsUser(long Id, XmlElement xmlDocumentRoot)
        {
            bool contains = false;

            XmlNodeList userNodes = xmlDocumentRoot.SelectNodes("user");
            if (userNodes != null)
            {
                foreach (XmlNode thisNode in userNodes)
                {
                    if (Convert.ToInt64(thisNode.SelectSingleNode("@id").Value) == Id)
                    {
                        contains = true;
                    }
                }
            }
            return contains;
        }
        public void ChangeFollow(bool temp)
        {
            if(Follow == temp)
            {
                return;
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            XmlElement xmlDocumentRoot = xmlDocument.DocumentElement;

            XmlNode node = GetXmlNode(Id, xmlDocumentRoot);
            if (node != null)
            {
                xmlDocumentRoot.RemoveChild(node);
            }
            xmlDocument.Save(path);

            WriteUserData(xmlDocument, xmlDocumentRoot, temp);

        }

        public DataBase(Update getUpdate)
        {
            update = getUpdate;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            XmlElement xmlDocumentRoot = xmlDocument.DocumentElement;

            Id = update.Message.Chat.Id;

            User user;
            if(ContainsUser(Id, xmlDocumentRoot))
            {
                XmlNode node = GetXmlNode(Id, xmlDocumentRoot);
                user = GetUserData(node);
            }
            else
            {
                WriteUserData(xmlDocument, xmlDocumentRoot, follow: false);
                XmlNode node = GetXmlNode(Id, xmlDocumentRoot);
                user = GetUserData(node);                
            }

            Id = user.id;
            Username = user.username;
            Admin = user.admin;
            Follow = user.follow;
        }

        private static void WriteUserData(XmlDocument xmlDocument, XmlElement xmlDocumentRoot, bool follow)
        {
            long id = update.Message.Chat.Id;
            string username = update.Message.Chat.Username;
            bool admin = false;

            XmlElement userElement = xmlDocument.CreateElement("user");

            XmlAttribute idAttribute = xmlDocument.CreateAttribute("id");

            XmlElement nameElement = xmlDocument.CreateElement("username");
            XmlElement adminElement = xmlDocument.CreateElement("admin");
            XmlElement followElement = xmlDocument.CreateElement("follow");

            XmlText idText = xmlDocument.CreateTextNode(id.ToString());
            XmlText nameText = xmlDocument.CreateTextNode(username);
            XmlText adminText = xmlDocument.CreateTextNode(admin.ToString());
            XmlText followText = xmlDocument.CreateTextNode(follow.ToString());

            idAttribute.AppendChild(idText);
            nameElement.AppendChild(nameText);
            adminElement.AppendChild(adminText);
            followElement.AppendChild(followText);

            userElement.Attributes.Append(idAttribute);

            userElement.AppendChild(nameElement);
            userElement.AppendChild(adminElement);
            userElement.AppendChild(followElement);

            xmlDocumentRoot.AppendChild(userElement);

            xmlDocument.Save(path);

        }
        private static User GetUserData(XmlNode node)
        {
            XmlNode attribute = node.Attributes.GetNamedItem("id");
            long id = Convert.ToInt64(attribute.Value);
            string username = "unexpected";
            bool admin = false;
            bool follow = false;

            foreach (XmlNode childnode in node.ChildNodes)
            {
                if (childnode.Name == "username")
                {
                    username = childnode.InnerText;
                }
                if (childnode.Name == "admin")
                {
                    admin = Convert.ToBoolean(childnode.InnerText);
                }
                if (childnode.Name == "follow")
                {
                    follow = Convert.ToBoolean(childnode.InnerText);
                }
            }

            User user = new User(id, username, admin:admin, follow:follow);
            return user;
        }
        
        public User GetUser(long id, string username, bool admin, bool follow)
        {
            return new User(id, username, admin:admin, follow:follow);
        }
        public User GetUser()
        {
            return GetUser(Id, Username, admin:Admin, follow:Follow);
        }
        public List<User> GetAllUsers()
        {
            List<User> list = new List<User>();
            
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            XmlElement xmlDocumentRoot = xmlDocument.DocumentElement;

            List<XmlNode> nodes = new List<XmlNode>();

            XmlNodeList userNodes = xmlDocumentRoot.SelectNodes("*");
            if (userNodes != null)
            {
                foreach (XmlNode thisNode in userNodes)
                {
                    nodes.Add(thisNode);
                }
            }

            foreach(XmlNode node in nodes)
            {
                User user = GetUserData(node);
                list.Add(user);
            }

            return list;
        }
    }
}