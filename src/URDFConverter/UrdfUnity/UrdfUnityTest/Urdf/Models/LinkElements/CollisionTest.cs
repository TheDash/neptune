﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.Attributes;
using UrdfUnity.Urdf.Models.LinkElements;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements
{
    [TestClass]
    public class CollisionTest
    {
        [TestMethod]
        public void ConstructCollision()
        {
            String name = "name";
            Origin origin = new Origin();
            Geometry geometry = new Geometry(new Sphere(1));
            Collision collision = new Collision.Builder(name).SetOrigin(origin).SetGeometry(geometry).Build();

            Assert.AreEqual(name, collision.Name);
            Assert.AreEqual(origin, collision.Origin);
            Assert.AreEqual(geometry, collision.Geometry);
        }

        [TestMethod]
        public void ConstructCollisionNoOrigin()
        {
            Geometry geometry = new Geometry(new Sphere(1));
            Collision collision = new Collision.Builder().SetGeometry(geometry).Build();

            Assert.IsNull(collision.Name);
            Assert.AreEqual(geometry, collision.Geometry);
            Assert.AreEqual(new Origin(), collision.Origin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructCollisionNoGeometry()
        {
            Collision collision = new Collision.Builder().Build();
        }

        [TestMethod]
        public void ConstructCollisionNullName()
        {
            Geometry geometry = new Geometry(new Sphere(1));
            Collision collision = new Collision.Builder(null).SetGeometry(geometry).Build();

            Assert.IsNull(collision.Name);
            Assert.AreEqual(geometry, collision.Geometry);
            Assert.AreEqual(new Origin(), collision.Origin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructCollisionNullGeometry()
        {
            Collision collision = new Collision.Builder().SetGeometry(null).Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructCollisionNullOrigin()
        {
            Collision collision = new Collision.Builder().SetOrigin(null).Build();
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Collision collision = new Collision.Builder().SetGeometry(new Geometry(new Sphere(1))).Build();
            Collision same = new Collision.Builder().SetGeometry(new Geometry(new Sphere(1))).Build();
            Collision diff = new Collision.Builder().SetOrigin(new Origin.Builder().SetXyz(new XyzAttribute(1, 2, 3)).Build()) 
                .SetGeometry(new Geometry(new Sphere(1))).Build();

            Assert.IsTrue(collision.Equals(collision));
            Assert.IsFalse(collision.Equals(null));
            Assert.IsTrue(collision.Equals(same));
            Assert.IsTrue(same.Equals(collision));
            Assert.IsFalse(collision.Equals(diff));
            Assert.AreEqual(collision.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(collision.GetHashCode(), diff.GetHashCode());
        }
    }
}
