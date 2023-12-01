using System;
using WorkerManager.Domain.Entities;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Application.Dto
{
    public class CreateTaskDto
    {
        public TaskName Name { get; set; }
        public TaskDescription Description { get; set; }
      
    }
}
