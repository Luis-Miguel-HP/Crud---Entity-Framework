using Api_Usuario.DTO;
using Api_Usuario.Modelo;
using Microsoft.AspNetCore.Mvc;


namespace Api_Usuario.Servicios

{
    public interface IProducto
    {
        Task<Respuesta<string>> InformacionProducto();
        Task<Respuesta<List<ProductoDTO>>> ProductosCategoriaEspecifica(int idproducto);
        Task<Respuesta<List<ProductoDTO>>> ProductosProveedorEspecifica(string nombreProveedor);
        Task<Respuesta<string>> cantidadTotalProducto();
    }
}
