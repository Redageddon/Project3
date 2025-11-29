<div id="top">

<!-- HEADER STYLE: CLASSIC -->
<div align="center">
<img width="30%" style="position: relative; top: 0; right: 0;" alt="Project Logo" src="https://github.com/user-attachments/assets/c3914c14-d342-4040-acde-af8dd7b0d7e1" />


# CMPS3390-PROJECT3

<em>Transforming Ideas into Impactful Innovation Daily</em>

<!-- BADGES -->
<img src="https://img.shields.io/github/last-commit/Redageddon/Project3?style=flat&logo=git&logoColor=white&color=0080ff" alt="last-commit">
<img src="https://img.shields.io/github/languages/top/Redageddon/Project3?style=flat&color=0080ff" alt="repo-top-language">
<img src="https://img.shields.io/github/languages/count/Redageddon/Project3?style=flat&color=0080ff" alt="repo-language-count">

<em>Built with the tools and technologies:</em>

<img src="https://img.shields.io/badge/JSON-000000.svg?style=flat&logo=JSON&logoColor=white" alt="JSON">
<img src="https://img.shields.io/badge/Markdown-000000.svg?style=flat&logo=Markdown&logoColor=white" alt="Markdown">
<img src="https://img.shields.io/badge/JavaScript-F7DF1E.svg?style=flat&logo=JavaScript&logoColor=black" alt="JavaScript">
<img src="https://img.shields.io/badge/NuGet-004880.svg?style=flat&logo=NuGet&logoColor=white" alt="NuGet">
<img src="https://img.shields.io/badge/Python-3776AB.svg?style=flat&logo=Python&logoColor=white" alt="Python">

</div>
<br>

---

## Table of Contents

- [Overview](#overview)
- [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
    - [Usage](#usage)
    - [Testing](#testing)
- [Features](#features)
- [Project Structure](#project-structure)
    - [Project Index](#project-index)

---

## Overview

CMPS3390-Project3 is a full-stack developer toolkit for building food and meal planning applications with a focus on modularity, security, and testability. It combines robust API endpoints, JSON data repositories, and a dynamic web interface to streamline development workflows.

**Why CMPS3390-Project3?**

This project aims to simplify the creation of scalable, maintainable food management systems. The core features include:

- **API Services:** Seamless CRUD operations for recipes, meals, users, and planners.
- **Extensive Testing:** Comprehensive unit and integration tests ensure reliability.
- **JSON Data Storage:** Easy management of recipes, meals, and user data.
- **Secure Authentication:** Robust session management and user login workflows.
- **Dynamic Web UI:** Razor views, CSS, and JavaScript for an engaging user experience.

---

## Features

|      | Component       | Details                                                                                     |
| :--- | :-------------- | :------------------------------------------------------------------------------------------ |
| ⚙️  | **Architecture**  | <ul><li>ASP.NET Core MVC framework for server-side rendering</li><li>Model-View-Controller pattern</li><li>Multiple Razor (.cshtml) views for UI</li></ul> |
| 🔩 | **Code Quality**  | <ul><li>Uses C# with organized project structure</li><li>Includes partial views for reusability</li><li>Consistent naming conventions</li></ul> |
| 📄 | **Documentation** | <ul><li>Basic README with project overview</li><li>Contains license and settings files</li><li>No extensive inline code comments observed</li></ul> |
| 🔌 | **Integrations**  | <ul><li>NuGet package manager for dependencies</li><li>JSON files for data (meals.json, recipes.json, users.json)</li><li>HTML/CSS/JavaScript for frontend assets</li></ul> |
| 🧩 | **Modularity**    | <ul><li>Uses partial views (_layout.cshtml, _recipecreateform.cshtml, etc.) for component reuse</li><li>Separate JSON files for data management</li></ul> |
| 🧪 | **Testing**       | <ul><li>No explicit test projects or frameworks identified</li><li>Potential testing via project3.sln, but no dedicated test code observed</li></ul> |
| ⚡️  | **Performance**   | <ul><li>Source code suggests minimal client-side scripting</li><li>Static assets (CSS maps, JSON data) for fast load times</li></ul> |
| 🛡️ | **Security**      | <ul><li>Includes _loginrequiredmodal.cshtml indicating login/authentication features</li><li>Security best practices not explicitly detailed in codebase</li></ul> |
| 📦 | **Dependencies**  | <ul><li>Primary dependency on NuGet packages (API, project files)</li><li>Uses JSON and static assets for data</li></ul> |

---

## Project Structure

```sh
└── CMPS3390-Project3/
    ├── API
    │   ├── API.csproj
    │   ├── Controllers
    │   ├── Data
    │   ├── DataModels
    │   ├── Program.cs
    │   ├── Properties
    │   ├── Services
    │   └── appsettings.json
    ├── LICENSE
    ├── P3RecipesRefactor
    │   ├── bev-tags
    │   └── mealtypes
    ├── Project3
    │   ├── Controllers
    │   ├── Models
    │   ├── Program.cs
    │   ├── Project3.csproj
    │   ├── Properties
    │   ├── Services
    │   ├── Views
    │   └── wwwroot
    ├── Project3.sln
    ├── Project3.sln.DotSettings
    ├── README.md
    ├── Tests
    │   ├── ApiTests
    │   ├── CustomWebApplicationFactory.cs
    │   └── Tests.csproj
    ├── appsettings.json
    ├── package-lock.json
    └── wwwroot
        └── css
```

---

### Project Index

<details open>
	<summary><b><code>CMPS3390-PROJECT3/</code></b></summary>
	<!-- __root__ Submodule -->
	<details>
		<summary><b>__root__</b></summary>
		<blockquote>
			<div class='directory-path' style='padding: 8px 0; color: #666;'>
				<code><b>⦿ __root__</b></code>
			<table style='width: 100%; border-collapse: collapse;'>
			<thead>
				<tr style='background-color: #f8f9fa;'>
					<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
					<th style='text-align: left; padding: 8px;'>Summary</th>
				</tr>
			</thead>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/LICENSE'>LICENSE</a></b></td>
					<td style='padding: 8px;'>- Provides the licensing terms that govern the entire project, ensuring legal clarity and open-source distribution rights<br>- It establishes the permissions, limitations, and liabilities associated with the software, supporting its broad accessibility and reuse within the overall architecture<br>- This foundational document underpins the projects open-source ethos and legal framework.</td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3.sln.DotSettings'>Project3.sln.DotSettings</a></b></td>
					<td style='padding: 8px;'>- This configuration file defines default settings for code editing patterns within the project, specifically related to null check assertions<br>- It establishes whether custom expression and statement patterns can be used, and provides template placeholders for crafting assertion expressions<br>- Overall, it supports customizable and consistent code validation practices across the codebase, enhancing code quality and maintainability within the applications architecture.</td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3.sln'>Project3.sln</a></b></td>
					<td style='padding: 8px;'>- Defines the overall project structure, integrating core application logic, API endpoints, and testing components within a Visual Studio solution<br>- Facilitates organized development and deployment workflows, ensuring seamless interaction between modules<br>- Serves as the foundational blueprint that aligns the different parts of the system, supporting scalable and maintainable software architecture.</td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/README.md'>README.md</a></b></td>
					<td style='padding: 8px;'>- Provides an overview of Project3, outlining its core purpose within the overall architecture<br>- It highlights how the project facilitates key functionalities or workflows, ensuring clarity on its role in supporting the system’s objectives<br>- This summary emphasizes the contribution of Project3 to the broader codebase, guiding users in understanding its significance and integration points.</td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/appsettings.json'>appsettings.json</a></b></td>
					<td style='padding: 8px;'>- Defines application-wide configuration settings for logging and host management, ensuring consistent behavior across the entire project<br>- It establishes the logging levels for different components and specifies allowed hosts, facilitating secure and efficient operation within the overall architecture<br>- This setup supports streamlined deployment and monitoring of the application environment.</td>
				</tr>
			</table>
		</blockquote>
	</details>
	<!-- API Submodule -->
	<details>
		<summary><b>API</b></summary>
		<blockquote>
			<div class='directory-path' style='padding: 8px 0; color: #666;'>
				<code><b>⦿ API</b></code>
			<table style='width: 100%; border-collapse: collapse;'>
			<thead>
				<tr style='background-color: #f8f9fa;'>
					<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
					<th style='text-align: left; padding: 8px;'>Summary</th>
				</tr>
			</thead>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Program.cs'>Program.cs</a></b></td>
					<td style='padding: 8px;'>- Sets up the core web application infrastructure, configuring services, middleware, and routing for the API<br>- It establishes dependency injection for repositories and services, enables API documentation with Swagger, and applies CORS policies to facilitate secure client interactions<br>- This file orchestrates the startup process, ensuring the API is ready to handle requests within the overall system architecture.</td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/API.csproj'>API.csproj</a></b></td>
					<td style='padding: 8px;'>- Defines the core web API project setup for a.NET 9.0 application, establishing the framework, dependencies, and data assets<br>- Facilitates the development of RESTful endpoints to manage recipes, meals, planners, and users, serving as the foundational layer that enables interaction with the applications data-driven functionalities within the overall architecture.</td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/appsettings.json'>appsettings.json</a></b></td>
					<td style='padding: 8px;'>- Defines application-wide configuration settings for logging and host permissions, ensuring consistent behavior across the API<br>- It centralizes environment-specific parameters, facilitating effective monitoring and secure access management within the overall architecture<br>- This setup supports reliable operation and streamlined deployment of the API service.</td>
				</tr>
			</table>
			<!-- Services Submodule -->
			<details>
				<summary><b>Services</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ API.Services</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Services/UserRepository.cs'>UserRepository.cs</a></b></td>
							<td style='padding: 8px;'>- Provides core user data management functionalities, enabling retrieval, creation, updating, and deletion of user records stored in a JSON file<br>- Facilitates seamless integration of user data operations within the application architecture, ensuring data consistency and concurrency control<br>- Serves as the primary interface for user-related data access, supporting authentication and user management workflows.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Services/RecipesRepositry.cs'>RecipesRepositry.cs</a></b></td>
							<td style='padding: 8px;'>- Provides an interface for managing recipe data stored in a JSON file, enabling retrieval, creation, updating, and deletion of recipes<br>- Serves as the core data access layer within the architecture, ensuring thread-safe operations and persistent storage for recipe-related functionalities across the application.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Services/PasswordHasher.cs'>PasswordHasher.cs</a></b></td>
							<td style='padding: 8px;'>- Provides secure password management by hashing and verifying user credentials<br>- Ensures that passwords are stored in a non-reversible format, enhancing overall application security<br>- Integrates seamlessly into authentication workflows, supporting safe user login processes within the broader system architecture.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Services/UserAuth.cs'>UserAuth.cs</a></b></td>
							<td style='padding: 8px;'>- Facilitates user authentication by managing registration and login processes, ensuring secure handling of credentials through password hashing and validation<br>- Integrates with user data repositories to verify uniqueness and retrieve user information, supporting the overall security and user management architecture of the application.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Services/MealsRepository.cs'>MealsRepository.cs</a></b></td>
							<td style='padding: 8px;'>- Provides an interface for managing meal data within the application, enabling retrieval, creation, updating, and deletion of meal records stored in a JSON file<br>- Facilitates data persistence and consistency across the system, supporting core CRUD operations essential for the meal management feature in the overall architecture<br>- Ensures thread-safe access to shared data, maintaining data integrity.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Services/PlannerRepository.cs'>PlannerRepository.cs</a></b></td>
							<td style='padding: 8px;'>- Provides data access and management for meal planners within the application, enabling retrieval, creation, updating, and deletion of planner records<br>- Ensures data consistency through file-based storage and locking mechanisms, supporting user-specific and global planner operations<br>- Integrates seamlessly into the overall architecture by serving as the core repository layer for persistent planner data handling.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Services/SessionService.cs'>SessionService.cs</a></b></td>
							<td style='padding: 8px;'>- Manages user sessions by generating, validating, and invalidating session identifiers to facilitate secure and persistent user authentication within the application<br>- Ensures session expiration handling and user association, supporting seamless user experience and security across the systems architecture.</td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Properties Submodule -->
			<details>
				<summary><b>Properties</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ API.Properties</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Properties/launchSettings.json'>launchSettings.json</a></b></td>
							<td style='padding: 8px;'>- Defines the development environment configuration for the API project, enabling local testing and debugging<br>- It specifies how the application launches, including URLs, environment variables, and browser behavior, facilitating seamless startup and interaction with the APIs Swagger documentation within the overall architecture.</td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Controllers Submodule -->
			<details>
				<summary><b>Controllers</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ API.Controllers</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Controllers/UsersController.cs'>UsersController.cs</a></b></td>
							<td style='padding: 8px;'>- Provides RESTful API endpoints for managing user data within the application<br>- Facilitates retrieval, updating, and deletion of user information, integrating with the underlying data repository<br>- Serves as a key interface for client interactions with user resources, supporting core user management functionalities essential to the overall system architecture.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Controllers/AuthController.cs'>AuthController.cs</a></b></td>
							<td style='padding: 8px;'>- Provides authentication endpoints for user registration, login, and logout, facilitating secure user access management within the application<br>- Integrates user credential validation with session handling to enable authenticated interactions, forming a core component of the overall security and user management architecture.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Controllers/MealsApiController.cs'>MealsApiController.cs</a></b></td>
							<td style='padding: 8px;'>- Defines API endpoints for managing meal data within the application, enabling clients to retrieve, create, update, and delete meal records<br>- Serves as the primary interface for interacting with meal-related information, integrating session-based user identification and ensuring data validation<br>- Facilitates seamless communication between clients and the backend data store, supporting core CRUD operations in the overall architecture.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Controllers/RecipesApiController.cs'>RecipesApiController.cs</a></b></td>
							<td style='padding: 8px;'>- Provides RESTful API endpoints for managing recipes within the application, enabling clients to create, retrieve, update, and delete recipe data<br>- Integrates session validation to ensure secure user interactions and supports comprehensive CRUD operations aligned with the overall architecture for food-related data management<br>- Facilitates seamless communication between frontend clients and the backend data store.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Controllers/PlannerApiController.cs'>PlannerApiController.cs</a></b></td>
							<td style='padding: 8px;'>- Provides API endpoints for managing meal planners, enabling creation, retrieval, updating, and deletion of planner data associated with users<br>- Facilitates interaction between client applications and the underlying data repository, ensuring secure session validation and consistent data handling within the overall architecture<br>- Supports user-specific planning workflows integral to the applications core functionality.</td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Data Submodule -->
			<details>
				<summary><b>Data</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ API.Data</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Data/users.json'>users.json</a></b></td>
							<td style='padding: 8px;'>- Defines the initial structure for user data within the application, serving as a foundational component for managing user information<br>- It establishes a centralized JSON repository that supports user-related operations, enabling seamless data handling and integration across the broader system architecture<br>- This setup facilitates future user data management and scalability within the project.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Data/planners.json'>planners.json</a></b></td>
							<td style='padding: 8px;'>- Defines an empty collection of planners within the applications data layer, serving as a foundational structure for managing and expanding planner-related information<br>- It establishes a placeholder for future planner entries, supporting the overall architecture by enabling organized storage and retrieval of planning data as the project evolves.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Data/recipes.json'>recipes.json</a></b></td>
							<td style='padding: 8px;'>- Recipes.jsonThis file serves as the core data repository for the applications recipe content, specifically storing detailed information about various recipes<br>- It provides structured data including recipe identifiers, names, ingredients, preparation instructions, and timing details<br>- Within the overall architecture, <code>recipes.json</code> acts as the primary source of recipe data, enabling the application to display, search, and manage culinary content efficiently<br>- Its role is essential for supporting features related to recipe presentation and user interaction with the recipe catalog.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/Data/meals.json'>meals.json</a></b></td>
							<td style='padding: 8px;'>- Defines an empty dataset for meal information within the API, serving as a foundational data structure for managing and expanding meal-related content<br>- It supports the overall architecture by providing a centralized location for storing meal entries, facilitating future data retrieval, updates, and integration with other system components.</td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- DataModels Submodule -->
			<details>
				<summary><b>DataModels</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ API.DataModels</b></code>
					<!-- Users Submodule -->
					<details>
						<summary><b>Users</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ API.DataModels.Users</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/DataModels/Users/LoginResponseUserDto.cs'>LoginResponseUserDto.cs</a></b></td>
									<td style='padding: 8px;'>- Defines data transfer objects for user login responses, encapsulating success status, messages, user details, and session identifiers<br>- These models facilitate consistent communication of authentication outcomes within the API, supporting seamless integration between client and server components in the overall architecture.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/DataModels/Users/LogoutRequest.cs'>LogoutRequest.cs</a></b></td>
									<td style='padding: 8px;'>- Defines the data model for user logout requests by encapsulating the session identifier<br>- It facilitates the process of terminating user sessions within the authentication flow, ensuring proper session management and security across the applications user management system<br>- This component integrates into the broader API architecture to handle user session invalidation efficiently.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/DataModels/Users/LoginRequest.cs'>LoginRequest.cs</a></b></td>
									<td style='padding: 8px;'>- Defines the data structure for user login requests, encapsulating email and password fields with validation rules<br>- Facilitates secure and consistent data transfer during authentication processes within the API, supporting the overall architectures focus on user management and access control<br>- Ensures input integrity and streamlines login workflows across the system.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/DataModels/Users/RegisterRequestModel.cs'>RegisterRequestModel.cs</a></b></td>
									<td style='padding: 8px;'>- Defines the data structure for user registration requests, ensuring essential validation rules such as required fields, minimum lengths, and email formatting<br>- Integrates into the broader API architecture to facilitate secure and validated user onboarding, supporting consistent data handling across the user management system<br>- This model streamlines the process of capturing and validating registration input within the applications user registration workflow.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/DataModels/Users/UserModel.cs'>UserModel.cs</a></b></td>
									<td style='padding: 8px;'>- Defines the UserModel data structure representing user information within the API<br>- It encapsulates essential user attributes such as unique identifier, username, email, and password hash, facilitating consistent data handling across authentication and user management components in the overall architecture<br>- This model serves as a foundational element for user-related operations and data exchange within the system.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/DataModels/Users/UserUpdateRequest.cs'>UserUpdateRequest.cs</a></b></td>
									<td style='padding: 8px;'>- Defines the data structure for updating user information within the API, enabling clients to specify changes to username, email, or password hash<br>- Serves as a key component in user management workflows, facilitating validation and data consistency during user profile modifications across the application.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/DataModels/Users/UsersDataModel.cs'>UsersDataModel.cs</a></b></td>
									<td style='padding: 8px;'>- Defines a data structure representing a collection of user profiles within the API<br>- It facilitates organized handling and transfer of multiple user data instances, supporting core functionalities such as user management, data retrieval, and integration across the applications data layer<br>- This model ensures consistent formatting and efficient processing of user-related information throughout the system.</td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- Food Submodule -->
					<details>
						<summary><b>Food</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ API.DataModels.Food</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/DataModels/Food/PlannerModel.cs'>PlannerModel.cs</a></b></td>
									<td style='padding: 8px;'>- Defines the data structure for meal planning, encapsulating user-specific dietary schedules with associated meal identifiers and dates<br>- Facilitates organized storage and retrieval of planned meals within the broader API architecture, supporting features like personalized meal scheduling and dietary management in the application<br>- Ensures data integrity and consistency for user meal plans across the system.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/DataModels/Food/RecipesModel.cs'>RecipesModel.cs</a></b></td>
									<td style='padding: 8px;'>- Defines the data structure for representing detailed recipe information within the applications food domain<br>- Facilitates consistent storage, retrieval, and manipulation of recipe data, supporting features like recipe listing, filtering, and user reviews<br>- Serves as a core component for managing culinary content in the overall system architecture.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/API/DataModels/Food/MealsModel.cs'>MealsModel.cs</a></b></td>
									<td style='padding: 8px;'>- Defines data structures representing meal collections within the API, encapsulating meal details, associated recipes, and user linkage<br>- These models facilitate organized data exchange and storage for meal planning features, supporting the overall architecture by standardizing how meal-related information is modeled and accessed across the system.</td>
								</tr>
							</table>
						</blockquote>
					</details>
				</blockquote>
			</details>
		</blockquote>
	</details>
	<!-- Project3 Submodule -->
	<details>
		<summary><b>Project3</b></summary>
		<blockquote>
			<div class='directory-path' style='padding: 8px 0; color: #666;'>
				<code><b>⦿ Project3</b></code>
			<table style='width: 100%; border-collapse: collapse;'>
			<thead>
				<tr style='background-color: #f8f9fa;'>
					<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
					<th style='text-align: left; padding: 8px;'>Summary</th>
				</tr>
			</thead>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Program.cs'>Program.cs</a></b></td>
					<td style='padding: 8px;'>- Sets up the web applications core infrastructure, including service registration, session management, API client configuration, and routing<br>- Facilitates seamless integration of backend services and session handling, enabling the application to process user requests, manage state, and communicate with external APIs effectively within the overall architecture.</td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Project3.csproj'>Project3.csproj</a></b></td>
					<td style='padding: 8px;'>- Defines the core web application setup for the project, establishing the target framework, dependencies, and project references<br>- It orchestrates the overall architecture by integrating essential components and configurations necessary for running the application, serving as the foundational entry point that enables the project to deliver its web-based functionalities within the broader system.</td>
				</tr>
			</table>
			<!-- Models Submodule -->
			<details>
				<summary><b>Models</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Project3.Models</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Models/MealCreateViewModel.cs'>MealCreateViewModel.cs</a></b></td>
							<td style='padding: 8px;'>- Defines the data structure for creating a new meal, capturing its name and associated dishes, drinks, and desserts<br>- Facilitates user input and selection of recipes within the applications architecture, supporting the process of assembling and managing meal configurations in the overall system<br>- Ensures data validation and provides recipe options for a seamless user experience.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Models/RecipeFilter.cs'>RecipeFilter.cs</a></b></td>
							<td style='padding: 8px;'>- Defines a RecipeFilter model that encapsulates various criteria for filtering recipes within the application<br>- It enables dynamic, multi-parameter filtering of recipe collections based on attributes like name, meal type, tags, ingredients, difficulty, cuisine, and nutritional ranges, supporting flexible search and retrieval functionalities integral to the platforms recipe discovery experience.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Models/ErrorViewModel.cs'>ErrorViewModel.cs</a></b></td>
							<td style='padding: 8px;'>- Defines the ErrorViewModel class, which encapsulates request identification data for error handling within the application<br>- It facilitates the display of relevant request information during error scenarios, supporting user-friendly error pages and debugging efforts<br>- This component integrates into the overall architecture by enabling consistent error reporting and diagnostics across the project.</td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Services Submodule -->
			<details>
				<summary><b>Services</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Project3.Services</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Services/RecipeApiService.cs'>RecipeApiService.cs</a></b></td>
							<td style='padding: 8px;'>- Provides an abstraction layer for interacting with the Recipe API, enabling seamless retrieval, creation, updating, and deletion of recipe data<br>- Facilitates communication between the application and external recipe services, supporting core CRUD operations essential for managing recipe information within the overall system architecture.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Services/PlannerApiService.cs'>PlannerApiService.cs</a></b></td>
							<td style='padding: 8px;'>- Provides an API service layer for managing meal planners within the application architecture<br>- Facilitates CRUD operations for planner data, enabling seamless interaction with the backend API to retrieve, create, update, and delete user-specific meal planning information, thereby supporting the core functionality of personalized meal planning in the system.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Services/UserApiService.cs'>UserApiService.cs</a></b></td>
							<td style='padding: 8px;'>- Provides an abstraction layer for user-related API interactions within the architecture, enabling seamless retrieval, updating, and deletion of user data<br>- Facilitates communication with the backend user service, supporting core user management functionalities essential for integrating user data across the application ecosystem<br>- Ensures consistent and reliable access to user information in alignment with the overall service-oriented architecture.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Services/AuthService.cs'>AuthService.cs</a></b></td>
							<td style='padding: 8px;'>- Provides authentication functionalities by interfacing with the API to handle user registration, login, and logout processes<br>- Facilitates secure user access management within the application architecture, enabling seamless integration of authentication workflows across the system<br>- Serves as a core component for managing user sessions and ensuring authorized interactions throughout the platform.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Services/FilterService.cs'>FilterService.cs</a></b></td>
							<td style='padding: 8px;'>- Provides core filtering functionalities to evaluate string and collection data against various criteria, supporting case-insensitive matching, range comparisons, and substring searches<br>- Integral to the architecture by enabling flexible, reusable filtering logic across different data types and collections, thereby facilitating dynamic data querying and refinement within the application.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Services/MealsApiService.cs'>MealsApiService.cs</a></b></td>
							<td style='padding: 8px;'>- Provides an abstraction layer for interacting with the Meals API, enabling seamless retrieval, creation, updating, and deletion of meal data within the application<br>- Facilitates communication between the client and backend services, supporting core CRUD operations essential for managing meal information in the overall architecture<br>- Ensures consistent data handling and error management across meal-related functionalities.</td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Properties Submodule -->
			<details>
				<summary><b>Properties</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Project3.Properties</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Properties/launchSettings.json'>launchSettings.json</a></b></td>
							<td style='padding: 8px;'>- Defines the development environment configuration for the web application, specifying how the project launches locally with URL endpoints and environment variables<br>- It facilitates seamless local testing and debugging by setting up the necessary runtime parameters, ensuring the application runs correctly in a development setting within the overall architecture.</td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Controllers Submodule -->
			<details>
				<summary><b>Controllers</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Project3.Controllers</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Controllers/LoginController.cs'>LoginController.cs</a></b></td>
							<td style='padding: 8px;'>- Facilitates user authentication workflows within the web application by managing login and registration requests<br>- Handles session creation for authenticated users, ensuring secure access to protected areas, while also supporting new user account registration<br>- Integrates with external authentication services to validate credentials and maintains session state, contributing to the overall security and user management architecture.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Controllers/PlannerController.cs'>PlannerController.cs</a></b></td>
							<td style='padding: 8px;'>- Provides the core controller logic for the meal planning feature, orchestrating data retrieval from recipe and meal services to render the planning interface<br>- Facilitates user interaction with meal data, supporting dynamic content display and error handling within the applications architecture<br>- Acts as the central point for managing meal plan views and ensuring seamless data flow.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Controllers/RecipesController.cs'>RecipesController.cs</a></b></td>
							<td style='padding: 8px;'>- Facilitates user interactions with recipe data by handling requests for viewing, filtering, and creating recipes within the web application<br>- Manages session validation, orchestrates data retrieval from the API, and renders appropriate views for recipe details, listings, and creation forms, thereby integrating user input with backend services to support recipe management workflows.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Controllers/HomeController.cs'>HomeController.cs</a></b></td>
							<td style='padding: 8px;'>- Defines the main entry point for the web application, handling user requests to the homepage and managing error responses<br>- Facilitates rendering of the primary view and logs errors to support troubleshooting, thereby ensuring smooth user interactions and maintaining application stability within the overall architecture.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Controllers/MealsController.cs'>MealsController.cs</a></b></td>
							<td style='padding: 8px;'>- Facilitates meal management within the application by providing endpoints for viewing, creating, and detailing meals<br>- Integrates user session handling and recipe selection, enabling users to assemble customized meals with various dishes, drinks, and desserts<br>- Serves as a central controller orchestrating interactions between the user interface, data services, and session context to support meal planning and management workflows.</td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Views Submodule -->
			<details>
				<summary><b>Views</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Project3.Views</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/_ViewImports.cshtml'>_ViewImports.cshtml</a></b></td>
							<td style='padding: 8px;'>- Establishes shared namespaces and tag helpers for Razor views within the project, facilitating consistent access to core models and functionalities across the applications user interface components<br>- It streamlines view development by ensuring necessary dependencies are globally available, supporting cohesive and maintainable UI rendering throughout the project architecture.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/_ViewStart.cshtml'>_ViewStart.cshtml</a></b></td>
							<td style='padding: 8px;'>- Defines the default layout for views within the project, establishing a consistent visual structure across pages<br>- Serves as a foundational element in the view rendering process, ensuring uniformity and simplifying layout management throughout the applications user interface<br>- Integrates seamlessly with the overall architecture by linking individual views to the shared layout template.</td>
						</tr>
					</table>
					<!-- Planner Submodule -->
					<details>
						<summary><b>Planner</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Project3.Views.Planner</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Planner/Index.cshtml'>Index.cshtml</a></b></td>
									<td style='padding: 8px;'>- Provides a user interface for planning daily meals by selecting recipes for breakfast, lunch, dinner, and dessert<br>- Facilitates date selection and meal customization through interactive components, enabling users to organize and submit personalized meal plans within the broader application architecture<br>- Enhances user engagement by streamlining meal scheduling and visualization.</td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- Home Submodule -->
					<details>
						<summary><b>Home</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Project3.Views.Home</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Home/Index.cshtml'>Index.cshtml</a></b></td>
									<td style='padding: 8px;'>- Provides the main landing page for the recipe application, enabling users to explore and search for food and drink recipes by meal type or specific keywords<br>- Serves as the central hub for navigating the recipe collection, guiding users to various categories such as breakfast, lunch, dinner, desserts, and beverages, thereby facilitating meal planning and discovery within the overall project architecture.</td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- Shared Submodule -->
					<details>
						<summary><b>Shared</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Project3.Views.Shared</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Shared/_RecipeCreateForm.cshtml'>_RecipeCreateForm.cshtml</a></b></td>
									<td style='padding: 8px;'>- Defines a comprehensive form for creating and submitting new recipes, capturing key details such as meal type, ingredients, instructions, tags, images, and nutritional info<br>- Facilitates dynamic input management and validation, ensuring users can efficiently input structured recipe data aligned with the applications architecture for recipe management and display.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Shared/Error.cshtml'>Error.cshtml</a></b></td>
									<td style='padding: 8px;'>- Provides a user-friendly error page for the web application, displaying relevant error information and request identifiers when available<br>- It guides users and developers on error diagnosis, especially in development environments, while safeguarding sensitive details in production<br>- This component enhances the applications robustness by offering clear feedback during error occurrences within the overall architecture.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Shared/_LoginRequiredModal.cshtml'>_LoginRequiredModal.cshtml</a></b></td>
									<td style='padding: 8px;'>- Provides a modal interface prompting users to log in when attempting to access features like recipe creation<br>- It enforces authentication requirements across the application, ensuring users are directed to authenticate before managing recipes<br>- Integrates seamlessly with the overall architecture by leveraging Bootstrap for modal behavior and linking to the login page, supporting a cohesive user experience.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Shared/_Layout.cshtml'>_Layout.cshtml</a></b></td>
									<td style='padding: 8px;'>- Defines the overall layout and navigation structure for the web application, establishing consistent branding, styling, and user interface elements across pages<br>- Facilitates seamless navigation through key sections like Recipes, Meals, Planner, and Login, while integrating shared styles and scripts to ensure a cohesive user experience within the applications architecture.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Shared/_MealCreateForm.cshtml'>_MealCreateForm.cshtml</a></b></td>
									<td style='padding: 8px;'>- Defines a user interface for creating and customizing meals by selecting recipes categorized as dishes, drinks, and desserts<br>- Facilitates input of meal details and recipe choices, ensuring proper categorization and optional selections<br>- Integrates with the overall architecture to enable dynamic meal composition, supporting a flexible and user-friendly meal planning experience within the application.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Shared/_ValidationScriptsPartial.cshtml'>_ValidationScriptsPartial.cshtml</a></b></td>
									<td style='padding: 8px;'>- Provides client-side validation capabilities across the web application by integrating jQuery Validation and unobtrusive validation scripts<br>- Ensures user input adheres to defined rules before submission, enhancing data integrity and user experience within the overall project architecture<br>- Serves as a shared partial view to maintain consistent validation behavior throughout the applications user interface.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Shared/_RecipeCard.cshtml'>_RecipeCard.cshtml</a></b></td>
									<td style='padding: 8px;'>- Provides a reusable visual component for displaying recipe summaries within the application<br>- It encapsulates recipe details such as image, name, rating, and review count, enabling consistent presentation across different views<br>- Serves as a key element in the user interface for browsing and navigating recipes, contributing to a cohesive and user-friendly experience in the overall project architecture.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Shared/_RecipeFilterSidebar.cshtml'>_RecipeFilterSidebar.cshtml</a></b></td>
									<td style='padding: 8px;'>- Provides a user interface component for filtering recipes based on various criteria such as name, meal type, difficulty, tags, cuisine, ingredients, and range-based attributes like servings, review count, rating, prep time, cook time, and calories<br>- Integrates seamlessly into the overall architecture to enable dynamic, multi-faceted search capabilities, enhancing user experience by allowing precise recipe discovery within the application.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Shared/_MealsCard.cshtml'>_MealsCard.cshtml</a></b></td>
									<td style='padding: 8px;'>- Render a visual summary of individual meal items within the applications user interface, highlighting meal images, titles, and associated actions<br>- It facilitates consistent presentation of meal data across different views, enabling users to browse, identify, and interact with meal options seamlessly within the overall architecture.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Shared/_MealPicker.cshtml'>_MealPicker.cshtml</a></b></td>
									<td style='padding: 8px;'>- Provides an interactive meal selection interface within the application, enabling users to browse, choose, and visualize recipes for specific meals<br>- It dynamically displays available recipes, tracks selected items, and updates the interface accordingly, supporting the overall architecture of a user-centric meal planning feature that integrates recipe data seamlessly into the UI.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Shared/_Plannerscripts.cshtml'>_Plannerscripts.cshtml</a></b></td>
									<td style='padding: 8px;'>- Facilitates interactive meal planning by enabling users to select, visualize, and manage recipes across breakfast, lunch, dinner, and dessert categories<br>- Provides dynamic UI components for recipe selection, visual feedback, and state management, ensuring an intuitive experience for customizing meal plans<br>- Supports submission of selected recipes, integrating user input into the broader planning workflow within the application architecture.</td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- Recipes Submodule -->
					<details>
						<summary><b>Recipes</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Project3.Views.Recipes</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Recipes/CreateRecipes.cshtml'>CreateRecipes.cshtml</a></b></td>
									<td style='padding: 8px;'>- Facilitates user interaction for creating new recipes by rendering the appropriate form or login prompt based on authentication status<br>- Integrates client-side scripts for form validation and dynamic behavior, ensuring a seamless experience for authenticated users while prompting unauthenticated visitors to log in<br>- Serves as a key interface within the recipe management architecture, enabling content creation workflows.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Recipes/Index.cshtml'>Index.cshtml</a></b></td>
									<td style='padding: 8px;'>- Displays a comprehensive recipes browsing interface, integrating filtering options and recipe summaries into a cohesive layout<br>- Facilitates user exploration of a recipe library by presenting recipe cards alongside filtering controls, supporting an intuitive and organized user experience within the overall application architecture.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Recipes/RecipesIndividual.cshtml'>RecipesIndividual.cshtml</a></b></td>
									<td style='padding: 8px;'>- Displays detailed information about individual recipes, including metadata, ingredients, and instructions, to facilitate user engagement and recipe exploration within the application<br>- Serves as the primary view for presenting comprehensive recipe data, integrating user-generated content and visual elements to enhance the overall user experience in the recipe management system.</td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- Login Submodule -->
					<details>
						<summary><b>Login</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Project3.Views.Login</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Login/Index.cshtml'>Index.cshtml</a></b></td>
									<td style='padding: 8px;'>- Provides the user interface for login and registration within the Food Planner portal, enabling users to authenticate or create new accounts<br>- Integrates form handling for user credentials, displays validation feedback, and offers navigation between login and sign-up views, forming a crucial entry point for user access and account management in the overall application architecture.</td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- Meals Submodule -->
					<details>
						<summary><b>Meals</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Project3.Views.Meals</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Meals/CreateMeals.cshtml'>CreateMeals.cshtml</a></b></td>
									<td style='padding: 8px;'>- Facilitates user interaction for creating new meal entries within the application<br>- Serves as the primary interface for inputting meal details, integrating a form component and validation scripts to ensure accurate data entry<br>- Supports the overall architecture by enabling users to contribute meal data, which can then be processed and stored within the system.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Meals/MealsIndividual.cshtml'>MealsIndividual.cshtml</a></b></td>
									<td style='padding: 8px;'>- Displays detailed information about an individual meal, integrating data from the MealsModel to present recipe specifics within the applications user interface<br>- Serves as a dedicated view component in the architecture, facilitating user interaction with specific meal details and enhancing the overall user experience by providing clear, styled presentation of recipe data.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/Views/Meals/Index.cshtml'>Index.cshtml</a></b></td>
									<td style='padding: 8px;'>- Displays the Meals overview page, enabling users to browse available meal options<br>- It structures the page layout, includes styling references, and sets up placeholders for dynamic meal content<br>- Integrates with the overall application architecture by rendering meal data within a consistent UI framework, facilitating seamless navigation and user interaction within the meal browsing feature.</td>
								</tr>
							</table>
						</blockquote>
					</details>
				</blockquote>
			</details>
			<!-- wwwroot Submodule -->
			<details>
				<summary><b>wwwroot</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Project3.wwwroot</b></code>
					<!-- css Submodule -->
					<details>
						<summary><b>css</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Project3.wwwroot.css</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/wwwroot/css/Planner.css.map'>Planner.css.map</a></b></td>
									<td style='padding: 8px;'>- Defines styling rules for the Planner interface, ensuring a consistent and visually appealing user experience within the web application<br>- Integrates seamlessly with the overall project architecture by styling the Planner view, which is a key component for scheduling or task management features<br>- Enhances usability and aesthetic coherence across the applications frontend.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/wwwroot/css/Meals.css.map'>Meals.css.map</a></b></td>
									<td style='padding: 8px;'>- Defines the styling mappings for the Meals section within the web application, ensuring consistent and maintainable visual presentation<br>- Integrates SCSS source files into the overall CSS architecture, facilitating efficient styling updates and debugging<br>- Supports the project’s modular design by linking visual assets to specific views, enhancing the user interfaces coherence and responsiveness.</td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- js Submodule -->
					<details>
						<summary><b>js</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Project3.wwwroot.js</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/wwwroot/js/recipe-create.js'>recipe-create.js</a></b></td>
									<td style='padding: 8px;'>- Facilitates dynamic creation and management of recipe components, including ingredients, tags, and preparation steps, within a web form<br>- Enables users to add, remove, and reorder steps via drag-and-drop, while ensuring proper data collection for submission<br>- Also manages conditional input visibility based on recipe type, streamlining the process of crafting comprehensive, structured recipe entries in the overall application architecture.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/wwwroot/js/site.js'>site.js</a></b></td>
									<td style='padding: 8px;'>- Facilitates client-side interactions and enhances user experience within the web application by managing dynamic behaviors and UI updates<br>- Integrates with ASP.NET Cores bundling and minification processes to optimize performance<br>- Serves as the central script for implementing interactive features, ensuring smooth and responsive functionality across the websites static assets.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/wwwroot/js/meal-validation.js'>meal-validation.js</a></b></td>
									<td style='padding: 8px;'>- Implements client-side validation for meal creation forms, ensuring that meal names are provided, at least one recipe is selected, and no duplicate recipes are used across categories<br>- Enhances user experience by preventing invalid submissions and guiding users to correct input errors before form processing within the overall web application architecture.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/wwwroot/js/recipe-validation.js'>recipe-validation.js</a></b></td>
									<td style='padding: 8px;'>- Provides client-side validation for the recipe submission form, ensuring all required fields are correctly filled and formatted before submission<br>- Enhances data integrity and user experience by preventing invalid entries, offering immediate feedback, and guiding users to complete the form accurately within the overall application architecture.</td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- lib Submodule -->
					<details>
						<summary><b>lib</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Project3.wwwroot.lib</b></code>
							<!-- jquery-validation-unobtrusive Submodule -->
							<details>
								<summary><b>jquery-validation-unobtrusive</b></summary>
								<blockquote>
									<div class='directory-path' style='padding: 8px 0; color: #666;'>
										<code><b>⦿ Project3.wwwroot.lib.jquery-validation-unobtrusive</b></code>
									<table style='width: 100%; border-collapse: collapse;'>
									<thead>
										<tr style='background-color: #f8f9fa;'>
											<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
											<th style='text-align: left; padding: 8px;'>Summary</th>
										</tr>
									</thead>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/wwwroot/lib/jquery-validation-unobtrusive/LICENSE.txt'>LICENSE.txt</a></b></td>
											<td style='padding: 8px;'>- Provides licensing information for the jQuery Validation Unobtrusive library, which integrates client-side validation with server-side validation in ASP.NET Core applications<br>- It ensures proper licensing compliance for the validation scripts used across the project, supporting robust and secure form validation workflows within the overall web application architecture.</td>
										</tr>
									</table>
								</blockquote>
							</details>
							<!-- jquery Submodule -->
							<details>
								<summary><b>jquery</b></summary>
								<blockquote>
									<div class='directory-path' style='padding: 8px 0; color: #666;'>
										<code><b>⦿ Project3.wwwroot.lib.jquery</b></code>
									<table style='width: 100%; border-collapse: collapse;'>
									<thead>
										<tr style='background-color: #f8f9fa;'>
											<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
											<th style='text-align: left; padding: 8px;'>Summary</th>
										</tr>
									</thead>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/wwwroot/lib/jquery/LICENSE.txt'>LICENSE.txt</a></b></td>
											<td style='padding: 8px;'>- Provides licensing information for the jQuery library used within the project, ensuring legal clarity regarding software usage and distribution<br>- This file supports the overall architecture by documenting third-party dependencies, facilitating compliance and transparency in the web applications frontend component.</td>
										</tr>
									</table>
								</blockquote>
							</details>
							<!-- jquery-validation Submodule -->
							<details>
								<summary><b>jquery-validation</b></summary>
								<blockquote>
									<div class='directory-path' style='padding: 8px 0; color: #666;'>
										<code><b>⦿ Project3.wwwroot.lib.jquery-validation</b></code>
									<table style='width: 100%; border-collapse: collapse;'>
									<thead>
										<tr style='background-color: #f8f9fa;'>
											<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
											<th style='text-align: left; padding: 8px;'>Summary</th>
										</tr>
									</thead>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/wwwroot/lib/jquery-validation/LICENSE.md'>LICENSE.md</a></b></td>
											<td style='padding: 8px;'>- Provides licensing information for the jQuery Validation plugin, which is integral to the project’s client-side form validation framework<br>- Ensures proper attribution and legal clarity for the open-source component used within the web applications architecture, supporting reliable and standardized user input validation across the platform.</td>
										</tr>
									</table>
								</blockquote>
							</details>
							<!-- bootstrap Submodule -->
							<details>
								<summary><b>bootstrap</b></summary>
								<blockquote>
									<div class='directory-path' style='padding: 8px 0; color: #666;'>
										<code><b>⦿ Project3.wwwroot.lib.bootstrap</b></code>
									<table style='width: 100%; border-collapse: collapse;'>
									<thead>
										<tr style='background-color: #f8f9fa;'>
											<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
											<th style='text-align: left; padding: 8px;'>Summary</th>
										</tr>
									</thead>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Project3/wwwroot/lib/bootstrap/LICENSE'>LICENSE</a></b></td>
											<td style='padding: 8px;'>- Provides the licensing information for Bootstrap, a foundational front-end framework used across the project to ensure consistent, responsive, and styled user interfaces<br>- It signifies Bootstrap’s role in shaping the visual and interactive aspects of the application, supporting a cohesive and accessible user experience throughout the codebase.</td>
										</tr>
									</table>
								</blockquote>
							</details>
						</blockquote>
					</details>
				</blockquote>
			</details>
		</blockquote>
	</details>
	<!-- Tests Submodule -->
	<details>
		<summary><b>Tests</b></summary>
		<blockquote>
			<div class='directory-path' style='padding: 8px 0; color: #666;'>
				<code><b>⦿ Tests</b></code>
			<table style='width: 100%; border-collapse: collapse;'>
			<thead>
				<tr style='background-color: #f8f9fa;'>
					<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
					<th style='text-align: left; padding: 8px;'>Summary</th>
				</tr>
			</thead>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/CustomWebApplicationFactory.cs'>CustomWebApplicationFactory.cs</a></b></td>
					<td style='padding: 8px;'>- Facilitates integration testing by providing a customized web application factory that configures the test environment<br>- It ensures consistent setup of the web host, including specific API behavior options, enabling reliable and isolated testing of the APIs endpoints and functionality within the overall project architecture.</td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/Tests.csproj'>Tests.csproj</a></b></td>
					<td style='padding: 8px;'>- Defines the testing framework and dependencies for the project, enabling comprehensive unit and integration testing of the API and related components<br>- Facilitates validation of functionality and ensures code quality across the architecture, supporting continuous integration and reliable deployment processes within the overall system.</td>
				</tr>
			</table>
			<!-- ApiTests Submodule -->
			<details>
				<summary><b>ApiTests</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Tests.ApiTests</b></code>
					<!-- ServiceTests Submodule -->
					<details>
						<summary><b>ServiceTests</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Tests.ApiTests.ServiceTests</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/ServiceTests/RecipeRepositoryTests.cs'>RecipeRepositoryTests.cs</a></b></td>
									<td style='padding: 8px;'>- Provides unit tests for the RecipeRepository, validating core CRUD operations to ensure reliable data management within the applications recipe handling architecture<br>- These tests confirm correct behavior when creating, retrieving, updating, and deleting recipes, supporting the integrity and robustness of the data layer in the overall system.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/ServiceTests/MealsRepositoryTests.cs'>MealsRepositoryTests.cs</a></b></td>
									<td style='padding: 8px;'>- Provides comprehensive unit tests for the MealsRepository, ensuring reliable management of meal data within the application<br>- Validates core CRUD operations, data integrity, and correct handling of edge cases, thereby supporting the overall architectures robustness and data consistency in the food-related service layer.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/ServiceTests/UserRepositoryTests.cs'>UserRepositoryTests.cs</a></b></td>
									<td style='padding: 8px;'>- Provides unit tests for the UserRepository, validating user data management functionalities such as creating and updating user profiles<br>- Ensures that individual fields can be modified independently or collectively, maintaining data integrity<br>- These tests support the overall architecture by verifying core user data operations within the service layer, ensuring reliable user management across the application.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/ServiceTests/AuthPlannerFlowTests.cs'>AuthPlannerFlowTests.cs</a></b></td>
									<td style='padding: 8px;'>- Validate the user registration, authentication, session management, and planner creation processes through integrated service interactions<br>- Ensures that user flow from registration to planner setup functions correctly, maintaining data integrity and proper chain execution within the overall application architecture<br>- Facilitates reliable end-to-end testing of core user workflows in the system.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/ServiceTests/UserAuthTests.cs'>UserAuthTests.cs</a></b></td>
									<td style='padding: 8px;'>- Provides comprehensive tests for user authentication workflows, validating registration and login functionalities<br>- Ensures correct handling of user credentials, including successful authentication and failure scenarios with invalid passwords<br>- Supports the overall security and reliability of the user management system within the application architecture.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/ServiceTests/PlannerRepositoryTests.cs'>PlannerRepositoryTests.cs</a></b></td>
									<td style='padding: 8px;'>- Provides comprehensive unit tests for the PlannerRepository, validating core CRUD operations and data integrity within the applications planning module<br>- Ensures reliable management of user-specific meal planners, supporting the overall architectures goal of maintaining accurate, consistent, and accessible planning data across the system.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/ServiceTests/SessionServiceTests.cs'>SessionServiceTests.cs</a></b></td>
									<td style='padding: 8px;'>- Provides comprehensive unit tests for the SessionService component, validating session creation, retrieval, expiration, removal, and independence across multiple sessions<br>- Ensures the session management logic functions correctly within the overall architecture, supporting secure and reliable user session handling across the application.</td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- Repositories Submodule -->
					<details>
						<summary><b>Repositories</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Tests.ApiTests.Repositories</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Repositories/RecipeRepositoryTests.cs'>RecipeRepositoryTests.cs</a></b></td>
									<td style='padding: 8px;'>- Provides comprehensive testing for the RecipeRepository, ensuring reliable CRUD operations and data consistency within the recipe management system<br>- Validates that recipes can be retrieved, added, updated, and deleted correctly, supporting the integrity of the applications core data handling functionalities<br>- Serves as a safeguard for maintaining accurate and stable recipe data throughout the platform.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Repositories/UserRepositoryTests.cs'>UserRepositoryTests.cs</a></b></td>
									<td style='padding: 8px;'>- Provides unit tests for UserRepository to verify proper creation and management of user data files, ensuring directory and file existence, data persistence, and non-overwriting behavior<br>- Supports the overall architecture by validating reliable data storage mechanisms, which are essential for maintaining consistent user data handling within the applications data access layer.</td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- Integration Submodule -->
					<details>
						<summary><b>Integration</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Tests.ApiTests.Integration</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Integration/ProgramTests_Integration.cs'>ProgramTests_Integration.cs</a></b></td>
									<td style='padding: 8px;'>- Validates core application startup and configuration by testing essential API endpoints, Swagger UI accessibility, and CORS setup within the integration testing suite<br>- Ensures the web application initializes correctly, serves documentation, handles cross-origin requests, and maps controllers properly, thereby confirming the applications readiness for deployment and reliable operation within the overall architecture.</td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- Helpers Submodule -->
					<details>
						<summary><b>Helpers</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Tests.ApiTests.Helpers</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Helpers/TestDataBuilder.cs'>TestDataBuilder.cs</a></b></td>
									<td style='padding: 8px;'>- Provides utility functions to generate valid and invalid test data models for API testing, including recipes, meals, planners, and user authentication requests<br>- Facilitates consistent, efficient creation of mock data to support comprehensive testing of the applications API endpoints and data handling logic within the overall architecture.</td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Helpers/TestFixtureBase.cs'>TestFixtureBase.cs</a></b></td>
									<td style='padding: 8px;'>- Provides foundational setup and utility functions for API integration testing within the project<br>- Facilitates user registration, login, and creation of core entities like recipes, meals, and planners, ensuring consistent test environment initialization<br>- Supports automated tests by managing session handling and resource creation, thereby enabling reliable and repeatable API validation across the codebase.</td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- Controllers Submodule -->
					<details>
						<summary><b>Controllers</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Tests.ApiTests.Controllers</b></code>
							<!-- Users Submodule -->
							<details>
								<summary><b>Users</b></summary>
								<blockquote>
									<div class='directory-path' style='padding: 8px 0; color: #666;'>
										<code><b>⦿ Tests.ApiTests.Controllers.Users</b></code>
									<table style='width: 100%; border-collapse: collapse;'>
									<thead>
										<tr style='background-color: #f8f9fa;'>
											<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
											<th style='text-align: left; padding: 8px;'>Summary</th>
										</tr>
									</thead>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Users/UsersDeleteTests.cs'>UsersDeleteTests.cs</a></b></td>
											<td style='padding: 8px;'>- Facilitates testing of user deletion endpoints within the API, ensuring correct handling of existing and non-existent users<br>- Validates that deletion requests return appropriate HTTP status codes, thereby supporting the robustness and reliability of user management functionalities in the overall system architecture.</td>
										</tr>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Users/UsersUpdateTests.cs'>UsersUpdateTests.cs</a></b></td>
											<td style='padding: 8px;'>- Defines comprehensive API endpoint tests for updating user profiles, ensuring functionality, validation, and error handling align with system requirements<br>- Validates successful updates of username, email, and password, while also testing edge cases like non-existent users and invalid input data<br>- Supports maintaining data integrity and robustness within the user management architecture of the overall application.</td>
										</tr>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Users/UsersApiTests.cs'>UsersApiTests.cs</a></b></td>
											<td style='padding: 8px;'>- Provides automated tests for the Users API endpoints, validating core functionalities such as retrieving all users, fetching individual users by ID, and handling non-existent user requests<br>- Ensures the API responds correctly with appropriate status codes and data integrity, supporting the overall robustness and reliability of the user management component within the applications architecture.</td>
										</tr>
									</table>
								</blockquote>
							</details>
							<!-- Auth Submodule -->
							<details>
								<summary><b>Auth</b></summary>
								<blockquote>
									<div class='directory-path' style='padding: 8px 0; color: #666;'>
										<code><b>⦿ Tests.ApiTests.Controllers.Auth</b></code>
									<table style='width: 100%; border-collapse: collapse;'>
									<thead>
										<tr style='background-color: #f8f9fa;'>
											<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
											<th style='text-align: left; padding: 8px;'>Summary</th>
										</tr>
									</thead>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Auth/AuthRegisterTests.cs'>AuthRegisterTests.cs</a></b></td>
											<td style='padding: 8px;'>- Defines comprehensive API tests for user registration, validating successful registration, handling duplicate emails and usernames, and enforcing input constraints such as email format, password length, and required fields<br>- Ensures the registration endpoint reliably manages various input scenarios, supporting robust user onboarding within the overall authentication architecture.</td>
										</tr>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Auth/AuthLogoutTests.cs'>AuthLogoutTests.cs</a></b></td>
											<td style='padding: 8px;'>- Implements automated tests to validate the logout functionality within the authentication API, ensuring correct handling of valid, empty, null, and invalid session IDs<br>- These tests verify that the logout endpoint responds appropriately, maintaining the integrity and reliability of user session management across the overall system architecture.</td>
										</tr>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Auth/AuthLoginTests.cs'>AuthLoginTests.cs</a></b></td>
											<td style='padding: 8px;'>- Defines comprehensive API endpoint tests for user authentication, focusing on login functionality<br>- Validates correct responses for successful logins and various failure scenarios, ensuring robustness and security within the authentication architecture<br>- Serves as a critical component for maintaining reliable user access control across the overall system.</td>
										</tr>
									</table>
								</blockquote>
							</details>
							<!-- Planners Submodule -->
							<details>
								<summary><b>Planners</b></summary>
								<blockquote>
									<div class='directory-path' style='padding: 8px 0; color: #666;'>
										<code><b>⦿ Tests.ApiTests.Controllers.Planners</b></code>
									<table style='width: 100%; border-collapse: collapse;'>
									<thead>
										<tr style='background-color: #f8f9fa;'>
											<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
											<th style='text-align: left; padding: 8px;'>Summary</th>
										</tr>
									</thead>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Planners/PlannerCreateTests.cs'>PlannerCreateTests.cs</a></b></td>
											<td style='padding: 8px;'>- Defines API endpoint tests for creating planner entities, ensuring proper handling of valid data, invalid models, and session authorization<br>- Validates that planner creation returns correct status codes and responses, supporting the overall architectures focus on robust API validation and security enforcement within the application.</td>
										</tr>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Planners/PlannerApiTests.cs'>PlannerApiTests.cs</a></b></td>
											<td style='padding: 8px;'>- Provides automated tests for the Planner API endpoints, validating retrieval of planner data by user ID and planner ID<br>- Ensures correct responses for existing, non-existent, and empty data scenarios, supporting the overall APIs reliability and correctness within the applications architecture.</td>
										</tr>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Planners/PlannerDeleteTests.cs'>PlannerDeleteTests.cs</a></b></td>
											<td style='padding: 8px;'>- Provides automated tests for the Planner deletion API endpoint, verifying correct behavior when deleting existing planners and handling non-existent planner requests<br>- Ensures the API responds with appropriate status codes and confirms the planners removal from the system, supporting the overall robustness and reliability of the applications planner management functionality within the API architecture.</td>
										</tr>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Planners/PlannerUpdateApiTests.cs'>PlannerUpdateApiTests.cs</a></b></td>
											<td style='padding: 8px;'>- Provides automated tests for the Planner update API, ensuring correct behavior when updating existing planners, handling non-existent entries, and validating input data<br>- These tests verify the APIs robustness and correctness within the overall application architecture, supporting reliable data management and seamless user interactions in the food planning system.</td>
										</tr>
									</table>
								</blockquote>
							</details>
							<!-- Recipes Submodule -->
							<details>
								<summary><b>Recipes</b></summary>
								<blockquote>
									<div class='directory-path' style='padding: 8px 0; color: #666;'>
										<code><b>⦿ Tests.ApiTests.Controllers.Recipes</b></code>
									<table style='width: 100%; border-collapse: collapse;'>
									<thead>
										<tr style='background-color: #f8f9fa;'>
											<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
											<th style='text-align: left; padding: 8px;'>Summary</th>
										</tr>
									</thead>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Recipes/RecipesCreateTests.cs'>RecipesCreateTests.cs</a></b></td>
											<td style='padding: 8px;'>- Defines comprehensive API endpoint tests for creating recipes, ensuring correct behavior across valid, invalid, and edge case inputs<br>- Validates response status codes, data integrity, and security measures like session authentication, contributing to the robustness and reliability of the recipe management system within the overall application architecture.</td>
										</tr>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Recipes/RecipesControllerTests.cs'>RecipesControllerTests.cs</a></b></td>
											<td style='padding: 8px;'>- Provides automated tests for the Recipes API endpoints, validating retrieval of all recipes, fetching specific recipes by ID, and handling non-existent entries<br>- Ensures API reliability and correctness within the broader food management system, supporting seamless integration and data consistency across the applications architecture.</td>
										</tr>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Recipes/RecipesUpdateTests.cs'>RecipesUpdateTests.cs</a></b></td>
											<td style='padding: 8px;'>- Provides comprehensive API endpoint tests for updating recipe data, ensuring correct behavior across valid, partial, invalid, and edge cases<br>- Validates that recipe updates are processed accurately, with appropriate responses for non-existent, malformed, or invalid data, thereby safeguarding data integrity and API reliability within the overall application architecture.</td>
										</tr>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Recipes/RecipesDeleteTests.cs'>RecipesDeleteTests.cs</a></b></td>
											<td style='padding: 8px;'>- Provides automated tests for the recipe deletion API endpoints, ensuring correct behavior when deleting existing recipes and handling non-existent recipe requests<br>- Validates that successful deletions return appropriate status codes and that subsequent retrieval attempts confirm removal, thereby maintaining API reliability and data integrity within the overall application architecture.</td>
										</tr>
									</table>
								</blockquote>
							</details>
							<!-- Meals Submodule -->
							<details>
								<summary><b>Meals</b></summary>
								<blockquote>
									<div class='directory-path' style='padding: 8px 0; color: #666;'>
										<code><b>⦿ Tests.ApiTests.Controllers.Meals</b></code>
									<table style='width: 100%; border-collapse: collapse;'>
									<thead>
										<tr style='background-color: #f8f9fa;'>
											<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
											<th style='text-align: left; padding: 8px;'>Summary</th>
										</tr>
									</thead>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Meals/MealsControllerTests.cs'>MealsControllerTests.cs</a></b></td>
											<td style='padding: 8px;'>- Provides automated tests for the Meals API endpoints, ensuring correct retrieval of meal data and proper handling of non-existent entries<br>- Validates that the API returns expected status codes and data structures, supporting the overall robustness and reliability of the applications food management features within the larger system architecture.</td>
										</tr>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Meals/MealsCreateTests.cs'>MealsCreateTests.cs</a></b></td>
											<td style='padding: 8px;'>- Defines API endpoint tests for creating meal entries, validating correct behavior for successful creation, input validation, and authorization<br>- Ensures the API responds appropriately to valid data, invalid models, null inputs, and session issues, thereby maintaining the integrity and security of the meal management functionality within the overall application architecture.</td>
										</tr>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Meals/MealsDeleteTests.cs'>MealsDeleteTests.cs</a></b></td>
											<td style='padding: 8px;'>- Provides automated tests for the meal deletion API endpoint, ensuring correct behavior when deleting existing meals and handling non-existent meal requests<br>- Validates that successful deletions return appropriate status codes and that subsequent retrieval attempts confirm removal, supporting the overall API reliability and data integrity within the applications food management architecture.</td>
										</tr>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/Tests/ApiTests/Controllers/Meals/MealsUpdateTests.cs'>MealsUpdateTests.cs</a></b></td>
											<td style='padding: 8px;'>- Provides comprehensive API endpoint tests for updating meal records, ensuring correct handling of valid updates, non-existent entries, invalid data, null inputs, and complex dish and drink associations<br>- Validates that the meal update functionality responds appropriately across various scenarios, supporting the overall robustness and reliability of the applications meal management architecture.</td>
										</tr>
									</table>
								</blockquote>
							</details>
						</blockquote>
					</details>
				</blockquote>
			</details>
		</blockquote>
	</details>
	<!-- wwwroot Submodule -->
	<details>
		<summary><b>wwwroot</b></summary>
		<blockquote>
			<div class='directory-path' style='padding: 8px 0; color: #666;'>
				<code><b>⦿ wwwroot</b></code>
			<!-- css Submodule -->
			<details>
				<summary><b>css</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ wwwroot.css</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/wwwroot/css/Planner.css.map'>Planner.css.map</a></b></td>
							<td style='padding: 8px;'>- Defines styling mappings for the Planner interface, enabling consistent and maintainable visual presentation within the web application<br>- Facilitates seamless integration of SCSS styles into the overall project architecture, ensuring the Planner components appearance aligns with design standards and enhances user experience across the platform.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/wwwroot/css/Recipes.css.map'>Recipes.css.map</a></b></td>
							<td style='padding: 8px;'>- Defines styling mappings for the Recipes section within the web application, enabling consistent and maintainable visual presentation<br>- Integrates with the overall project architecture by linking SCSS source files to the compiled CSS, ensuring seamless styling updates and efficient rendering of the Recipes interface across the platform.</td>
						</tr>
					</table>
				</blockquote>
			</details>
		</blockquote>
	</details>
	<!-- P3RecipesRefactor Submodule -->
	<details>
		<summary><b>P3RecipesRefactor</b></summary>
		<blockquote>
			<div class='directory-path' style='padding: 8px 0; color: #666;'>
				<code><b>⦿ P3RecipesRefactor</b></code>
			<!-- mealtypes Submodule -->
			<details>
				<summary><b>mealtypes</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ P3RecipesRefactor.mealtypes</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/P3RecipesRefactor/mealtypes/expand_side_appetizer_to_main_meals.py'>expand_side_appetizer_to_main_meals.py</a></b></td>
							<td style='padding: 8px;'>- Refactors recipe data by standardizing meal types, replacing Appetizer and Side Dish with Breakfast, Lunch, and Dinner<br>- Enhances data consistency for meal categorization, supporting improved recipe organization and filtering within the broader codebase<br>- This transformation ensures recipes are aligned with common meal classifications, facilitating better user experience and data management.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/P3RecipesRefactor/mealtypes/NewTags.json'>NewTags.json</a></b></td>
							<td style='padding: 8px;'>- The <code>NewTags.json</code> file within the <code>P3RecipesRefactor/mealtypes</code> directory serves as a structured data repository for recipe information, specifically focusing on a collection of recipes with their associated details<br>- Its primary purpose is to define and organize recipe metadata—such as ingredients, instructions, and preparation time—in a machine-readable format<br>- This file supports the broader application architecture by enabling dynamic loading, management, and display of recipe data, facilitating features like recipe browsing, filtering, and categorization within the system<br>- Overall, it acts as a foundational data source that enhances the flexibility and scalability of the recipe management functionality in the project.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/P3RecipesRefactor/mealtypes/newrecipes_meals_expanded.json'>newrecipes_meals_expanded.json</a></b></td>
							<td style='padding: 8px;'>- The <code>newrecipes_meals_expanded.json</code> file serves as a comprehensive data source for the recipe management component of the project<br>- It defines a structured collection of recipes, including detailed ingredients and step-by-step instructions, which are likely utilized to display, search, or recommend meal options within the application<br>- This file plays a crucial role in supporting the core functionality of the system by providing the foundational data needed to present diverse meal ideas, enhance user engagement, and facilitate meal planning features across the overall architecture.</td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- bev-tags Submodule -->
			<details>
				<summary><b>bev-tags</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ P3RecipesRefactor.bev-tags</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/P3RecipesRefactor/bev-tags/tag_alcohol_status_in_tags.py'>tag_alcohol_status_in_tags.py</a></b></td>
							<td style='padding: 8px;'>- Automates the classification of beverage recipes by ensuring each recipe is accurately tagged as either Alcoholic or Non-Alcoholic based on manual name lists<br>- It updates recipe tags, maintains data integrity, and generates audit logs, supporting consistent categorization within the broader recipe management system<br>- This enhances data accuracy and facilitates targeted filtering and analysis of beverage recipes.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/P3RecipesRefactor/bev-tags/recipes.json'>recipes.json</a></b></td>
							<td style='padding: 8px;'>- Summary of <code>recipes.json</code>This <code>recipes.json</code> file serves as a centralized data source within the <code>P3RecipesRefactor</code> project, specifically under the <code>bev-tags</code> module<br>- It defines a structured collection of recipe data, including unique identifiers, ingredient lists, preparation instructions, and estimated prep times<br>- Its primary purpose is to provide a standardized, easily accessible repository of recipe information that supports the applications core functionality—likely related to recipe management, display, or processing<br>- By encapsulating recipe details in a JSON format, it enables seamless integration with other components of the codebase, facilitating data-driven features such as recipe rendering, search, or customization within the overall architecture.</td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/Redageddon/Project3/blob/master/P3RecipesRefactor/bev-tags/NewTags.json'>NewTags.json</a></b></td>
							<td style='padding: 8px;'>- Summary of <code>NewTags.json</code>This file defines a collection of recipe data within the broader codebase, serving as a structured repository for culinary recipes<br>- Its primary purpose is to provide detailed information about various recipes, including ingredients, instructions, and preparation times, which can be utilized by the application to display, manage, or process recipe content<br>- In the context of the overall architecture, <code>NewTags.json</code> acts as a data source that supports features such as recipe browsing, customization, or recommendation, enabling a seamless and organized user experience around culinary content.</td>
						</tr>
					</table>
				</blockquote>
			</details>
		</blockquote>
	</details>
</details>

---

## Getting Started

### Prerequisites

This project requires the following dependencies:

- **Programming Language:** CSharp
- **Package Manager:** Nuget

### Installation

Build CMPS3390-Project3 from the source and install dependencies:

1. **Clone the repository:**

    ```sh
    ❯ git clone https://github.com/Redageddon/Project3.git
    ```

2. **Navigate to the project directory:**

    ```sh
    ❯ cd Project3
    ```

3. **Install the dependencies:**

**Using [nuget](https://docs.microsoft.com/en-us/dotnet/csharp/):**

```sh
❯ dotnet restore
```

### Usage

Run the project with:

**Using [nuget](https://docs.microsoft.com/en-us/dotnet/csharp/):**

```sh
dotnet run
```

### Testing

Cmps3390-project3 uses the {__test_framework__} test framework. Run the test suite with:

**Using [nuget](https://docs.microsoft.com/en-us/dotnet/csharp/):**

```sh
dotnet test
```

---

<div align="left"><a href="#top">⬆ Return</a></div>

---
