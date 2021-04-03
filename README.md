# SSCS
Simple Student Card System

## Description
Main Modules are: ClientMVVM and ServerAPI. 
Server side require SQL connection string in appsettings.json file (CHANGE_ME - default value).
Currently for testing purposes it automatically creates database & imports test schedule from "C:\code\temp\test.ics" file.

To test connection and other mechanisms, there are special endpoints in API that create/remove student who can gather schedule (identification based on Student Card ID). 
