# WorkerManagerApi

WorkerManagerApi provides capability for managers to
effectively manage tasks, including creating, editing, assigning
them to employees, and monitoring the completion status of tasks.
Employees, on the other hand, have the ability to mark tasks as completed,
enabling them to be assigned new tasks.

## Account Controller

- **Register**: Allows users to regiser.
  - **Method**: POST
  - **Endpoint**: `/api/account`
  - 
- **Login**: Allows users to login.
  - **Method**: POST
  - **Endpoint**: `/api/account`
    
## Manager Controller

- **Get Manager with tasks**: Allows managers to get Manager by id with associated tasks.
  - **Method**: GET
  - **Endpoint**: `/api/managers/{id:guid}`

- **Get all Managers with tasks**: Allows managers to get all Managers with associated tasks.
  - **Method**: GET
  - **Endpoint**: `/api/managers`

 ## Task Controller

 - **Get Manager's tasks by id**: Allows managers to get list of Manager's tasks by id.
   - **Method**: GET
   - **Endpoint**: `api/managers/{id:guid}/tasks`

- **Get Manager's task by task name**: Allows managers to get Manager's task by task name.
  - **Method**: GET
  - **Endpoint**: `api/managers/{id:guid}/tasks/{taskName}`
 
- **Create task**: Allows manager to create new task.
  - **Method**: POST
  - **Endpoint**: `api/managers/{id:guid}/tasks`
 
- **Assign task to the worker**: Allows managers to assign task to the worker.
  - **Method**: POST
  - **Endpoint**: `api/managers/{id:guid}/tasks/{taskname}/assign`

- **Remove task by task name**: Allows managers to remove thier task by task name.
  - **Method**: DELETE
  - **Endpoint**: `api/managers/{id:guid}/tasks/{taskname}`
 
- **Update task by task name**: Allows managers to update their task by task name.
  - **Method**: PUT
  - **Endpoint**: `api/managers/{id:guid}/tasks/{taskname}`
 
## Worker Controller

- **Get all workers with assigned tasks**: Allows users to get all workers with their assigned tasks.
  - **Method**: GET
  - **Endpoint**: `api/workers`
    
- **Get worker with assigned task by id**: Allows users to get worker with their assigned task by id.
  - **Method**: GET
  - **Endpoint**: `api/workers/{id:guid}`

- **Mark task as completed**: Marks a task as completed and enables the manager to assign a new task to this worker.
  - **Method**: POST
  - **Endpoint**: `api/workers/{id:guid}/completedtask`


    
