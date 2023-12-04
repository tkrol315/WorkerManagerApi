using System.Text.Json.Serialization;

namespace WorkerManager.Application.Dto
{
    public class GetManagerDto : GetUserDto
    {
        [JsonPropertyOrder(1)]
        public List<GetTaskManagerDto> Tasks { get; set; }
    }
}
