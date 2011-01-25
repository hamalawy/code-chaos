using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GameLib_01.Data
{
    public static class Serializer
    {
        #region Fields
        #endregion

        #region Init
        #endregion

        #region Functions
        /// <summary>
        /// Serializes an object to Xml as a string.
        /// </summary>
        /// <typeparam name="T">Datatype T.</typeparam>
        /// <param name="ToSerialize">Object of type T to be serialized.</param>
        /// <returns>Xml string of serialized object.</returns>
        public static string SerializeToXmlString<T>(T ToSerialize)
        {
            string xmlstream = String.Empty;

            using (MemoryStream memstream = new MemoryStream())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                XmlTextWriter xmlWriter = new XmlTextWriter(memstream, Encoding.UTF8);

                xmlSerializer.Serialize(xmlWriter, ToSerialize);
                xmlstream = UTF8ByteArrayToString(((MemoryStream)xmlWriter.BaseStream).ToArray());
            }

            return xmlstream;
        }
        /// <summary>
        /// Deserializes an object of type T.
        /// </summary>
        /// <typeparam name="T">Datatype T.</typeparam>
        /// <param name="FileName">File name from which to read the object.</param>
        /// <returns>Returns rehydrated object of type T.</returns>
        public static T Deserialize<T>(string FileName)
        {
            IFormatter formatter = new BinaryFormatter();
            T tempObject;

            using (Stream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            { tempObject = (T)formatter.Deserialize(stream); }

            return tempObject;
        }
        /// <summary>
        /// Deserializes Xml object of type T.
        /// </summary>
        /// <typeparam name="T">Datatype T.</typeparam>
        /// <param name="FileName">File name from which to read the Xml.</param>
        /// <returns>Returns rehydrated object of type T.</returns>
        public static T XmlDeserialize<T>(string FileName)
        {
            T tempObject;

            using (StreamReader streamReader = new StreamReader(FileName))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                tempObject = (T)xmlSerializer.Deserialize(streamReader);
            }

            return tempObject;
        }
        /// <summary>
        /// Deserializes Xml string of type T.
        /// </summary>
        /// <typeparam name="T">Datatype T.</typeparam>
        /// <param name="XmlString">Input Xml string from which to read.</param>
        /// <returns>Returns rehydrated object of type T.</returns>
        public static T DeserializeXmlString<T>(string XmlString)
        {
            T tempObject;

            using (MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(XmlString)))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                tempObject = (T)xs.Deserialize(memoryStream);
            }

            return tempObject;
        } 

        // Convert Array to String
        public static String UTF8ByteArrayToString(Byte[] ArrBytes)
        { return new UTF8Encoding().GetString(ArrBytes); }
        // Convert String to Array
        public static Byte[] StringToUTF8ByteArray(String XmlString)
        { return new UTF8Encoding().GetBytes(XmlString); }
        // Convert String to Array
        public static Byte[] NumberToUTF8ByteArray(Int32 Number)
        { return new UTF8Encoding().GetBytes(Convert.ToByte(Number).ToString()); }

        /// <summary>
        /// Serializes an object of type T.
        /// </summary>
        /// <typeparam name="T">Datatype T.</typeparam>
        /// <param name="FileName">File name to write serialized object.</param>
        /// <param name="ToSerialize">Object of type T to be serialized.</param>
        public static void Serialize<T>(string FileName, T ToSerialize)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None))
            { formatter.Serialize(stream, ToSerialize); }
        }
        /// <summary>
        /// Xml Serializes an object of type T.
        /// </summary>
        /// <typeparam name="T">Datatype T.</typeparam>
        /// <param name="FileName">File name to write Xml serialization.</param>
        /// <param name="ToSerialize">Object of type T to be serialized.</param>
        public static void XmlSerialize<T>(string FileName, T ToSerialize)
        {
            using (StreamWriter streamWriter = new StreamWriter(FileName))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(streamWriter, ToSerialize);
            }
        }
        #endregion
    }
}