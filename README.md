# PositionTracker API

## Overview
Position Tracker API is a robust service built using vertical slice architecture designed to handle various location-based queries. It supports converting IP addresses to physical locations and can be easily integrated into your web applications. The system uses data from third-party providers and caches results in MongoDB for optimized performance.

## Features
- **IP to Location**: Converts IP addresses to physical locations using third-party data.
- **Caching**: Utilize MongoDB for improved performance.
- **Dockerizing**: Containerize the LocateAPI using DockerFile and Docker Compose for simplified deployment and management.
- **Minimal APIs**: Leveraging ASP.NET Core's minimal APIs for creating lightweight HTTP APIs with minimal dependencies.

## Tools and Technologies
- **ASP.NET Core and .NET 8**
- **AutoMapper**: For object-object mapping.
- **FluentValidation**: For model validation.
- **MongoDB**: For caching previously queried data.
- **Entity Framework Core**: For database interaction.
- **OpenAPI**: For generating Swagger API documentation.
- **Docker**: For containerizing the application.
- **Xunit**: For unit testing.
- **Moq**: For mocking dependencies in tests.
- **FluentAssertions**: For more readable and maintainable assertions in tests.

## Architecture Overview
### Components
- **Request For Location**: Initiates requests for various location-based queries.
- **Features**: Core functionalities that process different types of location requests.
- **Related Service**: Handles specific tasks related to each feature.
- **Provider**: Third-party services that provide data for location.
- **Cache**: MongoDB is used to store previously queried data to improve performance.

## Getting Started

1. **Clone the Repository**:
    ```bash
    git clone https://github.com/Ananyapk25/PositionTracker-API.git
    ```

2. **Install Dependencies**:
    - Ensure you have **.NET SDK 8** installed.
    - Open a terminal and navigate to the project folder:
        ```bash
        cd PositionTracker-API
        ```
    - Restore dependencies:
        ```bash
        dotnet restore
        ```
3. **Pull and Run MongoDB Docker Image**:
    - Ensure you have Docker installed and running.
    - Pull the MongoDB Docker image:
        ```bash
        docker pull mongo
        ```
    - Run the MongoDB container:
        ```bash
        docker run -d -p 27017:27017 --name PositionTracker-API-mongo mongo
        ```


4. **Run the Application**:
    - Start the API by running the following command in your terminal:
        ```bash
        dotnet run
        ```

5. **Explore the API**:
    - Once the API is running, you can use tools like **Swagger** to interact with the endpoints.
    - Visit the Swagger UI at `https://localhost:<PORT>/swagger/index.html` to explore the API documentation interactively.

## AppSettings.json Configuration

Please note that `appsettings.json` files are not included in the Git repository. These files typically contain sensitive information such as database connection strings, API keys, and other configuration settings specific to the environment.

For local testing, you can create your own `appsettings.json` file in the root directory of the LocateAPI project and add the necessary configurations.

### Local Configuration

Here's an example of how you can configure your `appsettings.json` file:

```json
{
  "MongoDatabase": {
    "Host": "mongodb://localhost:27017",
    "DatabaseName": "PositionTracker-API"
  },
  "Features": {
    "IpLocation": {
      "IpApiBaseUrl": "http://ip-api.com/json/"
    }
  },
  // Other configurations...
}
```
If you deploy the LocateAPI to Azure App Service, you can add the connection strings in the Connection Strings section of the Configuration settings in the Azure portal. Azure App Service securely encrypts connection strings at rest and transmits them over an encrypted channel, ensuring the security of your sensitive information.
