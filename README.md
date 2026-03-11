# Goal

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![Quality Gate Status][sonarcloud-shield]][sonarcloud-url]

**Goal** is a .NET infrastructure library that provides reusable abstractions and base implementations for building enterprise applications. It follows Domain-Driven Design (DDD) and CQRS patterns, with support for Entity Framework Core, MediatR, and standardized HTTP API responses.

## Table of Contents

- [About](#about)
- [Packages](#packages)
- [Getting Started](#getting-started)
- [Architecture](#architecture)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## About

Goal offers a set of NuGet packages that help you structure .NET applications with:

- **Domain layer** — Entities, value objects, domain events, and repository contracts
- **Application layer** — Command handlers (CQRS), app services, and projection extensions
- **Infrastructure** — Repository implementations with EF Core, query repositories, HTTP controller base classes, and auditing
- **Crosscutting** — Specification pattern, pagination, validation (Ensure), and extension utilities

Built by [Ritter Soluções em TI](https://github.com/ritter-ti).

## Packages

| Package                              | Description                                                         |
| ------------------------------------ | ------------------------------------------------------------------- |
| `Goal.Domain.Abstractions`           | Domain entities, value objects, events, and repository interfaces   |
| `Goal.Application.Abstractions`      | Command/query handlers, app services, and MediatR integration       |
| `Goal.Infra.Data.Abstractions`       | Repository pattern, unit of work, and EF Core auditing              |
| `Goal.Infra.Data.Query.Abstractions` | Read-only query repository abstractions                             |
| `Goal.Infra.Http.Abstractions`       | API controller base classes, pagination, and standardized responses |
| `Goal.Infra.Crosscutting`            | Specifications, collections, Ensure validation, and extensions      |

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/ritter-ti/goal.git
   cd goal
   ```

2. Restore and build:

   ```bash
   dotnet restore
   dotnet build
   ```

3. Run tests:

   ```bash
   dotnet test
   ```

### Using as NuGet Packages

Add the packages you need to your project:

```xml
<PackageReference Include="Goal.Domain.Abstractions" Version="3.2.2" />
<PackageReference Include="Goal.Application.Abstractions" Version="3.2.2" />
<PackageReference Include="Goal.Infra.Data.Abstractions" Version="3.2.2" />
<PackageReference Include="Goal.Infra.Http.Abstractions" Version="3.2.2" />
<PackageReference Include="Goal.Infra.Crosscutting" Version="3.2.2" />
```

## Architecture

```
src/
├── Crosscutting/
│   └── Infra.Crosscutting/          # Specifications, pagination, Ensure, extensions
└── Abstractions/
    ├── Domain/                       # Entities, value objects, events, IRepository
    ├── Application/                 # ICommand, ICommandHandler, IAppService
    ├── Infra.Data/                  # Repository<TEntity,TKey>, UnitOfWork, auditing
    ├── Infra.Data.Query/            # IQueryRepository
    └── Infra.Http/                  # ApiController, PagedResponse, result types
```

### Key Concepts

- **Specification pattern** — Composable query logic with `&`, `|`, and `!` operators
- **Repository** — Generic CRUD and search with specifications and pagination
- **Commands** — MediatR-based command handlers for write operations
- **Paged responses** — Standardized pagination for HTTP APIs

## Usage

### Domain Entity

```csharp
public class Product : Entity<Guid>
{
    public string Name { get; init; } = string.Empty;
}
```

### Specification

```csharp
public class ActiveProductsSpec : Specification<Product>
{
    public override Expression<Func<Product, bool>> SatisfiedBy() =>
        p => p.IsActive;
}
```

### Repository

```csharp
public class ProductRepository : Repository<Product, Guid>
{
    public ProductRepository(DbContext context) : base(context) { }
}
```

### API Controller with Pagination

```csharp
public class ProductsController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PageSearchRequest request)
    {
        var pageSearch = request.ToPageSearch();
        var result = await _queryRepository.SearchAsync(pageSearch, HttpContext.RequestAborted);
        return OkOrNotFound(result);
    }
}
```

## Contributing

Contributions are welcome. Please read [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on code style and commit messages.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'feat(scope): add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

Distributed under the MIT License. See [LICENSE.txt](LICENSE.txt) for more information.

---

**Project Link:** [https://github.com/ritter-ti/goal](https://github.com/ritter-ti/goal)

[contributors-shield]: https://img.shields.io/github/contributors/ritter-ti/goal.svg?style=for-the-badge
[contributors-url]: https://github.com/ritter-ti/goal/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/ritter-ti/goal.svg?style=for-the-badge
[forks-url]: https://github.com/ritter-ti/goal/network/members
[stars-shield]: https://img.shields.io/github/stars/ritter-ti/goal.svg?style=for-the-badge
[stars-url]: https://github.com/ritter-ti/goal/stargazers
[issues-shield]: https://img.shields.io/github/issues/ritter-ti/goal.svg?style=for-the-badge
[issues-url]: https://github.com/ritter-ti/goal/issues
[license-shield]: https://img.shields.io/github/license/ritter-ti/goal.svg?style=for-the-badge
[license-url]: https://github.com/ritter-ti/goal/blob/master/LICENSE.txt
[sonarcloud-shield]: https://sonarcloud.io/api/project_badges/measure?project=rtt3r_goal&metric=alert_status
[sonarcloud-url]: https://sonarcloud.io/summary/new_code?id=rtt3r_goal
