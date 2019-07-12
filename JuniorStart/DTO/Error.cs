using Newtonsoft.Json;

namespace JuniorStart.DTO
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}