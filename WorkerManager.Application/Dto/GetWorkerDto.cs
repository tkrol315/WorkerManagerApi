using System.Text.Json.Serialization;

namespace WorkerManager.Application.Dto
{
    public class GetWorkerDto : GetUserDto
    {
        [JsonPropertyOrder(1)]
        public GetTaskWorkerDto? AssignedTask { get; set; }
    }
}
