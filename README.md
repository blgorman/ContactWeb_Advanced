# ContactWeb_Advanced
This is ContactWeb with advanced rework applied

The first commit is all of the changes from the original ContactWeb applied to do the following
1) separated db calls to a service
2) created a models project for db work
3) fixed issue with multiple db context by correcting namespaces
4) Removed any unnecessary files from tracking
5) updated to use SSL for facebook authentication
6) Created viewmodels project to disconnect the db models from direct use on the views
7) Added an address model, with relation to the original contact so the solution now allows for multiple contact addresses
8) Demonstrated use of Partial views and editor templates
9) Bootstrap modal integration

In general, where the original course and code is a nice "starter" the advanced course and this codebase is much closer to what
we would encounter and view in a real-world application.


Things that are still missing that I would like to add in the future:
1) Dependency Injection
2) Automapper


THINGS to do to get this project to work:
1) Download/Clone, get the files from a release
2) Remove the target for Roslyn CSC from the contactweb.csproj file [lines 453-459] 
3) Set your connection strings and other private keys into the web.config file
4) Run two update-database commands - 1 for each migration context [identity and web]
	a) update-database -configurationtypename ContactWeb.Migrations.Identity.ApplicationDbConfiguration
	b) update-database -configurationtypename ContactWeb.Migrations.ContactWebDb.ContactWebConfiguration
	
	
Once you have completed those steps, you should be able to work with the project and review the sections on adding tools at will
