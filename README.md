# Idempotent Workflow Application

A deterministic workflow application that uses a set of generic workflow classes to support creating, updatating and deleting information. When changes are requested they are never applied to the live record until approved. Workflows once submitted cant be modified and can only be approved or rejected. Addtional workflow requests can not be submitted to a live record until pending workflows (UPDATE, DELETE) are approved or rejected.

## Stack
1. SqlLite 
2. Fluent NHibernate
2. Angular MvcWebApi/Angular-UI
3. Web Api 2.0 
4. C# 4.5

## Workflow Types
### Create Workflow
On approval creates a record in the live record.

### Delete Workflow
On approval sets an Archive flag on the live record.

### Update Workflow
On approval updates the live record

## Improvements
1. No record level locking on the edit, delete approval and rejection requests
2. Fair amount of boiler plate code in controllers, BL and DalMapping.

## User Interface
![User Interface](https://github.com/JonathanSawyer/WorkflowApplication/blob/master/Angular-UI.png)
