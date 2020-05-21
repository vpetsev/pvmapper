README

pvDesktopSetup.iss requires the Inno Setup QuickStart Pack to edit and run.
Direct Download Link: http://www.jrsoftware.org/download.php/ispack.exe?site=1

The script needs to stay in the project's root folder because it needs access
to the Application folder from outside of it.

For additional versions, line 10 needs to be changed for the new version number.  
	Line 18 also needs to be changed to reflect the new version number.

The rest should not need to be changed unless a new version of .Net is required
  or other source files need to be added that are not in the pvDesktop/Application folder.
  For reference, the script code is Pascal. 

The compiled file will be placed in the pvDesktop/Output file. It will need to be added to source control manually.