# SedixScope

This repository is dedicated to building a web app that showcases my dev blogs and personal portfolio using the Model-View-Controller architecture. The following technologies where used:

- .NET 8.0
- Postgres
- Bootstrap 5
- Froala Editor
- Cloudinary

## Table of Contents

- [Introduction](#introduction)
- [Technologies](#technologies)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)

## Introduction

SedixScope is a modern web application designed to showcase my personal blogs and portfolio. It leverages the power of .NET 8.0 on the backend, ASP.NET Core Razor pages on the frontend, styled with Bootstrap5, database administration with Postgres, Froala for the WYSIWYG Editor and Cloudinary for my images hosting.

## Features

- User Authentication and Authorization
- Admin Control And Functionality
- Post Creation and Management
- Commenting System
- Posts Like System
- Responsive Design

## Installation

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Cloudinary SDK](https://cloudinary.com/documentation/cloudinary_sdks)
- [Git](https://git-scm.com/)

### Steps

1. Clone the repository:

   ```sh
   git clone https://github.com/FaroukDev-tech/sedixscope.web.git
   cd sedixscope.web
   ```

2. Set up the web app:

   ```sh
   cd sedixscope.web
   dotnet restore
   dotnet build
   dotnet ef database update
   dotnet run
   ```

## Usage

1. Navigate to `http://localhost:5000` in your web browser to access the frontend of web app.

## Contributing

Contributions are welcome! Please fork this repository and submit pull requests with your changes. For major changes, please open an issue first to discuss what you would like to change.

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/fooBar`)
3. Commit your changes (`git commit -m 'Add some fooBar'`)
4. Push to the branch (`git push origin feature/fooBar`)
5. Create a new Pull Request
