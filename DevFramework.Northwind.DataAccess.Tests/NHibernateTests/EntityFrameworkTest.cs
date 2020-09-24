﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevFramework.Northwind.DataAccess.Concrete.EntityFramework;
using DevFramework.Northwind.DataAccess.Concrete.NHibernate;
using DevFramework.Northwind.DataAccess.Concrete.NHibernate.Helpers;

namespace DevFramework.Northwind.DataAccess.Tests.NHibernateTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class NHibernateTest
    {


        [TestMethod]
        public void Get_All_returns_all_products()
        {
            NhProductDal productDal = new NhProductDal(new SqlServerHelper());
            var result = productDal.GitList();
            Assert.AreEqual(77, result.Count);



        }
        [TestMethod]
        public void Get_All_With_parameter_returns_filtered_products()
        {
            NhProductDal productDal = new NhProductDal(new SqlServerHelper());
            var result = productDal.GitList(p => p.ProductName.Contains("ab"));
            Assert.AreEqual(4, result.Count);



        }
    }
}