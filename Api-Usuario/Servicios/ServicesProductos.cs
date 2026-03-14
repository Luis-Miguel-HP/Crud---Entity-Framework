using Api_Usuario.Context;
using Api_Usuario.DTO;
using Api_Usuario.Modelo;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api_Usuario.Servicios
{
    public class ServicesProductos : IProducto
    {
        private readonly UsuarioContext _Producto;

        public ServicesProductos(UsuarioContext producto)
        {
            _Producto = producto;
        }

        public  async Task<Respuesta<string>> InformacionProducto()
        {
            var respuesta = new Respuesta<string>();


            try
            {
                var precioMasAlto =  _Producto.Producto.Max(p => p.Precio);
                var precioMasBajo = _Producto.Producto.Min(p => p.Precio);
                var todosLosPrecios = _Producto.Producto.Sum(p => p.Precio);
                var precioPromedio = _Producto.Producto.Average(p => p.Precio);

                respuesta.Successful = true;
                respuesta.Message = $"Precio más alto: {precioMasAlto:C2}" + "||" +
                    $"Precio más bajo: {precioMasBajo:C2} " + "||" +
                    $"Promedio de precios: {precioPromedio:C2} " + "||" +
                    $"Suma total: {todosLosPrecios:C2}";
                return respuesta;

            }
            catch (Exception) {
            
                respuesta.Successful = false;
                respuesta.Message = "No se puedo realizar la operacion";
                return respuesta ;

            }

        }

        public async Task<Respuesta<List<ProductoDTO>>> ProductosCategoriaEspecifica(int idCategoria)
        {
            var respuesta = new Respuesta<List<ProductoDTO>>();

            try
            {
                var productos = await _Producto.Producto
                .Where(p => p.IdCategoria == idCategoria)
                .Select(pr => new ProductoDTO
                {
                    id = pr.Id,
                    NombreProducto = pr.NombreProducto,
                    Precio = pr.Precio,

                }).ToListAsync();


                respuesta.SingleData = productos;
                return respuesta;
            }
            catch (Exception)
            {
                throw;

            }
        }

        public async Task<Respuesta<List<ProductoDTO>>> ProductosProveedorEspecifica(string nombreProveedor)
        {
            var respuesta = new Respuesta <List<ProductoDTO>>();

            try
            {
                var proveedor = await _Producto.Proveedor.FirstOrDefaultAsync(p => p.NombreProvedor.ToLower() == nombreProveedor.ToLower());

                if (proveedor == null)
                {
                    throw new Exception("El proveedor no existe dentro de la lista de proveedores");
                }
                else
                {
                    var productos = await _Producto.Producto
                                    .Where(p => p.IdProveedor == proveedor.Id)
                                    .Select(p => new ProductoDTO
                                    {
                                        id = p.Id,
                                        NombreProducto = p.NombreProducto,
                                        Precio = p.Precio,

                                    }).ToListAsync();
                    respuesta.SingleData = productos;
                }
                return respuesta;
               
                }
            
            catch (Exception) {

                throw;
            }
        }



        public async Task<Respuesta<string>> cantidadTotalProducto()
        {
          var resp = new Respuesta<string>();

            try
            {
                var cantidadTotal = _Producto.Producto.Count();
                resp.Successful = true;
                resp.Message = $"La cantidad total de productos registrado es: {cantidadTotal}";
                return resp;

            }
            catch (Exception) {
                resp.Successful = false;
                throw;
            }
        }

    }


}
