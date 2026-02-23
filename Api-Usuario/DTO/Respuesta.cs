using Azure;

namespace Api_Usuario.DTO
{
    public class Respuesta
    {
        public bool ThereIsError => Errors.Any();
        public long EntityId { get; set; }
        public bool Successful { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>(0);
    }

    public class Respuesta<T> : Respuesta where T : class
    {
        public IEnumerable<T> DataList { get; set; }
        public T SingleData { get; set; }
    }
}
