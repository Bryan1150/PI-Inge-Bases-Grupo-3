﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Planetario.Models;
using Planetario.Controllers;
using Planetario.Handlers;
using System.Web.Mvc;
using Moq;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace PruebasPlanetarios
{
    [TestClass]
    public class VentasControllerTest
    {
        [TestMethod]
        public void ListaProductosNoEsNula()
        {
            var mockVentas = new Mock<IVentasService>();
            mockVentas.Setup(servicio => servicio.ObtenerTodosLosProductos()).Returns(new List<ProductoModel>());
            VentasController ventasController = new VentasController(mockVentas.Object);

            ViewResult vistaResultado = ventasController.ListaProductos() as ViewResult;

            Assert.IsNotNull(vistaResultado);
        }

        [TestMethod]
        public void ListaProductosListaDeProductosNoEsNula()
        {
            var mockVentas = new Mock<IVentasService>();
            mockVentas.Setup(servicio => servicio.ObtenerTodosLosProductos()).Returns(new List<ProductoModel>());
            VentasController ventasController = new VentasController(mockVentas.Object);

            ViewResult vistaResultado = ventasController.ListaProductos() as ViewResult;
            var listaProductos = vistaResultado.ViewBag.ListaProductos;

            Assert.IsNotNull(listaProductos);
        }

        [TestMethod]
        public void ListaProductosListaDeProductosEsTipoLista()
        {
            var mockVentas = new Mock<IVentasService>();
            mockVentas.Setup(servicio => servicio.ObtenerTodosLosProductos()).Returns(new List<ProductoModel>());
            VentasController ventasController = new VentasController(mockVentas.Object);

            ViewResult vistaResultado = ventasController.ListaProductos() as ViewResult;
            var listaProductos = vistaResultado.ViewBag.ListaProductos;

            Assert.IsInstanceOfType(listaProductos, typeof(List<ProductoModel>));
        }

        [TestMethod]
        public void ListaProductosListaDeProductosNoTienesNulos()
        {
            var mockVentas = new Mock<IVentasService>();
            var listaProductos = new List<ProductoModel>() {
                new ProductoModel()
            };
            mockVentas.Setup(servicio => servicio.ObtenerTodosLosProductos()).Returns(listaProductos);
            VentasController ventasController = new VentasController(mockVentas.Object);

            ViewResult vistaResultado = ventasController.ListaProductos() as ViewResult;
            listaProductos = vistaResultado.ViewBag.ListaProductos;

            CollectionAssert.AllItemsAreNotNull(listaProductos);
        }

        [TestMethod]
        public void VerCarritoDelUsuarioNoEsNulo()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "diazfonseca.diego@gmail.com";
            mockVentas.Setup(servicio => servicio.ObtenerTodosLosProductosDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerTodasLasEntradasDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeProductosDelCarrito(correoUsuario)).Returns(5);
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeEntradasDelCarrito(correoUsuario)).Returns(5);
            VentasController ventasController = new VentasController(mockVentas.Object);

            ViewResult vistaResultado = ventasController.VerCarritoDelUsuario(correoUsuario) as ViewResult;

            Assert.IsNotNull(vistaResultado);
        }

        [TestMethod]
        public void VerCarritoDelUsuarioRetornaTotalCorrecto()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "diazfonseca.diego@gmail.com";
            mockVentas.Setup(servicio => servicio.ObtenerTodosLosProductosDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerTodasLasEntradasDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeProductosDelCarrito(correoUsuario)).Returns(5);
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeEntradasDelCarrito(correoUsuario)).Returns(5);
            VentasController ventasController = new VentasController(mockVentas.Object);
            double precioEsperado = 10;

            ViewResult vistaResultado = ventasController.VerCarritoDelUsuario(correoUsuario) as ViewResult;
            double precioRecivido = vistaResultado.ViewBag.PrecioTotal;

            Assert.AreEqual(precioEsperado, precioRecivido);
        }

        [TestMethod]
        public void VerCarritoDelUsuarioRetornaListaProductosNoNula()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "diazfonseca.diego@gmail.com";
            mockVentas.Setup(servicio => servicio.ObtenerTodosLosProductosDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerTodasLasEntradasDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeProductosDelCarrito(correoUsuario)).Returns(5);
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeEntradasDelCarrito(correoUsuario)).Returns(5);
            VentasController ventasController = new VentasController(mockVentas.Object);

            ViewResult vistaResultado = ventasController.VerCarritoDelUsuario(correoUsuario) as ViewResult;
            var listaProductos = vistaResultado.ViewBag.ListaProductos;

            Assert.IsNotNull(listaProductos);
        }

        [TestMethod]
        public void VerCarritoDelUsuarioRetornaListaEntradasNoNula()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "diazfonseca.diego@gmail.com";
            mockVentas.Setup(servicio => servicio.ObtenerTodosLosProductosDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerTodasLasEntradasDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeProductosDelCarrito(correoUsuario)).Returns(5);
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeEntradasDelCarrito(correoUsuario)).Returns(5);
            VentasController ventasController = new VentasController(mockVentas.Object);

            ViewResult vistaResultado = ventasController.VerCarritoDelUsuario(correoUsuario) as ViewResult;
            var listaEntradas = vistaResultado.ViewBag.ListaEntradas;

            Assert.IsNotNull(listaEntradas);
        }

        [TestMethod]
        public void VerCarritoDelUsuarioRetornaTotalNoNulo()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "danielmonge25@hotmail.com";
            mockVentas.Setup(servicio => servicio.ObtenerTodosLosProductosDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerTodasLasEntradasDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeProductosDelCarrito(correoUsuario)).Returns(5);
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeEntradasDelCarrito(correoUsuario)).Returns(5);
            VentasController ventasController = new VentasController(mockVentas.Object);

            ViewResult vistaResultado = ventasController.VerCarritoDelUsuario(correoUsuario) as ViewResult;
            var total = vistaResultado.ViewBag.PrecioTotal;

            Assert.IsNotNull(total);
        }

        [TestMethod]
        public void VerCarritoListaDeProductosEsTipoLista()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "danielmonge25@hotmail.com";
            mockVentas.Setup(servicio => servicio.ObtenerTodosLosProductosDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerTodasLasEntradasDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeProductosDelCarrito(correoUsuario)).Returns(5);
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeEntradasDelCarrito(correoUsuario)).Returns(5);
            VentasController ventasController = new VentasController(mockVentas.Object);

            ViewResult vistaResultado = ventasController.VerCarritoDelUsuario(correoUsuario) as ViewResult;
            var listaProductos = vistaResultado.ViewBag.ListaProductos;

            Assert.IsInstanceOfType(listaProductos, typeof(List<ProductoModel>));
        }

        [TestMethod]
        public void VerCarritoListaDeEntradasEsTipoLista()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "danielmonge25@hotmail.com";
            mockVentas.Setup(servicio => servicio.ObtenerTodosLosProductosDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerTodasLasEntradasDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeProductosDelCarrito(correoUsuario)).Returns(5);
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeEntradasDelCarrito(correoUsuario)).Returns(5);
            VentasController ventasController = new VentasController(mockVentas.Object);

            ViewResult vistaResultado = ventasController.VerCarritoDelUsuario(correoUsuario) as ViewResult;
            var listaEntradas = vistaResultado.ViewBag.ListaEntradas;

            Assert.IsInstanceOfType(listaEntradas, typeof(List<ProductoModel>));
        }

        [TestMethod]
        public void VerCarritoPrecioTotalsEsTipoDouble()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "danielmonge25@hotmail.com";
            mockVentas.Setup(servicio => servicio.ObtenerTodosLosProductosDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerTodasLasEntradasDelCarrito(correoUsuario)).Returns(new List<ProductoModel>());
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeProductosDelCarrito(correoUsuario)).Returns(5);
            mockVentas.Setup(servicio => servicio.ObtenerPrecioTotalDeEntradasDelCarrito(correoUsuario)).Returns(5);
            VentasController ventasController = new VentasController(mockVentas.Object);

            ViewResult vistaResultado = ventasController.VerCarritoDelUsuario(correoUsuario) as ViewResult;
            var total = vistaResultado.ViewBag.PrecioTotal;

            Assert.IsInstanceOfType(total, typeof(double));
        }

        [TestMethod]
        public void EliminarElementoDelCarritoDelUsuarioNoDevuelveNulo()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "diazfonseca.diego@gmail.com";
            int idComprable = 5;
            mockVentas.Setup(servicio => servicio.EliminarDelCarrito(correoUsuario,idComprable)).Returns(false);
            VentasController ventasController = new VentasController(mockVentas.Object);

            JsonResult resultado = ventasController.EliminarElementoDelCarritoDelUsuario(correoUsuario, idComprable);

            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void EliminarElementoDelCarritoDelUsuarioDevuelveFalsoCuandoFalla()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "diazfonseca.diego@gmail.com";
            int idComprable = 5;
            mockVentas.Setup(servicio => servicio.EliminarDelCarrito(correoUsuario, idComprable)).Returns(false);
            VentasController ventasController = new VentasController(mockVentas.Object);

            JsonResult resultado = ventasController.EliminarElementoDelCarritoDelUsuario(correoUsuario, idComprable);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string dataRecibida = serializer.Serialize(resultado.Data);


            Assert.AreEqual(dataRecibida, "{\"Exito\":false}");
        }


        [TestMethod]
        public void EliminarElementoDelCarritoDelUsuarioDevuelveVerdaderoCuandoCumple()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "diazfonseca.diego@gmail.com";
            int idComprable = 5;
            mockVentas.Setup(servicio => servicio.EliminarDelCarrito(correoUsuario, idComprable)).Returns(true);
            VentasController ventasController = new VentasController(mockVentas.Object);

            JsonResult resultado = ventasController.EliminarElementoDelCarritoDelUsuario(correoUsuario, idComprable);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string dataRecibida = serializer.Serialize(resultado.Data);


            Assert.AreEqual(dataRecibida, "{\"Exito\":true}");
        }

        [TestMethod]
        public void DisminiuirLaCantidadDelElementoDelCarritoDelUsuarioDevuelveFalsoCuandoFalla()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "diazfonseca.diego@gmail.com";
            int idComprable = 5;
            mockVentas.Setup(servicio => servicio.DisminiuirLaCantidadDelElementoDelCarrito(correoUsuario, idComprable)).Returns(false);
            VentasController ventasController = new VentasController(mockVentas.Object);

            JsonResult resultado = ventasController.DisminiuirLaCantidadDelElementoDelCarritoDelUsuario(correoUsuario, idComprable);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string dataRecibida = serializer.Serialize(resultado.Data);


            Assert.AreEqual(dataRecibida, "{\"Exito\":false}");
        }

        [TestMethod]
        public void DisminiuirLaCantidadDelElementoDelCarritoDelUsuarioDevuelveVerdaderoCuandoCumple()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "diazfonseca.diego@gmail.com";
            int idComprable = 5;
            mockVentas.Setup(servicio => servicio.DisminiuirLaCantidadDelElementoDelCarrito(correoUsuario, idComprable)).Returns(true);
            VentasController ventasController = new VentasController(mockVentas.Object);

            JsonResult resultado = ventasController.DisminiuirLaCantidadDelElementoDelCarritoDelUsuario(correoUsuario, idComprable);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string dataRecibida = serializer.Serialize(resultado.Data);

            Assert.AreEqual(dataRecibida, "{\"Exito\":true}");
        }

        [TestMethod]
        public void AumentarLaCantidadDelElementoDelCarritoDelUsuarioDevuelveFalsoCuandoFalla()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "diazfonseca.diego@gmail.com";
            int idComprable = 5;
            mockVentas.Setup(servicio => servicio.AumentarLaCantidadDelElementoDelCarrito(correoUsuario, idComprable)).Returns(false);
            VentasController ventasController = new VentasController(mockVentas.Object);

            JsonResult resultado = ventasController.AumentarLaCantidadDelElementoDelCarritoDelUsuario(correoUsuario, idComprable);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string dataRecibida = serializer.Serialize(resultado.Data);


            Assert.AreEqual(dataRecibida, "{\"Exito\":false}");
        }

        [TestMethod]
        public void AumentarLaCantidadDelElementoDelCarritoDelUsuarioDevuelveVerdaderoCuandoCumple()
        {
            var mockVentas = new Mock<IVentasService>();
            string correoUsuario = "diazfonseca.diego@gmail.com";
            int idComprable = 5;
            mockVentas.Setup(servicio => servicio.AumentarLaCantidadDelElementoDelCarrito(correoUsuario, idComprable)).Returns(true);
            VentasController ventasController = new VentasController(mockVentas.Object);

            JsonResult resultado = ventasController.AumentarLaCantidadDelElementoDelCarritoDelUsuario(correoUsuario, idComprable);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string dataRecibida = serializer.Serialize(resultado.Data);

            Assert.AreEqual(dataRecibida, "{\"Exito\":true}");
        }

        [TestMethod]
        public void AgregarAlCarritoDevuelveFalsoCuandoFalla()
        {
            var mockVentas = new Mock<IVentasService>();
            int cantidad = 5;
            int idComprable = 5;
            mockVentas.Setup(servicio => servicio.AgregarAlCarrito(idComprable, cantidad)).Returns(false);
            VentasController ventasController = new VentasController(mockVentas.Object);

            JsonResult resultado = ventasController.AgregarAlCarrito(idComprable, cantidad);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string dataRecibida = serializer.Serialize(resultado.Data);


            Assert.AreEqual(dataRecibida, "{\"Exito\":false}");
        }

        [TestMethod]
        public void AgregarAlCarritoDevuelveVerdaderoCuandoCumple()
        {
            var mockVentas = new Mock<IVentasService>();
            int cantidad = 5;
            int idComprable = 5;
            mockVentas.Setup(servicio => servicio.AgregarAlCarrito(idComprable, cantidad)).Returns(true);
            VentasController ventasController = new VentasController(mockVentas.Object);

            JsonResult resultado = ventasController.AgregarAlCarrito(idComprable, cantidad);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string dataRecibida = serializer.Serialize(resultado.Data);

            Assert.AreEqual(dataRecibida, "{\"Exito\":true}");
        }

        [TestMethod]
        public void AgregarProductoNoDevuelveVistaNula()
        {
            var mockVentas = new Mock<IVentasService>();
            VentasController ventasController = new VentasController(mockVentas.Object);

            ViewResult vistaResultado = ventasController.AgregarProducto() as ViewResult;

            Assert.IsNotNull(vistaResultado);
        }

        [TestMethod]
        public void AgregarProductoTieneMensajeCorrectoAlInsertar()
        {
            var mockVentas = new Mock<IVentasService>();
            ProductoModel producto = new ProductoModel {
                Id = 1,
                Nombre = "Diego",
                Precio = 123456,
                CantidadDisponible = 12,
                CantidadRebastecer = 12,
                Tamano = "Grande",
                Categoria = "Categoria",
                Descripcion = "Descripción",
                FechaIngreso = "12/12/12",
                FechaUltimaVenta = "12/12/12"
            };
            mockVentas.Setup(servicio => servicio.InsertarProducto(producto)).Returns(true);
            VentasController ventasController = new VentasController(mockVentas.Object);

            ViewResult vistaResultado = ventasController.AgregarProducto(producto) as ViewResult;
            string mensaje = vistaResultado.ViewBag.Mensaje;

            Assert.AreEqual(mensaje, "El producto " + producto.Nombre + " fue agregado con éxito");
        }

        [TestMethod]
        public void AgregarProductoTieneMensajeCorrectoCuandoFallaInsertar()
        {
            var mockVentas = new Mock<IVentasService>();
            ProductoModel producto = new ProductoModel
            {
                Id = 1,
                Nombre = "Diego",
                Precio = 123456,
                CantidadDisponible = 12,
                CantidadRebastecer = 12,
                Tamano = "Grande",
                Categoria = "Categoria",
                Descripcion = "Descripción",
                FechaIngreso = "12/12/12",
                FechaUltimaVenta = "12/12/12"
            };
            mockVentas.Setup(servicio => servicio.InsertarProducto(producto)).Returns(false);
            VentasController ventasController = new VentasController(mockVentas.Object);

            ViewResult vistaResultado = ventasController.AgregarProducto(producto) as ViewResult;
            string mensaje = vistaResultado.ViewBag.Mensaje;

            Assert.AreEqual(mensaje, "Hubo un error en el servidor");
        }
    }
}