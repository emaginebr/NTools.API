# zTools

[![NuGet](https://img.shields.io/nuget/v/zTools.svg)](https://www.nuget.org/packages/zTools/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Overview

**zTools** is a unified library that combines the Anti-Corruption Layer (ACL) clients and Data Transfer Objects (DTOs) for zTools API services. It provides strongly-typed HTTP clients for consuming zTools API endpoints, along with all required models, settings, and request/response objects.

This package was previously distributed as two separate packages (`zTools.ACL` and `zTools.DTO`), now unified into a single package for simpler dependency management.

## zTools Ecosystem

| Project | Type | Description | Links |
|---------|------|-------------|-------|
| **zTools.API** | REST API | Main API with controllers, business logic, and services | [GitHub](https://github.com/landim32/zTools.API) |
| **zTools** (this) | NuGet Package | ACL clients + DTOs for consuming the API | [NuGet](https://www.nuget.org/packages/zTools/) |

## Features

### Anti-Corruption Layer (ACL) Clients

Strongly-typed HTTP clients for consuming zTools API endpoints:

- **ChatGPTClient** - ChatGPT/OpenAI integration (messages, conversations, custom requests, DALL-E)
- **MailClient** - Email validation and sending (MailerSend)
- **FileClient** - File upload and URL retrieval (S3-compatible storage)
- **DocumentClient** - CPF/CNPJ validation
- **StringClient** - String manipulation utilities (slug, unique IDs, number extraction)

### Data Transfer Objects (DTOs)

#### ChatGPT Integration
- `ChatGPTRequest` / `ChatGPTResponse` - Request/response models for ChatGPT API
- `ChatGPTMessageRequest` - Simple message request wrapper
- `ChatGPTErrorResponse` / `ChatGPTError` - Error handling models
- `ChatMessage` / `Choice` / `Usage` - Conversation and usage models
- `DallERequest` / `DallEResponse` / `DallEImageData` - DALL-E image generation models

#### Email Service (MailerSend)
- `MailerInfo` - Email composition and sending
- `MailerRecipientInfo` - Recipient information
- `MailerErrorInfo` - Email service error handling

#### Configuration Settings
- `ChatGPTSetting` - OpenAI API configuration (ApiUrl, ApiKey, Model)
- `MailerSendSetting` - Email service configuration
- `S3Setting` - S3-compatible storage configuration
- `zToolsetting` - General API settings

## Installation

```bash
dotnet add package zTools
```

## Configuration

### appsettings.json

```json
{
  "NTool": {
    "ApiUrl": "https://your-zTools-api.com"
  }
}
```

### Startup.cs / Program.cs

```csharp
services.Configure<zToolsetting>(Configuration.GetSection("NTool"));
services.AddHttpClient<IChatGPTClient, ChatGPTClient>();
services.AddHttpClient<IMailClient, MailClient>();
services.AddHttpClient<IFileClient, FileClient>();
services.AddHttpClient<IDocumentClient, DocumentClient>();
services.AddHttpClient<IStringClient, StringClient>();
```

## Usage Examples

### ChatGPT Client

```csharp
using zTools.ACL;
using zTools.ACL.Interfaces;

public class MyService
{
    private readonly IChatGPTClient _chatGPTClient;

    public MyService(IChatGPTClient chatGPTClient)
    {
        _chatGPTClient = chatGPTClient;
    }

    public async Task<string> AskQuestion(string question)
    {
        return await _chatGPTClient.SendMessageAsync(question);
    }
}
```

### ChatGPT DTOs

```csharp
using zTools.DTO.ChatGPT;

var request = new ChatGPTRequest
{
    Model = "gpt-4o",
    Messages = new List<ChatMessage>
    {
        new ChatMessage { Role = "user", Content = "Hello" },
        new ChatMessage { Role = "assistant", Content = "Hi!" }
    },
    Temperature = 0.7,
    MaxCompletionTokens = 1000
};
```

### Configuration Models

```csharp
using zTools.DTO.Settings;

var chatGPTSettings = new ChatGPTSetting
{
    ApiUrl = "https://api.openai.com/v1/chat/completions",
    ApiKey = "your-api-key",
    Model = "gpt-4o"
};

var s3Settings = new S3Setting
{
    AccessKey = "your-access-key",
    SecretKey = "your-secret-key",
    Endpoint = "https://your-bucket.s3.amazonaws.com"
};
```

### Email Models

```csharp
using zTools.DTO.MailerSend;

var email = new MailerInfo
{
    From = new MailerRecipientInfo
    {
        Email = "sender@example.com",
        Name = "Sender Name"
    },
    To = new List<MailerRecipientInfo>
    {
        new MailerRecipientInfo
        {
            Email = "recipient@example.com",
            Name = "Recipient Name"
        }
    },
    Subject = "Test Email",
    Html = "<h1>Hello World</h1>",
    Text = "Hello World"
};
```

## Key Benefits

- Strongly Typed - Type-safe API calls with IntelliSense support
- Dependency Injection - Full DI support with IHttpClientFactory
- Logging - Integrated logging with ILogger
- Configuration - Uses IOptions pattern for settings
- Error Handling - Automatic HTTP error handling and dedicated error models
- JSON Serialization - Built-in JSON handling with Newtonsoft.Json
- OpenAI Compatible - ChatGPT DTOs match OpenAI API specifications

## Dependencies

- **Newtonsoft.Json** (13.0.3) - JSON serialization/deserialization
- **Microsoft.AspNetCore.Authentication** (2.3.0) - Authentication support

## Compatible With

- **.NET 8.0**
- **ASP.NET Core**
- **OpenAI API**
- **MailerSend API**
- **AWS S3 / DigitalOcean Spaces**

## Repository

- **GitHub**: [https://github.com/landim32/zTools.API](https://github.com/landim32/zTools.API)
- **Issues**: [https://github.com/landim32/zTools.API/issues](https://github.com/landim32/zTools.API/issues)

## License

MIT License - Copyright (c) Rodrigo Landim / Emagine

## Author

- **Rodrigo Landim** - [Emagine](https://github.com/landim32)
