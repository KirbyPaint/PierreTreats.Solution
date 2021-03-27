# _Pierre's Sweet and Savory Treats_

#### _Web application to demonstrate knowledge of MVC, Many-to-Many database relationships, and Authentication_

#### By _**Ash Porter (KirbyPaint)**_

## Description

_The purpose of this web application is to demonstrate knowledge of how MVC concepts tie in to databases with many-to-many relationships, as well as implementing authorization with Identity, and creating a program that creates and uses this data effectively._  

## Setup/Installation Requirements

### Cloning with Git:
* Open Git Bash, or your preferred terminal
* Navigate to your directory for Git projects (not within an existing project)
* Type the following: `git clone https://github.com/KirbyPaint/PierreTreats.Solution.git`

This program was built with and requires .NET version 5.0.102. You may install the 64-bit version for Windows [using this link](https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.102-windows-x64-installer)  

Once the installation of .NET 5 is complete, you may check that the proper version was installed by opening up Git Bash and typing:  

`dotnet --version`  

Then, open your Git Bash terminal and navigate to:

`C:<filepath the files are installed at>\PierreTreats.Solution`

where "filepath the files are installed at" will be the location you saved your copy of the project at, or alternatively:  

You may navigate to the folder in the project labeled "PierreTreats.Solution".  
Right-click inside the File Explorer window, and in the right-click menu, choose "Open Git Bash Here," or Shift+Right-click and choose "Open Powershell Window Here."  
This will open a Powershell/Git Bash window that is already inside the proper directory.

[Click here for tips on navigating the terminal](https://docs.microsoft.com/en-us/powershell/scripting/samples/managing-current-location?view=powershell-7.1)

### Configuration File Setup

First, you will need to ensure you navigate into the `\PierreTreats.Solution\PierreTreats` directory. Create a file named `appsettings.json` and paste the following code into the newly created `appsettings.json` file:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=[DATABASE_NAME];uid=root;pwd=[YOUR PASSWORD];"
  }
}
```

You will then need to make at least TWO changes to the appsettings.json file:  
Where the text says `database=[DATABASE_NAME]`, enter your own database's name, and remove the brackets. If your database was named `my_database` this code will look like `database=my_database`  
Where the text says `pwd=[YOUR PASSWORD]`, enter your own secure password, and remove the brackets. If your password is `SafePassword123` this code will look like `pwd=SafePassword123`  
This ensures that the program will be able to read and write to your own local database.

### After everything is set up

Once you have properly navigated to the project directory (`<your directory>\PierreTreats.Solution\PierreTreats`), your appsettings.json file has been created, and your local server has been set up, type:

`dotnet restore`

The program should automatically restore all necessary packages.  

Note: This program does come with a "Migrations" folder, but if that folder is missing, please run the following command:

`dotnet ef migrations add Initial`

If the Migrations folder already exists in the project structure, skip this step and enter this next command:

`dotnet ef database update`

to fully apply the database structure.

Once all of the previous steps are applied, enter the following:

`dotnet run`

This will run the web application on a local server. Look for terminal output containing these lines:  

```
Now listening on: http://localhost:5000
Now listening on: https://localhost:5001
Application started. Press Ctrl+C to shut down.
```

Once you see this in the terminal, you will be able to open the web page as a link in your browser. Copy either URL and paste it in the browser. If you choose the `https://localhost:5001` link, the browser will likely attempt to protect you by informing you this site has no certificates. That is true; simply bypass the site's warning, or use the unsecured hyperlink `http://localhost:5000` instead.  

## How To Use The Program

Upon opening the program, you will see a landing page with links to listing all flavors, all treats, as well as a bottom navigation bar that will change whether you are logged in or not.  

First, you will want to create an account, for maximum accessibility. Users who are not logged in can only view items, but not create, edit, or delete them. The password you login with must contain at least six characters, a capital letter, a digit, and a special character.  

Once you have an account, the full site is available to you. You will be able to navigate into either the Flavors or the Treats to create, edit, view, and delete new items. Additionally, if you are logged in and at the Home Page, the Login hotbar will be replaced with an "Account Management" link where you can log out.  

Clicking on the Flavors or Treats links will take you to a page where you can view all of the items that have been added. If you are logged in, you will see your items. If you are not logged in, you will be able to see all items.  

Clicking on a Flavor or Treat will enable you to view all associated items. For example, if you have a Flavor named Chocolate, and you've attributed the Chocolate flavor to a Cake and Cupcake Treat, then the Chocolate listing will show both the Cake and Cupcake options. Conversely, if you navigate to the Treats page and select that Cupcake, you would be able to see that it has a Chocolate flavor attributed.  

From the Details of either a Flavor or a Treat, you are able to Edit, Delete, or Add a new Treat or Flavor (respective to the original category you chose). Also from the Details screen, you can remove an attributed Flavor or Treat. 

## Known Bugs/Issues

Users can add duplicate Treats to Flavors and vice-versa.  
CSS could be improved.  

## Support and contact details

_Discord: @KirbyPaint#0751_

## Technologies Used

_C#, .NET 5, ASP.NET MVC, MySQL and MySQL Workbench, Bootstrap (CSS), Authorization with Identity, Entity Framework Core_

### License Information

_GNU Public License_

Copyright (c) 2021 **_KirbyPaint_**