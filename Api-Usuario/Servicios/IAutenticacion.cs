namespace Api_Usuario.Servicios
{
    public interface IAutenticacion
    {

        //este metodo devuelve el token en un string
        Task<string> Autenticacion(string username, string contraseña);
    }
}
