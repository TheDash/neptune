﻿using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.LinkElements.InertialElements;
using UrdfUnity.Urdf.Models.LinkElements.InertialElements;

namespace UrdfUnityTest.Parse.Xml.LinkElements.InertialElements
{
    [TestClass]
    public class MassParserTest
    {
        private static readonly string FORMAT_STRING = "<mass>{0}</mass>";

        private readonly MassParser parser = new MassParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();

        [TestMethod]
        public void ParseMass()
        {
            double[] testValues = { 0, 25, 123.456 };

            foreach (double value in testValues)
            {
                string xml = String.Format(FORMAT_STRING, value);
                this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
                Mass mass = this.parser.Parse(xmlDoc.DocumentElement);

                Assert.AreEqual(value, mass.Value);
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseNegativeMass()
        {
            string xml = String.Format(FORMAT_STRING, -1);
            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Mass mass = this.parser.Parse(xmlDoc.DocumentElement);
        }

        [TestMethod]
        public void ParseMassNoValue()
        {
            string xml = String.Format(FORMAT_STRING, String.Empty);
            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Mass mass = this.parser.Parse(xmlDoc.DocumentElement);

            Assert.AreEqual(0d, mass.Value);
        }
    }
}