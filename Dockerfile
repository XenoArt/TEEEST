<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.3" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.3">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.3" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.3">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="8.0.3" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
    <!-- Added the direct reference -->
    <PackageReference Include="Microsoft.EntityFrameworkCore.Relational" Version="9.0.3" />
  </ItemGroup>

</Project>
