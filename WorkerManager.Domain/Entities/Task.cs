﻿namespace WorkerManager.Domain.Entities
{
    public class Task
    {
        
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public Manager Manager { get;  set; }
        public Guid ManagerId { get; set; } 
        public Worker? Worker {  get;   set; }
        public Guid? WorkerId { get; set; }
        public Guid? CompletedByWorkerWithId { get; set; }
        public Enums.TaskStatus TaskStatus { get;  set; } = Enums.TaskStatus.NotAssigned;

      
    }
}
