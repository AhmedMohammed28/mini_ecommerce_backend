# mini_ecommerce_backend

A minimal single-layer e-commerce backend built with the [ABP Framework](https://abp.io). It provides product management and order creation with automatic discount calculation, served via a Blazor Server UI.

---

## Features

### 🛍️ Products
- List products with name, price, and available stock
- Create new products with price and quantity validation
- Stock is automatically decremented when an order is placed

### 📦 Orders
- Create orders by selecting one or more products and quantities
- Automatic discount applied based on total item count:
  | Total Items | Discount |
  |---|---|
  | 1 | 0% |
  | 2–4 | 5% |
  | 5+ | 10% |
- Displays subtotal, discount rate, and final total
- Stock validation — prevents ordering more than what is available

---

## Domain Model

```
Product
  - Id (Guid)
  - Name (string)
  - Price (decimal)
  - Quantity (int)  ← decremented on order

Order
  - Id (Guid)
  - CustomerName (string)
  - CustomerEmail (string)
  - Items → OrderItem[]

OrderItem
  - ProductId, ProductName
  - UnitPrice, Quantity
```

---

## Project Structure

Single-layer ABP application under `mini_ecommerce_backend/`:

```
Entities/
  Products/Product.cs
  Orders/Order.cs, OrderItem.cs
Services/
  Products/ProductAppService.cs
  Orders/OrderAppService.cs
  Dtos/...
Components/Pages/
  Products.razor       ← product list
  CreateOrder.razor    ← order creation form
Data/
  mini_ecommerce_backendDbContext.cs
  BookStoreDataSeederContributor.cs  ← seeds sample products
Migrations/           ← EF Core migrations
```

---

## Pre-requirements

- [.NET 10.0+ SDK](https://dotnet.microsoft.com/download/dotnet)
- [Node v18 or 20](https://nodejs.org/en)
- SQL Server (configured in `appsettings.json`)

---

## Getting Started

### 1. Configure the database

Edit `ConnectionStrings` in `mini_ecommerce_backend/appsettings.json`:

```json
"ConnectionStrings": {
  "Default": "Server=localhost;Database=mini_ecommerce_backend;..."
}
```

### 2. Apply migrations

```bash
dotnet ef database update --project mini_ecommerce_backend
```

Or use the provided script:

```powershell
.\migrate-database.ps1
```

### 3. Install client-side libraries

```bash
abp install-libs
```

### 4. Run the application

```bash
cd mini_ecommerce_backend
dotnet run
```

The app seeds sample products automatically on first run:
- **Laptop** — $1,500 × 10 units
- **Mouse** — $25 × 100 units

---

## Generating an OpenIddict Signing Certificate

A certificate (`openiddict.pfx`) is included for development. To regenerate it:

```bash
dotnet dev-certs https -v -ep openiddict.pfx -p a323e97a-bb81-40b9-8713-ccbc97eefa6d
```

For production, use two separate RSA certificates (one for signing, one for encryption). See [OpenIddict Certificate Configuration](https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html).

---

## Deploying with Docker

```powershell
# Build images
.\etc\build\build-images-locally.ps1

# Start containers
.\etc\docker\run-docker.ps1

# Stop containers
.\etc\docker\stop-docker.ps1
```

> Developer certificates are only valid for `localhost`. Use Let's Encrypt or similar for production DNS.

---

## Additional Resources

- [ABP Framework Docs](https://abp.io/docs/latest)
- [ABP Single Layer Template](https://abp.io/docs/latest/solution-templates/application-single-layer)
- [ABP Deployment Guide](https://abp.io/docs/latest/Deployment/Index)
