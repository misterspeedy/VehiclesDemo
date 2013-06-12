VehiclesDemo
============

A demo of the F# SQL Server Type Provider

Setup
=====

This demo needs a SQL Server database.  To set up:

- Unzip the file Data\databasesetup.zip
- Edit it (it's big) and change values in the first few lines to match your SQL Server setup.
- Run it using SQLCmd - eg. sqlcmd -S myserver -i databasesetup.sql
- Edit the connection string in Vehicles.fs to reference the location where the database was set up.
