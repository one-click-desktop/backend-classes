# OneClickDesktop Backend Classes

C# library containing common classes used by other modules. 

Library consists of namespaces:

- `OneClickDesktop.BackendClasses.Model`: contains classes used for storing and management of system model. All functions perform actions only on model. All other actions, like data transfer, need to be performed manually. Methods perform basic validation to preserve model integrity. Business logic validation needs to be performed manually.

- `OneClickDesktop.BackendClasses.Communication`: contains classes describing messages and data needed to properly deserialize them.

