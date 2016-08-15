# Idempotent Workflow Application

A simple application to show how to create a set of generic workflow classes that support a system that only allows for a single request at a time for changes to a live or active record. There are three types of changes namely Create, Delete and Update. When changes are requested they are never applied to the live record until approved. 

## Stack
1. SqlLite with flient NHibernate, by default the system is setup to generate the database.
2. Angular front end to try things out. This can be accessed by running the MvcWebApi project and navigating to /Angular-UI
3. The backend is exposed through Web Api 2.0 and is written in C# 4.5

## Workflow Types
### Create Workflow
A create worklow only enters the live data table once its been approved.

### Delete Workflow
An approved delete workflow sets an archive flag to true on the live table. This can be modified by relaxing the foreign key constrains and thereby completly remove the record. The history will still be stored in the associated workflow table.

### Update Workflow
Changes are only applied to a live record once approved

## Improvements
1. There is not currently record level locking on the edit, delete approval and rejection requests
2. Fair amount of boiler plate code in controllers, BL and DalMapping.
