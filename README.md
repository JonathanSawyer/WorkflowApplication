# Idempotent Workflow Application

A simple application to show how to create a set of generic workflow classes. This can be useful when workflowing very simple information and with a few modifications it can be scaled to handle more complex cases. 

## Stack
1. SqlLite with flient NHibernate, by default the system is setup to generate the database.
2. Angular front end to try things out. This can be accessed by running the MvcWebApi project and navigating to /Angular-UI
3. The backend is exposed through Web Api 2.0 and is written in C# 4.5
