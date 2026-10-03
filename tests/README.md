# Portable source regressions

Run with .NET 10.0.401 and C# 5:

```sh
dotnet run --project tests/Portable.Tests/Portable.Tests.csproj --configuration Release
```

Links the actual parser, packet and item interface. An inert configuration dictionary replaces only the full section catalog. Exercises valid, null, empty, unknown and rejected inputs, plus multiline aggregation. No drone, network, video, or worker object is created.

These tests do not build or validate the historical application SDK/device runtime. Original platform targets, dependencies and vendor binaries are unchanged. The .NET harness has no package feeds or external test packages.
